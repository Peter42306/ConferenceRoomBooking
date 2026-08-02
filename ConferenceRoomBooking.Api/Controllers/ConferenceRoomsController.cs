using ConferenceRoomBooking.Application.DTOs.ConferenceRooms;
using ConferenceRoomBooking.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.Api.Controllers
{
    [ApiController]
    [Route("api/conference-rooms")]    
    public class ConferenceRoomsController : ControllerBase
    {
        private readonly IConferenceRoomService _service;

        public ConferenceRoomsController(IConferenceRoomService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(
            CreateConferenceRoomDto dto,
            CancellationToken ct)
        {
            var id = await _service.CreateAsync(dto, ct);

            return Ok(id);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateConferenceRoomDto dto,
            CancellationToken ct)
        {
            var updated = await _service.UpdateAsync(id, dto, ct);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken ct)
        {
            var deleted = await _service.DeleteAsync(id, ct);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("search")]
        public async Task<ActionResult<IReadOnlyCollection<AvailableConferenceRoomDto>>> SearchAvailable(
            SearchAvailableConferenceRoomsDto dto,
            CancellationToken ct)
        {
            var rooms = await _service.SearchAvailableAsync(dto, ct);

            return Ok(rooms);
        }
    }    
}
