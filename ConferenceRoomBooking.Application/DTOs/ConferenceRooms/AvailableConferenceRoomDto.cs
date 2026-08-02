namespace ConferenceRoomBooking.Application.DTOs.ConferenceRooms
{
    public sealed record AvailableConferenceRoomDto(
        int Id,
        string Name,
        int Capacity,
        decimal RatePerHour);
}
