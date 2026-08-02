namespace ConferenceRoomBooking.Application.DTOs.ConferenceRooms
{
    public sealed record SearchAvailableConferenceRoomsDto(
        DateTime StartTime,
        DateTime EndTime,
        int Capacity);
}
