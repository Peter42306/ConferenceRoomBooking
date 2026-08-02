using ConferenceRoomBooking.Application.DTOs.ConferenceRooms;

namespace ConferenceRoomBooking.Application.Interfaces.Services
{
    public interface IConferenceRoomService
    {
        Task<int> CreateAsync(
            CreateConferenceRoomDto dto,
            CancellationToken ct = default);

        Task<bool> UpdateAsync(
            int id,
            UpdateConferenceRoomDto dto,
            CancellationToken ct = default);

        Task<bool> DeleteAsync(
            int id,
            CancellationToken ct = default);
    }
}
