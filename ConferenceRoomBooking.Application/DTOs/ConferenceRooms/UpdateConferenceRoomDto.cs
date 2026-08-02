using System.ComponentModel.DataAnnotations;

namespace ConferenceRoomBooking.Application.DTOs.ConferenceRooms
{
    public sealed record UpdateConferenceRoomDto(
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        string Name,

        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than zero.")]
        int Capacity,

        [Range(0.01, double.MaxValue, ErrorMessage = "Rate per hour must be greater than zero.")]
        decimal RatePerHour,

        IReadOnlyCollection<int> ServiceIds);    
}
