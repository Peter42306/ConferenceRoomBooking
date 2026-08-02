using ConferenceRoomBooking.Application.DTOs.Bookings;
using ConferenceRoomBooking.Application.Interfaces.Repositories;
using ConferenceRoomBooking.Application.Interfaces.Services;
using ConferenceRoomBooking.Domain.Entities;
using ConferenceRoomBooking.Domain.Enums;
using BookingServiceEntity = ConferenceRoomBooking.Domain.Entities.BookingService;

namespace ConferenceRoomBooking.Application.Services
{
    public sealed class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IConferenceRoomRepository _conferenceRoomRepository;

        public BookingService(
            IBookingRepository bookingRepository,
            IConferenceRoomRepository conferenceRoomRepository)
        {
            _bookingRepository = bookingRepository;
            _conferenceRoomRepository = conferenceRoomRepository;
        }

        public async Task<BookingResultDto> CreateAsync(
            CreateBookingDto dto, 
            CancellationToken ct = default)
        {
            if (dto.StartTime < DateTime.UtcNow)
            {
                throw new ArgumentException(
                    "Start time cannot be in the past.");
            }

            if (dto.StartTime.Minute != 0 || dto.StartTime.Second != 0)
            {
                throw new ArgumentException(
                    "Booking must start at the beginning of an hour.");
            }

            if (dto.DurationHours <= 0)
            {
                throw new ArgumentException(
                    "Duration must be greater than zero.");
            }

            var endTime = dto.StartTime.AddHours(dto.DurationHours);

            var conferenceRoom = await _conferenceRoomRepository.GetByIdWithServicesAsync(
                dto.ConferenceRoomId,
                ct);

            if (conferenceRoom is null)
            {
                throw new KeyNotFoundException(
                    $"Conference room with id '{dto.ConferenceRoomId}' was not found.");
            }

            var hasOverlap = await _bookingRepository.ExistsOverlappingBookingAsync(
                dto.ConferenceRoomId,
                dto.StartTime,
                endTime,
                ct);

            if (hasOverlap)
            {
                throw new InvalidOperationException(
                    "The conference room is already booked for the selected time.");
            }

            var selectedServiceIds = dto.ServiceIds
                .Distinct()
                .ToArray();

            var selectedServices = conferenceRoom.Services
                .Where(service => selectedServiceIds.Contains(service.Id))
                .ToList();

            var allSelectedServicesAreAvailable = selectedServices.Count == selectedServiceIds.Length;

            if (!allSelectedServicesAreAvailable)
            {
                throw new ArgumentException(
                    "One or more selected services are not available for this conference room.");
            }            

            var rentalPrice = CalculateRentalPrice(
                conferenceRoom.RatePerHour,
                dto.StartTime,
                endTime);

            
            decimal servicesPrice = 0;

            foreach (var service in selectedServices)
            {
                servicesPrice += service.Price;
            }

            var booking = new Booking
            {
                ConferenceRoomId = conferenceRoom.Id,
                StartTime = dto.StartTime,
                EndTime = endTime,
                Status = BookingStatus.Confirmed,
                RentalPrice = rentalPrice,
                ServicesPrice = servicesPrice
            };

            foreach (var service in selectedServices)
            {
                booking.BookingServices.Add(
                    new BookingServiceEntity
                    {
                        ServiceId = service.Id,
                        Price = service.Price
                    });
            }

            await _bookingRepository.AddAsync(booking, ct);
            await _bookingRepository.SaveChangesAsync(ct);

            return new BookingResultDto(
                BookingId: booking.Id,
                RentalPrice: booking.RentalPrice,
                ServicesPrice: booking.ServicesPrice,
                TotalPrice: booking.RentalPrice + booking.ServicesPrice);
        }


        // helpers

        private static decimal CalculateRentalPrice(
            decimal ratePerHour,
            DateTime startTime,
            DateTime endTime)
        {
            decimal total = 0;

            for (var current = startTime; current < endTime; current = current.AddHours(1))
            {
                total += ratePerHour * GetPriceMultiplier(current.TimeOfDay);
            }

            return decimal.Round(total, 2);
        }

        private static decimal GetPriceMultiplier(TimeSpan time)
        {
            if (time >= TimeSpan.FromHours(12) &&
                time < TimeSpan.FromHours(14))
            {
                return 1.15m;
            }

            if (time >= TimeSpan.FromHours(18) &&
                time < TimeSpan.FromHours(23))
            {
                return 0.80m;
            }

            if (time >= TimeSpan.FromHours(6) &&
                time < TimeSpan.FromHours(9))
            {
                return 0.90m;
            }

            if (time >= TimeSpan.FromHours(9) &&
                time < TimeSpan.FromHours(18))
            {
                return 1.00m;
            }           

            throw new ArgumentException(
                "Bookings are allowed only between 06:00 and 23:00.");
        }
    }
}
