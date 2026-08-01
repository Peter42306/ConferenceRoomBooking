using ConferenceRoomBooking.Application.Interfaces.Repositories;
using ConferenceRoomBooking.Domain.Entities;
using ConferenceRoomBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Infrastructure.Repositories
{
    public sealed class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByNameAsync(
            string name, 
            CancellationToken ct = default)
        {
            return await _context.Services.AnyAsync(service => service.Name == name, ct);
        }

        public async Task AddAsync(
            Service service, 
            CancellationToken ct = default)
        {
            await _context.Services.AddAsync(service, ct);
        }

        public async Task SaveChangesAsync(
            CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task<Service?> GetByIdAsync(
            int id, 
            CancellationToken ct = default)
        {
            return await _context.Services
                .FirstOrDefaultAsync(
                    service => service.Id == id,
                    ct);
        }

        public void Remove(Service service)
        {
            _context.Services.Remove(service);
        }
    }
}
