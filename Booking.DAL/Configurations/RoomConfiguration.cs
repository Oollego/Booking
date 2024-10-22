using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Configurations
{
    internal class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("rooms");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RoomName).HasMaxLength(254);
            builder.Property(x => x.Cancellation).HasDefaultValue(0).HasPrecision(10, 2);
            builder.Property(x => x.RoomQuantity).IsRequired();
            builder.Property(x => x.RoomPrice).HasPrecision(10, 2);
            builder.Property(x => x.HotelId).IsRequired();
           
            builder.HasMany<Book>(x => x.Books)
                .WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId)
                .HasPrincipalKey(x => x.Id);
            //builder.HasMany<BedType>(x => x.BedTypes)
            //    .WithMany(x => x.Rooms)
            //    .UsingEntity(x => x.ToTable("Beds"));
            //builder.HasMany<RoomFacilityType>(x => x.RoomFacilityTypes)
            //    .WithMany(x => x.Rooms)
            //    .UsingEntity(x => x.ToTable("RoomFacilities"));
            builder.HasMany<RoomImage>(x => x.RoomImages)
                .WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId)
                .HasPrincipalKey(x => x.Id);
            //builder.HasMany<RoomComfortIconType>(x => x.RoomComfortIcons)
            //    .WithOne(x => x.Room)
            //    .HasForeignKey(x => x.RoomId)
            //    .HasPrincipalKey(x => x.Id);

            builder.HasData(new List<Room>
        {
            new Room { Id = 1, RoomQuantity = 4, RoomName = "Single Room", RoomPrice = 1200.00M, Cancellation = 150.00M, HotelId = 1 },
            new Room { Id = 2, RoomQuantity = 2, RoomName = "Double Room", RoomPrice = 1800.00M, Cancellation = 0.00M, HotelId = 1 },
            new Room { Id = 3, RoomQuantity = 1, RoomName = "Suite", RoomPrice = 3000.00M, Cancellation = 0.00M, HotelId = 1 },

            new Room { Id = 4, RoomQuantity = 1, RoomName = "Deluxe Room", RoomPrice = 2200.00M, Cancellation = 0.00M, HotelId = 2 },
            new Room { Id = 5, RoomQuantity = 2, RoomName = "Family Room", RoomPrice = 2500.00M, Cancellation = 0.00M, HotelId = 2 },
                              
            new Room { Id = 6, RoomQuantity = 3, RoomName = "Standard Room", RoomPrice = 1500.00M, Cancellation = 100.00M, HotelId = 3 },
            new Room { Id = 7, RoomQuantity = 2, RoomName = "Executive Suite", RoomPrice = 3500.00M, Cancellation = 15.00M, HotelId = 3 },
                            
            new Room { Id = 8, RoomQuantity = 3, RoomName = "Mountain View Room", RoomPrice = 2000.00M, Cancellation = 200.00M, HotelId = 4 },
            new Room { Id = 9, RoomQuantity = 2, RoomName = "Ski Suite", RoomPrice = 2800.00M, Cancellation = 0.00M, HotelId = 4 },
                              
            new Room { Id = 10, RoomQuantity = 2, RoomName = "Oceanfront Room", RoomPrice = 2400.00M, Cancellation = 0.00M, HotelId = 5 },
            new Room { Id = 11, RoomQuantity = 1, RoomName = "Luxury Suite", RoomPrice = 3500.00M, Cancellation = 0.00M, HotelId = 5 },
                              
            new Room { Id = 12, RoomQuantity = 4, RoomName = "Classic Room", RoomPrice = 1300.00M, Cancellation = 150.00M, HotelId = 6 },
            new Room { Id = 13, RoomQuantity = 4, RoomName = "Royal Suite", RoomPrice = 3000.00M, Cancellation = 0.00M, HotelId = 6 },
                              
            new Room { Id = 14, RoomQuantity = 2, RoomName = "River View Room", RoomPrice = 1600.00M, Cancellation = 100.00M, HotelId = 7 },
            new Room { Id = 15, RoomQuantity = 4, RoomName = "Deluxe River Suite", RoomPrice = 2700.00M, Cancellation = 0.00M, HotelId = 7 },
                              
            new Room { Id = 16, RoomQuantity = 2, RoomName = "Park View Room", RoomPrice = 1900.00M, Cancellation = 0.00M, HotelId = 8 },
            new Room { Id = 17, RoomQuantity = 1, RoomName = "Luxury Park Suite", RoomPrice = 3200.00M, Cancellation = 0.00M, HotelId = 8 },
                              
            new Room { Id = 18, RoomQuantity = 4, RoomName = "Beachfront Room", RoomPrice = 2000.00M, Cancellation = 120.00M, HotelId = 9 },
            new Room { Id = 19, RoomQuantity = 2, RoomName = "Seaside Suite", RoomPrice = 3000.00M, Cancellation = 120.00M, HotelId = 9 },
                               
            new Room { Id = 20, RoomQuantity = 3, RoomName = "Grand Room", RoomPrice = 2500.00M, Cancellation = 0.00M, HotelId = 10 },
            new Room { Id = 21, RoomQuantity = 2, RoomName = "Historic Suite", RoomPrice = 4000.00M, Cancellation = 0.00M, HotelId = 10 },
                             
            new Room { Id = 22, RoomQuantity = 5, RoomName = "City View Room", RoomPrice = 1400.00M, Cancellation = 0.00M, HotelId = 11 },
            new Room { Id = 23, RoomQuantity = 2, RoomName = "Palace Suite", RoomPrice = 2900.00M, Cancellation = 250.00M, HotelId = 11 },
                              
            new Room { Id = 24, RoomQuantity = 2, RoomName = "Boutique Room", RoomPrice = 1800.00M, Cancellation = 140.00M, HotelId = 12 },
            new Room { Id = 25, RoomQuantity = 4, RoomName = "Gran Via Suite", RoomPrice = 3000.00M, Cancellation = 0.00M, HotelId = 12 },
                               
            new Room { Id = 26, RoomQuantity = 1, RoomName = "Lakeview Room", RoomPrice = 1700.00M, Cancellation = 0.00M, HotelId = 13 },
            new Room { Id = 27, RoomQuantity = 4, RoomName = "Lakefront Suite", RoomPrice = 2900.00M, Cancellation = 150.00M, HotelId = 13 },
                               
            new Room { Id = 28, RoomQuantity = 3, RoomName = "Ocean Breeze Room", RoomPrice = 2000.00M, Cancellation = 100.00M, HotelId = 14 },
            new Room { Id = 29, RoomQuantity = 3, RoomName = "Business Suite", RoomPrice = 3200.00M, Cancellation = 0.00M, HotelId = 14 },
                               
            new Room { Id = 30, RoomQuantity = 2, RoomName = "Countryside Room", RoomPrice = 1500.00M, Cancellation = 0.00M, HotelId = 15 },
            new Room { Id = 31, RoomQuantity = 2, RoomName = "Rustic Suite", RoomPrice = 2600.00M, Cancellation = 0.00M, HotelId = 15 },
        });
        }
    }
}
 
