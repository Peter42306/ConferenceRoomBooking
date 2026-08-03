using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRoomBooking.Application.DTOs.Bookings
{
    public sealed record BookingServiceDto(
        int ServiceId,
        string Name,
        decimal Price);
}
