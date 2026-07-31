using ConferenceRoomBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRoomBooking.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        public int ConferenceRoomId { get; set; }
        public ConferenceRoom ConferenceRoom { get; set; } = null!;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public BookingStatus Status { get; set; }

        public decimal RentalPrice { get; set; }
        public decimal ServicesPrice { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return RentalPrice + ServicesPrice;
            }
        }

        public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
    }
}
