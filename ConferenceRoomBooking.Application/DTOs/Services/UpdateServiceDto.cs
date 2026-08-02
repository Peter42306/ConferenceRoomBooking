using System.ComponentModel.DataAnnotations;

namespace ConferenceRoomBooking.Application.DTOs.Services
{
    public sealed record UpdateServiceDto(
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        string Name,
                
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        decimal Price);    
}
