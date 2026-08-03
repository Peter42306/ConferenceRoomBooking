using ConferenceRoomBooking.Domain.Entities;

namespace ConferenceRoomBooking.Application.Interfaces.Repositories
{
    public interface IServiceRepository
    {
        Task<bool> ExistsByNameAsync(
            string name, 
            CancellationToken ct = default);

        Task AddAsync(
            Service service,
            CancellationToken ct = default);

        Task SaveChangesAsync(
            CancellationToken ct = default);

        Task<Service?> GetByIdAsync(
            int id,
            CancellationToken ct = default);

        void Remove(Service service);

        Task<IReadOnlyCollection<Service>> GetByIdsAsync(
            IReadOnlyCollection<int> ids,
            CancellationToken ct = default);

        Task<IReadOnlyCollection<Service>> GetAllAsync(
            CancellationToken ct = default);
    }
}
