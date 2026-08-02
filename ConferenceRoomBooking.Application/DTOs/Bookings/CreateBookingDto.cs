using System.ComponentModel.DataAnnotations;

namespace ConferenceRoomBooking.Application.DTOs.Bookings
{
    public sealed record CreateBookingDto(
         [Range(1, int.MaxValue, ErrorMessage = "Conference room id must be greater than zero.")]
        int ConferenceRoomId,

        [Required(ErrorMessage = "Start time is required.")]
        DateTime StartTime,

        [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than zero.")]
        int DurationHours,

        IReadOnlyCollection<int> ServiceIds);
}
