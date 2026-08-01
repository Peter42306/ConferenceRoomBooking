using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRoomBooking.Application.DTOs.Services
{
    public sealed record UpdateServiceDto(
        string Name,
        decimal Price);    
}
