using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Configurations
{
    internal class RoomImageConfiguration: IEntityTypeConfiguration<RoomImage>
    {
        public void Configure(EntityTypeBuilder<RoomImage> builder)
        {
            builder.ToTable("room_images");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ImageName).IsRequired().HasMaxLength(254);
            builder.Property(x => x.RoomId).IsRequired();

            builder.HasData(new List<RoomImage>
        {
            // Photos for Single Room (Id = 1)
            new RoomImage { Id = 1, ImageName = "578237709.jpg", RoomId = 1 },
            new RoomImage { Id = 2, ImageName = "215800733.jpg", RoomId = 1 },
            new RoomImage { Id = 3, ImageName = "215800969.jpg", RoomId = 1 },

            // Photos for Double Room (Id = 2)
            new RoomImage { Id = 4, ImageName = "215800314.jpg", RoomId = 2 },
            new RoomImage { Id = 5, ImageName = "110147907.jpg", RoomId = 2 },
            new RoomImage { Id = 6, ImageName = "215802543.jpg", RoomId = 2 },

            // Photos for Suite (Id = 3)
            new RoomImage { Id = 7, ImageName = "401492281.jpg", RoomId = 3 },
            new RoomImage { Id = 8, ImageName = "167218141.jpg", RoomId = 3 },
            new RoomImage { Id = 9, ImageName = "215802499.jpg", RoomId = 3 },

            // Photos for Deluxe Room (Id = 4)
            new RoomImage { Id = 10, ImageName = "215800752.jpg", RoomId = 4 },
            new RoomImage { Id = 11, ImageName = "372185637.jpg", RoomId = 4 },
            new RoomImage { Id = 12, ImageName = "371580726.jpg", RoomId = 4 },

            // Photos for Family Room (Id = 5)
            new RoomImage { Id = 13, ImageName = "110147742.jpg", RoomId = 5 },
            new RoomImage { Id = 14, ImageName = "386343575.jpg", RoomId = 5 },
            new RoomImage { Id = 15, ImageName = "246926985.jpg", RoomId = 5 },

            // Photos for Standard Room (Id = 6)
            new RoomImage { Id = 16, ImageName = "245006813.jpg", RoomId = 6 },
            new RoomImage { Id = 17, ImageName = "245006010.jpg", RoomId = 6 },
            new RoomImage { Id = 18, ImageName = "245006824.jpg", RoomId = 6 },

            // Photos for Executive Suite (Id = 7)
            new RoomImage { Id = 19, ImageName = "245006830.jpg", RoomId = 7 },
            new RoomImage { Id = 20, ImageName = "245021477.jpg", RoomId = 7 },
            new RoomImage { Id = 21, ImageName = "245006785.jpg", RoomId = 7 },

            // Photos for Mountain View Room (Id = 8)
            new RoomImage { Id = 22, ImageName = "245021511.jpg", RoomId = 8 },
            new RoomImage { Id = 23, ImageName = "333511081.jpg", RoomId = 8 },
            new RoomImage { Id = 24, ImageName = "304574142.jpg", RoomId = 8 },

            // Photos for Ski Suite (Id = 9)
            new RoomImage { Id = 25, ImageName = "455699363.jpg", RoomId = 9 },
            new RoomImage { Id = 26, ImageName = "70567712.jpg", RoomId = 9 },
            new RoomImage { Id = 27, ImageName = "467565298.jpg", RoomId = 9 },

            // Photos for Oceanfront Room (Id = 10)
            new RoomImage { Id = 28, ImageName = "467565311.jpg", RoomId = 10 },
            new RoomImage { Id = 29, ImageName = "177737946.jpg", RoomId = 10 },
            new RoomImage { Id = 30, ImageName = "588813242.jpg", RoomId = 10 },

            // Photos for Luxury Suite (Id = 11)
            new RoomImage { Id = 31, ImageName = "588813265.jpg", RoomId = 11 },
            new RoomImage { Id = 32, ImageName = "588813256.jpg", RoomId = 11 },
            new RoomImage { Id = 33, ImageName = "588813289.jpg", RoomId = 11 },

            // Photos for Grand Room (Id = 20)
            new RoomImage { Id = 34, ImageName = "268368565.jpg", RoomId = 20 },
            new RoomImage { Id = 35, ImageName = "268368556.jpg", RoomId = 20 },
            new RoomImage { Id = 36, ImageName = "212051436.jpg", RoomId = 20 },

            // Photos for Historic Suite (Id = 21)
            new RoomImage { Id = 37, ImageName = "212050872.jpg", RoomId = 21 },
            new RoomImage { Id = 38, ImageName = "212051275.jpg", RoomId = 21 },
            new RoomImage { Id = 39, ImageName = "99356031.jpg", RoomId = 21 },

            // Photos for City View Room (Id = 22)
            new RoomImage { Id = 40, ImageName = "290857715.jpg", RoomId = 22 },
            new RoomImage { Id = 41, ImageName = "164172199.jpg", RoomId = 22 },
            new RoomImage { Id = 42, ImageName = "61405250.jpg", RoomId = 22 },

            // Photos for Palace Suite (Id = 23)
            new RoomImage { Id = 43, ImageName = "10809767.jpg", RoomId = 23 },
            new RoomImage { Id = 44, ImageName = "578710641.jpg", RoomId = 23 },
            new RoomImage { Id = 45, ImageName = "339032030.jpg", RoomId = 23 },

            // Photos for Boutique Room (Id = 24)
            new RoomImage { Id = 46, ImageName = "56881927.jpg", RoomId = 24 },
            new RoomImage { Id = 47, ImageName = "96302217.jpg", RoomId = 24 },
            new RoomImage { Id = 48, ImageName = "339032034.jpg", RoomId = 24 },

            // Photos for Gran Via Suite (Id = 25)
            new RoomImage { Id = 49, ImageName = "339032086.jpg", RoomId = 25 },
            new RoomImage { Id = 50, ImageName = "431757119.jpg", RoomId = 25 },
            new RoomImage { Id = 51, ImageName = "432014924.jpg", RoomId = 25 },

            // Photos for Lakeview Room (Id = 26)
            new RoomImage { Id = 52, ImageName = "432014924.jpg", RoomId = 26 },
            new RoomImage { Id = 53, ImageName = "81091432.jpg", RoomId = 26 },
            new RoomImage { Id = 54, ImageName = "81091427.jpg", RoomId = 26 },

            // Photos for Lakefront Suite (Id = 27)
            new RoomImage { Id = 55, ImageName = "354322700.jpg", RoomId = 27 },
            new RoomImage { Id = 56, ImageName = "354666698.jpg", RoomId = 27 },
            new RoomImage { Id = 57, ImageName = "354322689.jpg", RoomId = 27 },

            // Photos for Ocean Breeze Room (Id = 28)
            new RoomImage { Id = 58, ImageName = "241134673.jpg", RoomId = 28 },
            new RoomImage { Id = 59, ImageName = "354322694.jpg", RoomId = 28 },
            new RoomImage { Id = 60, ImageName = "241134815.jpg", RoomId = 28 },

            // Photos for Business Suite (Id = 29)
            new RoomImage { Id = 61, ImageName = "354666694.jpg", RoomId = 29 },
            new RoomImage { Id = 62, ImageName = "354666691.jpg", RoomId = 29 },
            new RoomImage { Id = 63, ImageName = "354668277.jpg", RoomId = 29 },

            // Photos for Countryside Room (Id = 30)
            new RoomImage { Id = 64, ImageName = "88036291.jpg", RoomId = 30 },
            new RoomImage { Id = 65, ImageName = "484533046.jpg", RoomId = 30 },
            new RoomImage { Id = 66, ImageName = "484519440.jpg", RoomId = 30 },

            // Photos for Rustic Suite (Id = 31)
            new RoomImage { Id = 67, ImageName = "126696551.jpg", RoomId = 31 },
            new RoomImage { Id = 68, ImageName = "94457992.jpg", RoomId = 31 },
            new RoomImage { Id = 69, ImageName = "96661618.jpg", RoomId = 31 }
        });
        }
    }
}
