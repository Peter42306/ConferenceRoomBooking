namespace ConferenceRoomBooking.Application.DTOs.ConferenceRooms
{
    public sealed record UpdateConferenceRoomDto(
        string Name,
        int Capacity,
        decimal RatePerHour,
        IReadOnlyCollection<int> ServiceIds);    
}
