using ConferenceRoomBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRoomBooking.Infrastructure.Data.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.RentalPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.ServicesPrice)
                .HasPrecision(18, 2);

            builder.Ignore(x => x.TotalPrice);

            builder.HasOne(x => x.ConferenceRoom)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.ConferenceRoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
