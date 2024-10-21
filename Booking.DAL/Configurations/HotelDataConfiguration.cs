using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Configurations
{
    internal class HotelDataConfiguration : IEntityTypeConfiguration<HotelData>
    {
        void IEntityTypeConfiguration<HotelData>.Configure(EntityTypeBuilder<HotelData> builder)
        {
            builder.ToTable("hotelData");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.HotelId).ValueGeneratedOnAdd();
            builder.Property(x => x.ReviewCount).HasDefaultValue(0);
            builder.Property(x => x.Rating).HasDefaultValue(0);
            builder.Property(x => x.HotelMinRoomPrice).HasDefaultValue(0);

            builder.HasOne<Hotel>(x => x.Hotel)
                .WithOne(x => x.HotelData)
                .HasForeignKey<HotelData>(x => x.HotelId);

            builder.HasData(new List<HotelData>
            {
                new HotelData{Id = 1,  HotelId = 1, ReviewCount = 9, HotelMinRoomPrice = 1200, HotelMaxRoomPrice = 3000, Rating = 9.1 },
                new HotelData{Id = 2,  HotelId = 2, ReviewCount = 7, HotelMinRoomPrice = 2200, HotelMaxRoomPrice = 2500, Rating = 7.6 },
                new HotelData{Id = 3,  HotelId = 3, ReviewCount = 7, HotelMinRoomPrice = 1500, HotelMaxRoomPrice = 3500, Rating = 8.1 },
                new HotelData{Id = 4,  HotelId = 4, ReviewCount = 5, HotelMinRoomPrice = 2000, HotelMaxRoomPrice = 2800, Rating = 8.4 },
                new HotelData{Id = 5,  HotelId = 5, ReviewCount = 5, HotelMinRoomPrice = 2400, HotelMaxRoomPrice = 3500, Rating = 8 },
                new HotelData{Id = 6,  HotelId = 6, ReviewCount = 2, HotelMinRoomPrice = 1300, HotelMaxRoomPrice = 3000, Rating = 7.7 },
                new HotelData{Id = 7,  HotelId = 7, ReviewCount = 1, HotelMinRoomPrice = 1600, HotelMaxRoomPrice = 2700, Rating = 9 },
                new HotelData{Id = 8,  HotelId = 8, ReviewCount = 2, HotelMinRoomPrice = 1900, HotelMaxRoomPrice = 3200, Rating = 8.5 },
                new HotelData{Id = 9,  HotelId = 9, ReviewCount = 1, HotelMinRoomPrice = 2000, HotelMaxRoomPrice = 3000, Rating = 7 },
                new HotelData{Id = 10, HotelId = 10, ReviewCount = 2, HotelMinRoomPrice = 2500, HotelMaxRoomPrice = 4000, Rating = 8.3 },
                new HotelData{Id = 11, HotelId = 11, ReviewCount = 1, HotelMinRoomPrice = 1400, HotelMaxRoomPrice = 2900, Rating = 10 },
                new HotelData{Id = 12, HotelId = 12, ReviewCount = 1, HotelMinRoomPrice = 1800, HotelMaxRoomPrice = 3000, Rating = 9 },
                new HotelData{Id = 13, HotelId = 13, ReviewCount = 1, HotelMinRoomPrice = 1700, HotelMaxRoomPrice = 2900, Rating = 10 },
                new HotelData{Id = 14, HotelId = 14, ReviewCount = 1, HotelMinRoomPrice = 2000, HotelMaxRoomPrice = 3200, Rating = 8 },
                new HotelData{Id = 15, HotelId = 15, ReviewCount = 1, HotelMinRoomPrice = 1500, HotelMaxRoomPrice = 2600, Rating = 7.5 },
            });
        }
    }
}
