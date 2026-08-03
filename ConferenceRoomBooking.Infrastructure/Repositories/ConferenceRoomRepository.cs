using ConferenceRoomBooking.Application.Interfaces.Repositories;
using ConferenceRoomBooking.Domain.Entities;
using ConferenceRoomBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Infrastructure.Repositories
{
    public sealed class ConferenceRoomRepository : IConferenceRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public ConferenceRoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByNameAsync(
            string name, 
            CancellationToken ct = default)
        {
            return await _context.ConferenceRooms
                .AnyAsync(room => room.Name == name, ct);
        }

        public async Task AddAsync(
            ConferenceRoom conferenceRoom, 
            CancellationToken ct = default)
        {
            await _context.ConferenceRooms
                .AddAsync(conferenceRoom, ct);
        }
        
        public async Task SaveChangesAsync(
            CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task<ConferenceRoom?> GetByIdWithServicesAsync(
            int id, 
            CancellationToken ct = default)
        {
            return await _context.ConferenceRooms
                .Include(room => room.Services)
                .FirstOrDefaultAsync(room => room.Id == id, ct);
        }

        public async Task<ConferenceRoom?> GetByIdAsync(
            int id, 
            CancellationToken ct = default)
        {
            return await _context.ConferenceRooms
                .FirstOrDefaultAsync(room => room.Id == id, ct);
        }

        public void Remove(ConferenceRoom conferenceRoom)
        {
            _context.ConferenceRooms.Remove(conferenceRoom);
        }

        public async Task<IReadOnlyCollection<ConferenceRoom>> SearchAvailableAsync(
            DateTime startTime, 
            DateTime endTime, 
            int capacity, 
            CancellationToken ct = default)
        {
            return await _context.ConferenceRooms
                .AsNoTracking()
                .Where(room => room.Capacity >= capacity)
                .Where(room => !room.Bookings.Any(booking => booking.StartTime < endTime && booking.EndTime > startTime))
                .OrderBy(room => room.Capacity)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyCollection<ConferenceRoom>> GetAllWithServicesAsync(
            CancellationToken ct = default)
        {
            return await _context.ConferenceRooms
                .AsNoTracking()
                .Include(room => room.Services)
                .OrderBy(room => room.Name)
                .ToListAsync(ct);
        }
    }
}
