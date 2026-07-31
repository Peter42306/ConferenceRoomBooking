using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRoomBooking.Domain.Entities
{
    public class ConferenceRoom
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty; 
        public int Capacity { get; set; } 
        public decimal RatePerHour { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
