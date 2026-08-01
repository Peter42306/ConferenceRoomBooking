using ConferenceRoomBooking.Application.DTOs.Services;

namespace ConferenceRoomBooking.Application.Interfaces.Services
{
    public interface IServiceService
    {
        Task<int> CreateAsync(
            CreateServiceDto dto,
            CancellationToken ct = default);
    }
}
