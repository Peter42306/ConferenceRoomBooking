namespace ConferenceRoomBooking.Application.DTOs.Bookings
{
    public sealed record CreateBookingDto(
        int ConferenceRoomId,
        DateTime StartTime,
        int DurationHours,
        IReadOnlyCollection<int> ServiceIds);
}
