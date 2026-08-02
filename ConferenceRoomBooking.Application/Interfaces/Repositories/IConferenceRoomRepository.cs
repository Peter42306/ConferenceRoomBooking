using ConferenceRoomBooking.Domain.Entities;

namespace ConferenceRoomBooking.Application.Interfaces.Repositories
{
    public interface IConferenceRoomRepository
    {
        Task<bool> ExistsByNameAsync(
            string name,
            CancellationToken ct = default);

        Task AddAsync(
            ConferenceRoom conferenceRoom,
            CancellationToken ct = default);

        Task SaveChangesAsync(
            CancellationToken ct = default);

        Task<ConferenceRoom?> GetByIdWithServicesAsync(
            int id,
            CancellationToken ct = default);
    }
}
