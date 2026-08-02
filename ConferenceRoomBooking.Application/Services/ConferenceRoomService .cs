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

            var allServicesExist = services.Count == serviceIds.Length;

            if (!allServicesExist)
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

        
        public async Task<bool> UpdateAsync(
            int id,
            UpdateConferenceRoomDto dto, 
            CancellationToken ct = default)
        {
            var conferenceRoom = await _conferenceRoomRepository.GetByIdWithServicesAsync(id, ct);

            if (conferenceRoom is null)
            {
                return false;
            }

            var nameChanged = conferenceRoom.Name != dto.Name;

            if (nameChanged)
            {
                var roomExists = await _conferenceRoomRepository.ExistsByNameAsync(dto.Name, ct);

                if (roomExists)
                {
                    throw new InvalidOperationException(
                        $"Conference room with name '{dto.Name}' already exists.");
                }
            }           

            var selectedServiceIds = dto.ServiceIds
                .Distinct()
                .ToArray();            

            var selectedServices = await _serviceRepository.GetByIdsAsync(selectedServiceIds, ct);

            var allServiceExist = selectedServiceIds.Length == selectedServices.Count;

            if (!allServiceExist)
            {
                throw new ArgumentException(
                    "One or more selected services do not exist.");
            }

            conferenceRoom.Name = dto.Name;
            conferenceRoom.Capacity = dto.Capacity;
            conferenceRoom.RatePerHour = dto.RatePerHour;

            // Replace current services with services selected in the DTO.
            conferenceRoom.Services.Clear();
            foreach (var service in selectedServices)
            {
                conferenceRoom.Services.Add(service);
            }

            await _conferenceRoomRepository.SaveChangesAsync(ct);

            return true;
        }

        public async Task<bool> DeleteAsync(
            int id, 
            CancellationToken ct = default)
        {            
            var conferenceRoom = await _conferenceRoomRepository.GetByIdAsync(id, ct);

            if (conferenceRoom is null)
            {
                return false;
            }

            _conferenceRoomRepository.Remove(conferenceRoom);

            await _conferenceRoomRepository.SaveChangesAsync(ct);

            return true;
        }

    }
}
