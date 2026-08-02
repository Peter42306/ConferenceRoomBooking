using System.ComponentModel.DataAnnotations;

namespace ConferenceRoomBooking.Application.DTOs.ConferenceRooms
{
    public sealed record SearchAvailableConferenceRoomsDto(
        [Required(ErrorMessage = "Start time is required.")]
        DateTime StartTime,

        [Required(ErrorMessage = "End time is required.")]
        DateTime EndTime,

        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than zero.")]
        int Capacity);
}
