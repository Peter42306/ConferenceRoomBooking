using ConferenceRoomBooking.Application.DTOs.Services;
using ConferenceRoomBooking.Application.Interfaces.Repositories;
using ConferenceRoomBooking.Application.Interfaces.Services;
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

        public async Task<bool> UpdateAsync(
            int id, 
            UpdateServiceDto dto, 
            CancellationToken ct = default)
        {
            var service = await _repository.GetByIdAsync(id, ct);

            if (service is null)
            {
                return false;
            }

            if (service.Name != dto.Name)
            {
                var serviceExists = await _repository.ExistsByNameAsync(
                    dto.Name,
                    ct);

                if (serviceExists)
                {
                    throw new InvalidOperationException(
                        $"Service with name '{dto.Name}' already exists.");
                }
            }

            service.Name = dto.Name;
            service.Price = dto.Price;

            await _repository.SaveChangesAsync(ct);

            return true;            
        }
    }
}
