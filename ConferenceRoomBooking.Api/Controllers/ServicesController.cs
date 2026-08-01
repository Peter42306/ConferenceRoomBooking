using ConferenceRoomBooking.Application.DTOs.Services;
using ConferenceRoomBooking.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.Api.Controllers
{
    [ApiController]
    [Route("api/services")]    
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _service;

        public ServicesController(IServiceService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(
            CreateServiceDto dto,
            CancellationToken ct)
        {
            var id = await _service.CreateAsync(dto, ct);
            
            return Ok(id);
        }
    }
}
