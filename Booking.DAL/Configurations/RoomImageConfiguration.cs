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
            new RoomImage { Id = 1, ImageName = "210104412.jpg", RoomId = 1 },
            new RoomImage { Id = 2, ImageName = "389389244.jpg", RoomId = 1 },
            new RoomImage { Id = 3, ImageName = "210058370.jpg", RoomId = 1 },

            // Photos for Double Room (Id = 2)
            new RoomImage { Id = 4, ImageName = "531031584.jpg", RoomId = 2 },
            new RoomImage { Id = 5, ImageName = "210216470.jpg", RoomId = 2 },
            new RoomImage { Id = 6, ImageName = "210214000.jpg", RoomId = 2 },

            // Photos for Suite (Id = 3)
            new RoomImage { Id = 7, ImageName = "210109848.jpg", RoomId = 3 },
            new RoomImage { Id = 8, ImageName = "210105329.jpg", RoomId = 3 },
            new RoomImage { Id = 9, ImageName = "12687426.jpg", RoomId = 3 },

            // Photos for Deluxe Room (Id = 4)
            new RoomImage { Id = 10, ImageName = "480093531.jpg", RoomId = 4 },
            new RoomImage { Id = 11, ImageName = "431186385.jpg", RoomId = 4 },
            new RoomImage { Id = 12, ImageName = "431187876.jpg", RoomId = 4 },

            // Photos for Family Room (Id = 5)
            new RoomImage { Id = 13, ImageName = "431188274.jpg", RoomId = 5 },
            new RoomImage { Id = 14, ImageName = "480093539.jpg", RoomId = 5 },
            new RoomImage { Id = 15, ImageName = "534790264.jpg", RoomId = 5 },

            // Photos for Standard Room (Id = 6)
            new RoomImage { Id = 16, ImageName = "534790860.jpg", RoomId = 6 },
            new RoomImage { Id = 17, ImageName = "431180646.jpg", RoomId = 6 },
            new RoomImage { Id = 18, ImageName = "534790634.jpg", RoomId = 6 },

            // Photos for Executive Suite (Id = 7)
            new RoomImage { Id = 19, ImageName = "463083660.jpg", RoomId = 7 },
            new RoomImage { Id = 20, ImageName = "257699424.jpg", RoomId = 7 },
            new RoomImage { Id = 21, ImageName = "233998172.jpg", RoomId = 7 },

            // Photos for Mountain View Room (Id = 8)
            new RoomImage { Id = 22, ImageName = "233777816.jpg", RoomId = 8 },
            new RoomImage { Id = 23, ImageName = "245890737.jpg", RoomId = 8 },
            new RoomImage { Id = 24, ImageName = "252578346.jpg", RoomId = 8 },

            // Photos for Ski Suite (Id = 9)
            new RoomImage { Id = 25, ImageName = "233777798.jpg", RoomId = 9 },
            new RoomImage { Id = 26, ImageName = "233777868.jpg", RoomId = 9 },
            new RoomImage { Id = 27, ImageName = "233777783.jpg", RoomId = 9 },

            // Photos for Oceanfront Room (Id = 10)
            new RoomImage { Id = 28, ImageName = "170874260.jpg", RoomId = 10 },
            new RoomImage { Id = 29, ImageName = "158232442.jpg", RoomId = 10 },
            new RoomImage { Id = 30, ImageName = "120415775.jpg", RoomId = 10 },

            // Photos for Luxury Suite (Id = 11)
            new RoomImage { Id = 31, ImageName = "120412627.jpg", RoomId = 11 },
            new RoomImage { Id = 32, ImageName = "170873817.jpg", RoomId = 11 },
            new RoomImage { Id = 33, ImageName = "158244132.jpg", RoomId = 11 },

            // Photos for Grand Room (Id = 12)
            new RoomImage { Id = 34, ImageName = "158244608.jpg", RoomId = 12 },
            new RoomImage { Id = 35, ImageName = "216077342.jpg", RoomId = 12 },
            new RoomImage { Id = 36, ImageName = "158227246.jpg", RoomId = 12 },

            // Photos for Historic Suite (Id = 13)
            new RoomImage { Id = 37, ImageName = "581982230.jpg", RoomId = 13 },
            new RoomImage { Id = 38, ImageName = "521690205.jpg", RoomId = 13 },
            new RoomImage { Id = 39, ImageName = "65822867.jpg",  RoomId = 13 },

            // Photos for City View Room (Id = 14)
            new RoomImage { Id = 40, ImageName = "317277974.jpg", RoomId = 14 },
            new RoomImage { Id = 41, ImageName = "520815355.jpg", RoomId = 14 },
            new RoomImage { Id = 42, ImageName = "182754259.jpg", RoomId = 14 },

            new RoomImage { Id = 43, ImageName = "81856512.jpg",  RoomId = 15 },
            new RoomImage { Id = 44, ImageName = "182753071.jpg", RoomId = 15 },
            new RoomImage { Id = 45, ImageName = "520815372.jpg", RoomId = 15 },

            new RoomImage { Id = 46, ImageName = "341936743.jpg", RoomId = 16 },
            new RoomImage { Id = 47, ImageName = "399559598.jpg", RoomId = 16 },
            new RoomImage { Id = 48, ImageName = "374514783.jpg", RoomId = 16 },

            new RoomImage { Id = 49, ImageName = "341938138.jpg", RoomId = 17 },
            new RoomImage { Id = 50, ImageName = "595168488.jpg", RoomId = 17 },
            new RoomImage { Id = 51, ImageName = "341938123.jpg", RoomId = 17 },

            new RoomImage { Id = 52, ImageName = "341938129.jpg", RoomId = 18 },
            new RoomImage { Id = 53, ImageName = "353666305.jpg", RoomId = 18 },
            new RoomImage { Id = 54, ImageName = "374514791.jpg", RoomId = 18 },

            new RoomImage { Id = 55, ImageName = "438054500.jpg", RoomId = 19 },
            new RoomImage { Id = 56, ImageName = "438054487.jpg", RoomId = 19 },
            new RoomImage { Id = 57, ImageName = "438054499.jpg", RoomId = 19 },

            new RoomImage { Id = 58, ImageName = "415932921.jpg", RoomId = 20 },
            new RoomImage { Id = 59, ImageName = "438054489.jpg", RoomId = 20 },
            new RoomImage { Id = 60, ImageName = "250659784.jpg", RoomId = 20 },

            new RoomImage { Id = 61, ImageName = "415932912.jpg", RoomId = 21 },
            new RoomImage { Id = 62, ImageName = "438058113.jpg", RoomId = 21 },
            new RoomImage { Id = 63, ImageName = "378321159.jpg", RoomId = 21 },

            new RoomImage { Id = 64, ImageName = "370611422.jpg", RoomId = 22 },
            new RoomImage { Id = 65, ImageName = "370611499.jpg", RoomId = 22 },
            new RoomImage { Id = 66, ImageName = "370611483.jpg", RoomId = 22 },

            new RoomImage { Id = 67, ImageName = "370611385.jpg", RoomId = 23 },
            new RoomImage { Id = 68, ImageName = "370611395.jpg", RoomId = 23 },
            new RoomImage { Id = 69, ImageName = "370611454.jpg", RoomId = 23 },

            new RoomImage { Id = 70, ImageName = "250714430.jpg", RoomId = 24 },
            new RoomImage { Id = 71, ImageName = "253842761.jpg", RoomId = 24 },
            new RoomImage { Id = 72, ImageName = "132970545.jpg", RoomId = 24 },

            new RoomImage { Id = 73, ImageName = "250715273.jpg", RoomId = 25 },
            new RoomImage { Id = 74, ImageName = "250713187.jpg", RoomId = 25 },
            new RoomImage { Id = 75, ImageName = "132970495.jpg", RoomId = 25 },

            new RoomImage { Id = 76, ImageName = "514674707.jpg", RoomId = 26 },
            new RoomImage { Id = 77, ImageName = "250713903.jpg", RoomId = 26 },
            new RoomImage { Id = 78, ImageName = "253841744.jpg", RoomId = 26 },

            new RoomImage { Id = 79, ImageName = "580039327.jpg", RoomId = 27 },
            new RoomImage { Id = 80, ImageName = "580039457.jpg", RoomId = 27 },
            new RoomImage { Id = 81, ImageName = "580039326.jpg", RoomId = 27 },

            new RoomImage { Id = 82, ImageName = "580039343.jpg", RoomId = 28 },
            new RoomImage { Id = 83, ImageName = "581895260.jpg", RoomId = 28 },
            new RoomImage { Id = 84, ImageName = "580039360.jpg", RoomId = 28 },

            new RoomImage { Id = 85, ImageName = "581895264.jpg", RoomId = 29 },
            new RoomImage { Id = 86, ImageName = "581895268.jpg", RoomId = 29 },
            new RoomImage { Id = 87, ImageName = "581895266.jpg", RoomId = 29 },

            new RoomImage { Id = 88, ImageName = "463103171.jpg", RoomId = 30 },
            new RoomImage { Id = 89, ImageName = "119014811.jpg", RoomId = 30 },
            new RoomImage { Id = 90, ImageName = "180159228.jpg", RoomId = 30 },

            new RoomImage { Id = 91, ImageName = "346951934.jpg", RoomId = 31 },
            new RoomImage { Id = 92, ImageName = "346951988.jpg", RoomId = 31 },
            new RoomImage { Id = 93, ImageName = "334747217.jpg", RoomId = 31 },
        });
        }
    }
}
