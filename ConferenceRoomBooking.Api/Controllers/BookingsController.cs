using ConferenceRoomBooking.Application.DTOs.Bookings;
using ConferenceRoomBooking.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.Api.Controllers
{
    [ApiController]
    [Route("api/bookings")]    
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingsController(IBookingService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<BookingResultDto>> Create(
            CreateBookingDto dto,
            CancellationToken ct)
        {
            var result = await _service.CreateAsync(dto, ct);

            return Ok(result);
        }
    }
}
