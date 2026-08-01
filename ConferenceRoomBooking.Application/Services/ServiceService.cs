using ConferenceRoomBooking.Application.DTOs.Services;
using ConferenceRoomBooking.Application.Interfaces.Repositories;
using ConferenceRoomBooking.Application.Interfaces.Services;
using ConferenceRoomBooking.Domain;
using ConferenceRoomBooking.Domain.Entities;

namespace ConferenceRoomBooking.Application.Services
{
    public sealed class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repository;

        public ServiceService(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(
            CreateServiceDto dto, 
            CancellationToken ct = default)
        {
            var serviceExists = await _repository.ExistsByNameAsync(
                dto.Name, 
                ct);

            if (serviceExists)
            {
                throw new InvalidOperationException(
                    $"Service with name '{dto.Name}' already exists.");
            }

            var service = new Service
            {
                Name = dto.Name,
                Price = dto.Price
            };

            await _repository.AddAsync(service, ct);
            await _repository.SaveChangesAsync(ct);

            return service.Id;
        }
    }
}
