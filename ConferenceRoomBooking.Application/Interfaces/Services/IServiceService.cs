using ConferenceRoomBooking.Application.DTOs.Services;

namespace ConferenceRoomBooking.Application.Interfaces.Services
{
    public interface IServiceService
    {
        Task<int> CreateAsync(
            CreateServiceDto dto,
            CancellationToken ct = default);

        Task<bool> UpdateAsync(
            int id,
            UpdateServiceDto dto,
            CancellationToken ct = default);
    }
}
