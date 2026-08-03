using ConferenceRoomBooking.Application.DTOs.Services;

namespace ConferenceRoomBooking.Application.DTOs.ConferenceRooms
{
    public sealed record ConferenceRoomDto(
        int Id,
        string Name,
        int Capacity,
        decimal RatePerHour,
        IReadOnlyCollection<ServiceDto> Services);
}
