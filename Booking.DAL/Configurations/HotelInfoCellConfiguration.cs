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
    internal class HotelInfoCellConfiguration : IEntityTypeConfiguration<HotelInfoCell>
    {
        public void Configure(EntityTypeBuilder<HotelInfoCell> builder)
        {
            builder.ToTable("hotel_info_cells");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TextLine_1).HasMaxLength(254).IsRequired();
            builder.Property(x => x.TextLine_2).HasMaxLength(254).HasDefaultValue("");
            builder.Property(x => x.HotelId).IsRequired();

            builder.HasData(new List<HotelInfoCell> 
            {
                new HotelInfoCell()
                {
                    Id = 1,
                    TextLine_1 = "Check-in time from",
                    TextLine_2 = "15:00 to 23:00",
                    HotelId = 1,
                    InfoIconId = 1
                },
                 new HotelInfoCell()
                {
                    Id = 2,
                    TextLine_1 = "Maximum number of",
                    TextLine_2 = "extra beds 1",
                    HotelId = 1,
                    InfoIconId = 2
                },
                 new HotelInfoCell()
                {
                    Id = 3,
                    TextLine_1 = "City center 45 m",
                    TextLine_2 = "",
                    HotelId = 1,
                    InfoIconId = 3
                },
                 new HotelInfoCell()
                {
                    Id = 4,
                    TextLine_1 = "Train station 356 m",
                    TextLine_2 = "Airport 543 m",
                    HotelId = 1,
                    InfoIconId = 4
                },
                 new HotelInfoCell()
                {
                    Id = 5,
                    TextLine_1 = "Pets are allowed",
                    TextLine_2 = "No extra charge",
                    HotelId = 1,
                    InfoIconId = 4
                },
                 new HotelInfoCell()
                {
                    Id = 6,
                    TextLine_1 = "Check-in time from",
                    TextLine_2 = "15:00 to 23:00",
                    HotelId = 2,
                    InfoIconId = 1
                },
                new HotelInfoCell()
                {
                    Id = 7,
                    TextLine_1 = "Maximum number of",
                    TextLine_2 = "extra beds 2",
                    HotelId = 2,
                    InfoIconId = 2
                },
                new HotelInfoCell()
                {
                    Id = 8,
                    TextLine_1 = "City center 100 m",
                    TextLine_2 = "",
                    HotelId = 2,
                    InfoIconId = 3
                },
                new HotelInfoCell()
                {
                    Id = 9,
                    TextLine_1 = "Train station 200 m",
                    TextLine_2 = "Airport 300 m",
                    HotelId = 2,
                    InfoIconId = 4
                },
                new HotelInfoCell()
                {
                    Id = 10,
                    TextLine_1 = "Pets are allowed",
                    TextLine_2 = "No extra charge",
                    HotelId = 2,
                    InfoIconId = 5
                },

                new HotelInfoCell()
                {
                    Id = 11,
                    TextLine_1 = "Check-in time from",
                    TextLine_2 = "16:00 to 23:00",
                    HotelId = 3,
                    InfoIconId = 1
                },
                new HotelInfoCell()
                {
                    Id = 12,
                    TextLine_1 = "Maximum number of",
                    TextLine_2 = "extra beds 2",
                    HotelId = 3,
                    InfoIconId = 2
                },
                new HotelInfoCell()
                {
                    Id = 13,
                    TextLine_1 = "City center 200 m",
                    TextLine_2 = "",
                    HotelId = 3,
                    InfoIconId = 3
                },
                new HotelInfoCell()
                {
                    Id = 14,
                    TextLine_1 = "Train station 150 m",
                    TextLine_2 = "Airport 400 m",
                    HotelId = 3,
                    InfoIconId = 4
                },
                new HotelInfoCell()
                {
                    Id = 15,
                    TextLine_1 = "Pets are allowed",
                    TextLine_2 = "No extra charge",
                    HotelId = 3,
                    InfoIconId = 5
                },
                new HotelInfoCell()
                {
                    Id = 16,
                    TextLine_1 = "Check-in time from",
                    TextLine_2 = "14:00 to 23:00",
                    HotelId = 4,
                    InfoIconId = 1
                },
                new HotelInfoCell()
                {
                    Id = 17,
                    TextLine_1 = "Maximum number of",
                    TextLine_2 = "extra beds 1",
                    HotelId = 4,
                    InfoIconId = 2
                },
                new HotelInfoCell()
                {
                    Id = 18,
                    TextLine_1 = "City center 300 m",
                    TextLine_2 = "",
                    HotelId = 4,
                    InfoIconId = 3
                },
                new HotelInfoCell()
                {
                    Id = 19,
                    TextLine_1 = "Train station 500 m",
                    TextLine_2 = "Airport 600 m",
                    HotelId = 4,
                    InfoIconId = 4
                },
                new HotelInfoCell()
                {
                    Id = 20,
                    TextLine_1 = "Pets are allowed",
                    TextLine_2 = "No extra charge",
                    HotelId = 4,
                    InfoIconId = 5
                },
                new HotelInfoCell()
                {
                    Id = 21,
                    TextLine_1 = "Check-in time from",
                    TextLine_2 = "14:00 to 22:00",
                    HotelId = 5,
                    InfoIconId = 1
                },
                new HotelInfoCell()
                {
                    Id = 22,
                    TextLine_1 = "Maximum number of",
                    TextLine_2 = "extra beds 2",
                    HotelId = 5,
                    InfoIconId = 2
                },
                new HotelInfoCell()
                {
                    Id = 23,
                    TextLine_1 = "City center 250 m",
                    TextLine_2 = "",
                    HotelId = 5,
                    InfoIconId = 3
                },
                new HotelInfoCell()
                {
                    Id = 24,
                    TextLine_1 = "Train station 350 m",
                    TextLine_2 = "Airport 700 m",
                    HotelId = 5,
                    InfoIconId = 4
                },
                new HotelInfoCell()
                {
                    Id = 25,
                    TextLine_1 = "Pets are allowed",
                    TextLine_2 = "No extra charge",
                    HotelId = 5,
                    InfoIconId = 5
                },

                new HotelInfoCell()
                {
                    Id = 26,
                    TextLine_1 = "Check-in time from",
                    TextLine_2 = "16:00 to 24:00",
                    HotelId = 6,
                    InfoIconId = 1
                },
                new HotelInfoCell()
                {
                    Id = 27,
                    TextLine_1 = "Maximum number of",
                    TextLine_2 = "extra beds 3",
                    HotelId = 6,
                    InfoIconId = 2
                },
                new HotelInfoCell()
                {
                    Id = 28,
                    TextLine_1 = "City center 350 m",
                    TextLine_2 = "",
                    HotelId = 6,
                    InfoIconId = 3
                },
                new HotelInfoCell()
                {
                    Id = 29,
                    TextLine_1 = "Train station 500 m",
                    TextLine_2 = "Airport 800 m",
                    HotelId = 6,
                    InfoIconId = 4
                },
                new HotelInfoCell()
                {
                    Id = 30,
                    TextLine_1 = "Pets are allowed",
                    TextLine_2 = "No extra charge",
                    HotelId = 6,
                    InfoIconId = 5
                },

                new HotelInfoCell()
                {
                    Id = 31,
                    TextLine_1 = "Check-in time from",
                    TextLine_2 = "12:00 to 22:00",
                    HotelId = 7,
                    InfoIconId = 1
                },
                new HotelInfoCell()
                {
                    Id = 32,
                    TextLine_1 = "Maximum number of",
                    TextLine_2 = "extra beds 4",
                    HotelId = 7,
                    InfoIconId = 2
                },
                new HotelInfoCell()
                {
                    Id = 33,
                    TextLine_1 = "City center 450 m",
                    TextLine_2 = "",
                    HotelId = 7,
                    InfoIconId = 3
                },
                new HotelInfoCell()
                {
                    Id = 34,
                    TextLine_1 = "Train station 600 m",
                    TextLine_2 = "Airport 900 m",
                    HotelId = 7,
                    InfoIconId = 4
                },
                new HotelInfoCell()
                {
                    Id = 35,
                    TextLine_1 = "Pets are allowed",
                    TextLine_2 = "No extra charge",
                    HotelId = 7,
                    InfoIconId = 5
                },

                new HotelInfoCell()
                {
                    Id = 36,
                    TextLine_1 = "Check-in time from",
                    TextLine_2 = "13:00 to 23:00",
                    HotelId = 8,
                    InfoIconId = 1
                },
                new HotelInfoCell()
                {
                    Id = 37,
                    TextLine_1 = "Maximum number of",
                    TextLine_2 = "extra beds 1",
                    HotelId = 8,
                    InfoIconId = 2
                },
                new HotelInfoCell()
                {
                    Id = 38,
                    TextLine_1 = "City center 550 m",
                    TextLine_2 = "",
                    HotelId = 8,
                    InfoIconId = 3
                },
                new HotelInfoCell()
                {
                    Id = 39,
                    TextLine_1 = "Train station 450 m",
                    TextLine_2 = "Airport 750 m",
                    HotelId = 8,
                    InfoIconId = 4
                },
                new HotelInfoCell()
                {
                    Id = 40,
                    TextLine_1 = "Pets are allowed",
                    TextLine_2 = "No extra charge",
                    HotelId = 8,
                    InfoIconId = 5
                },

                new HotelInfoCell()
                {
                    Id = 41,
                    TextLine_1 = "Check-in time from",
                    TextLine_2 = "11:00 to 21:00",
                    HotelId = 9,
                    InfoIconId = 1
                },
                new HotelInfoCell()
                {
                    Id = 42,
                    TextLine_1 = "Maximum number of",
                    TextLine_2 = "extra beds 2",
                    HotelId = 9,
                    InfoIconId = 2
                },
                new HotelInfoCell()
                {
                    Id = 43,
                    TextLine_1 = "City center 650 m",
                    TextLine_2 = "",
                    HotelId = 9,
                    InfoIconId = 3
                },
                new HotelInfoCell()
                {
                    Id = 44,
                    TextLine_1 = "Train station 550 m",
                    TextLine_2 = "Airport 850 m",
                    HotelId = 9,
                    InfoIconId = 4
                },
                new HotelInfoCell()
                {
                    Id = 45,
                    TextLine_1 = "Pets are allowed",
                    TextLine_2 = "No extra charge",
                    HotelId = 9,
                    InfoIconId = 5
                },

                new HotelInfoCell()
                {
                    Id = 46,
                    TextLine_1 = "Check-in time from",
                    TextLine_2 = "14:00 to 22:00",
                    HotelId = 10,
                    InfoIconId = 1
                },
                new HotelInfoCell()
                {
                    Id = 47,
                    TextLine_1 = "Maximum number of",
                    TextLine_2 = "extra beds 2",
                    HotelId = 10,
                    InfoIconId = 2
                },
                new HotelInfoCell()
                {
                    Id = 48,
                    TextLine_1 = "City center 750 m",
                    TextLine_2 = "",
                    HotelId = 10,
                    InfoIconId = 3
                },
                new HotelInfoCell()
                {
                    Id = 49,
                    TextLine_1 = "Train station 650 m",
                    TextLine_2 = "Airport 950 m",
                    HotelId = 10,
                    InfoIconId = 4
                },
                new HotelInfoCell()
                {
                    Id = 50,
                    TextLine_1 = "Pets are allowed",
                    TextLine_2 = "No extra charge",
                    HotelId = 10,
                    InfoIconId = 5
                },

                new HotelInfoCell()
                {
                    Id = 51,
                    TextLine_1 = "Check-in time from",
                    TextLine_2 = "16:00 to 24:00",
                    HotelId = 11,
                    InfoIconId = 1
                },
                new HotelInfoCell()
                {
                    Id = 52,
                    TextLine_1 = "Maximum number of",
                    TextLine_2 = "extra beds 2",
                    HotelId = 11,
                    InfoIconId = 2
                },
                new HotelInfoCell()
                {
                    Id = 53,
                    TextLine_1 = "City center 850 m",
                    TextLine_2 = "",
                    HotelId = 11,
                    InfoIconId = 3
                },
                new HotelInfoCell()
                {
                    Id = 54,
                    TextLine_1 = "Train station 750 m",
                    TextLine_2 = "Airport 1050 m",
                    HotelId = 11,
                    InfoIconId = 4
                },
                new HotelInfoCell()
                {
                    Id = 55,
                    TextLine_1 = "Pets are allowed",
                    TextLine_2 = "No extra charge",
                    HotelId = 11,
                    InfoIconId = 5
                },

                new HotelInfoCell()
                {
                    Id = 56,
                    TextLine_1 = "Check-in time from",
                    TextLine_2 = "15:00 to 23:00",
                    HotelId = 12,
                    InfoIconId = 1
                },
                new HotelInfoCell()
                {
                    Id = 57,
                    TextLine_1 = "Maximum number of",
                    TextLine_2 = "extra beds 1",
                    HotelId = 12,
                    InfoIconId = 2
                },
                new HotelInfoCell()
                {
                    Id = 58,
                    TextLine_1 = "City center 950 m",
                    TextLine_2 = "",
                    HotelId = 12,
                    InfoIconId = 3
                },
                new HotelInfoCell()
                {
                    Id = 59,
                    TextLine_1 = "Train station 850 m",
                    TextLine_2 = "Airport 1150 m",
                    HotelId = 12,
                    InfoIconId = 4
                },
                new HotelInfoCell()
                {
                    Id = 60,
                    TextLine_1 = "Pets are allowed",
                    TextLine_2 = "No extra charge",
                    HotelId = 12,
                    InfoIconId = 5
                },

                new HotelInfoCell()
                {
                    Id = 61,
                    TextLine_1 = "Check-in time from",
                    TextLine_2 = "12:00 to 22:00",
                    HotelId = 13,
                    InfoIconId = 1
                },
                new HotelInfoCell()
                {
                    Id = 62,
                    TextLine_1 = "Maximum number of",
                    TextLine_2 = "extra beds 2",
                    HotelId = 13,
                    InfoIconId = 2
                },
                new HotelInfoCell()
                {
                    Id = 63,
                    TextLine_1 = "City center 1050 m",
                    TextLine_2 = "",
                    HotelId = 13,
                    InfoIconId = 3
                },
                new HotelInfoCell()
                {
                    Id = 64,
                    TextLine_1 = "Train station 950 m",
                    TextLine_2 = "Airport 1250 m",
                    HotelId = 13,
                    InfoIconId = 4
                },
                new HotelInfoCell()
                {
                    Id = 65,
                    TextLine_1 = "Pets are allowed",
                    TextLine_2 = "No extra charge",
                    HotelId = 13,
                    InfoIconId = 5
                },

                new HotelInfoCell()
                {
                    Id = 66,
                    TextLine_1 = "Check-in time from",
                    TextLine_2 = "13:00 to 23:00",
                    HotelId = 14,
                    InfoIconId = 1
                },
                new HotelInfoCell()
                {
                    Id = 67,
                    TextLine_1 = "Maximum number of",
                    TextLine_2 = "extra beds 1",
                    HotelId = 14,
                    InfoIconId = 2
                },
                new HotelInfoCell()
                {
                    Id = 68,
                    TextLine_1 = "City center 1150 m",
                    TextLine_2 = "",
                    HotelId = 14,
                    InfoIconId = 3
                },
                new HotelInfoCell()
                {
                    Id = 69,
                    TextLine_1 = "Train station 1050 m",
                    TextLine_2 = "Airport 1350 m",
                    HotelId = 14,
                    InfoIconId = 4
                },
                new HotelInfoCell()
                {
                    Id = 70,
                    TextLine_1 = "Pets are allowed",
                    TextLine_2 = "No extra charge",
                    HotelId = 14,
                    InfoIconId = 5
                },
                new HotelInfoCell()
                {
                    Id = 71,
                    TextLine_1 = "Check-in time from",
                    TextLine_2 = "15:00 to 23:00",
                    HotelId = 15,
                    InfoIconId = 1
                },
                new HotelInfoCell()
                {
                    Id = 72,
                    TextLine_1 = "Maximum number of",
                    TextLine_2 = "extra beds 2",
                    HotelId = 15,
                    InfoIconId = 2
                },
                new HotelInfoCell()
                {
                    Id = 73,
                    TextLine_1 = "City center 500 m",
                    TextLine_2 = "",
                    HotelId = 15,
                    InfoIconId = 3
                },
                new HotelInfoCell()
                {
                    Id = 74,
                    TextLine_1 = "Train station 700 m",
                    TextLine_2 = "Airport 800 m",
                    HotelId = 15,
                    InfoIconId = 4
                },
                new HotelInfoCell()
                {
                    Id = 75,
                    TextLine_1 = "Pets are allowed",
                    TextLine_2 = "No extra charge",
                    HotelId = 15,
                    InfoIconId = 5
                }
            });
        }
    }
}
