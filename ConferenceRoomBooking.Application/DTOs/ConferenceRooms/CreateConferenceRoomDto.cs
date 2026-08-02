namespace ConferenceRoomBooking.Application.DTOs.ConferenceRooms
{
    public sealed record CreateConferenceRoomDto(
        string Name,
        int Capacity,
        decimal RatePerHour,
        IReadOnlyCollection<int> ServiceIds);
}
