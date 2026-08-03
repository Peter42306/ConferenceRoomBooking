using ConferenceRoomBooking.Application.Interfaces.Repositories;
using ConferenceRoomBooking.Domain.Entities;
using ConferenceRoomBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Infrastructure.Repositories
{
    public sealed class BookingRepository:IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsOverlappingBookingAsync(
            int conferenceRoomId, 
            DateTime startTime, 
            DateTime endTime, 
            CancellationToken ct = default)
        {
            return await _context.Bookings.AnyAsync(
                booking =>
                    booking.ConferenceRoomId == conferenceRoomId &&
                    booking.StartTime < endTime &&
                    booking.EndTime > startTime,
                ct);
        }

        public async Task AddAsync(
            Booking booking, 
            CancellationToken ct = default)
        {
            await _context.Bookings.AddAsync(booking, ct);
        }        

        public async Task SaveChangesAsync(
            CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyCollection<Booking>> GetAllAsync(
            CancellationToken ct = default)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Include(booking => booking.ConferenceRoom)
                .Include(booking => booking.BookingServices)
                    .ThenInclude(bookingService => bookingService.Service)
                .OrderByDescending(booking => booking.StartTime)
                .ToListAsync();
        }
    }
}
