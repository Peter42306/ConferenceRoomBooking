using ConferenceRoomBooking.Application.DTOs.Bookings;

namespace ConferenceRoomBooking.Application.Interfaces.Services
{
    public interface IBookingService
    {
        Task<BookingResultDto> CreateAsync(
            CreateBookingDto dto,
            CancellationToken ct = default);

        Task<IReadOnlyCollection<BookingDto>> GetAllAsync(
            CancellationToken ct = default);
    }    
}
