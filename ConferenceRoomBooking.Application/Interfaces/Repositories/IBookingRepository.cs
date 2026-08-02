using ConferenceRoomBooking.Domain.Entities;

namespace ConferenceRoomBooking.Application.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        Task<bool> ExistsOverlappingBookingAsync(
            int conferenceRoomId,
            DateTime startTime,
            DateTime endTime,
            CancellationToken ct = default);

        Task AddAsync(
            Booking booking,
            CancellationToken ct = default);

        Task SaveChangesAsync(
            CancellationToken ct = default);
    }
}
