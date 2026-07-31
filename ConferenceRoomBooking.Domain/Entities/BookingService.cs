using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRoomBooking.Domain.Entities
{
    // Represents a service selected for a specific booking and stores its price at the time of booking
    public class BookingService
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
