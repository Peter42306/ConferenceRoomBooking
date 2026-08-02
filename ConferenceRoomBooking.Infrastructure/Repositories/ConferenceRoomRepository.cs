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
    }
}
