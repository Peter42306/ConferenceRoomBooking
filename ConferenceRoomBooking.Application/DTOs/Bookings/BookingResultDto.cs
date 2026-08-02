namespace ConferenceRoomBooking.Application.DTOs.Bookings
{
    public sealed record BookingResultDto(
        int BookingId,
        decimal RentalPrice,
        decimal ServicesPrice,
        decimal TotalPrice);
}
