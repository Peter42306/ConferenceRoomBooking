using ConferenceRoomBooking.Domain.Enums;

namespace ConferenceRoomBooking.Application.DTOs.Bookings
{
    public sealed record BookingDto(
        int Id,
        int ConferenceRoomId,
        string ConferenceRoomName,
        DateTime StartTime,
        DateTime EndTime,
        BookingStatus Status,
        decimal RentalPrice,
        decimal ServicePrice,
        decimal TotalPrice,
        IReadOnlyCollection<BookingServiceDto> Services);
}
