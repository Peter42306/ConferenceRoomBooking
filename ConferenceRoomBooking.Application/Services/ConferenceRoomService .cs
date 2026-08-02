using ConferenceRoomBooking.Application.DTOs.ConferenceRooms;
using ConferenceRoomBooking.Application.Interfaces.Repositories;
using ConferenceRoomBooking.Application.Interfaces.Services;
using ConferenceRoomBooking.Domain.Entities;

namespace ConferenceRoomBooking.Application.Services
{
    public sealed class ConferenceRoomService : IConferenceRoomService
    {
        private readonly IConferenceRoomRepository _conferenceRoomRepository;
        private readonly IServiceRepository _serviceRepository;

        public ConferenceRoomService(
            IConferenceRoomRepository conferenceRoomRepository,
            IServiceRepository serviceRepository)
        {
            _conferenceRoomRepository = conferenceRoomRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<int> CreateAsync(
            CreateConferenceRoomDto dto,
            CancellationToken ct = default)
        {
            var roomExists = await _conferenceRoomRepository.ExistsByNameAsync(
                dto.Name,
                ct);

            if (roomExists)
            {
                throw new InvalidOperationException(
                    $"Conference room with name '{dto.Name}' already exists.");
            }

            var serviceIds = dto.ServiceIds
                .Distinct()
                .ToArray();

            var services = await _serviceRepository.GetByIdsAsync(
                serviceIds,
                ct);

            if (services.Count != serviceIds.Length)
            {
                throw new ArgumentException(
                    "One ore more selected services do not exist.");
            }

            var conferenceRoom = new ConferenceRoom
            {
                Name = dto.Name,
                Capacity = dto.Capacity,
                RatePerHour = dto.RatePerHour,
                Services = services.ToList()
            };

            await _conferenceRoomRepository.AddAsync(
                conferenceRoom,
                ct);

            await _conferenceRoomRepository.SaveChangesAsync(ct);

            return conferenceRoom.Id;
        }
    }
}
