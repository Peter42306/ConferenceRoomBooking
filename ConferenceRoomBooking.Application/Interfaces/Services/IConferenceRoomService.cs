using ConferenceRoomBooking.Application.DTOs.ConferenceRooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRoomBooking.Application.Interfaces.Services
{
    public interface IConferenceRoomService
    {
        Task<int> CreateAsync(
            CreateConferenceRoomDto dto,
            CancellationToken ct = default);
    }
}
