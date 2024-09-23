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
    internal class FacilityConfiguration : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder.ToTable("facilities");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FacilityName).HasMaxLength(254).IsRequired();
            builder.Property(x => x.FacilityGroupId).IsRequired();

            builder.HasData(new List<Facility>()
            {
                new Facility()
                {
                    Id = 1,
                    FacilityName = "Shuttle service",
                    FacilityGroupId = 1,
                },
                new Facility()
                {
                    Id = 2,
                    FacilityName = "Additional charge",
                    FacilityGroupId = 1,
                },
                new Facility()
                {
                    Id = 3,
                    FacilityName = "Grocery deliveries",
                    FacilityGroupId = 1,
                },
                new Facility()
                {
                    Id = 4,
                    FacilityName = "Minimarket on site",
                    FacilityGroupId = 1,
                },
                new Facility()
                {
                    Id = 5,
                    FacilityName = "Designated smoking area",
                    FacilityGroupId = 1,
                },
                new Facility()
                {
                    Id = 6,
                    FacilityName = "Air conditioning",
                    FacilityGroupId = 1,
                },
                 new Facility()
                {
                    Id = 7,
                    FacilityName = "Mosquito net",
                    FacilityGroupId = 1,
                },
                 new Facility()
                {
                    Id = 8,
                    FacilityName = "Wake-up service",
                    FacilityGroupId = 1,
                },
                 new Facility()
                {
                    Id = 9,
                    FacilityName = "Heating",
                    FacilityGroupId = 1,
                },
                 new Facility()
                {
                    Id = 10,
                    FacilityName = "Interconnected room(s) available",
                    FacilityGroupId = 1,
                },
                 new Facility()
                {
                    Id = 11,
                    FacilityName = "Lift",
                    FacilityGroupId = 1,
                },
                 new Facility()
                {
                    Id = 12,
                    FacilityName = "Family rooms",
                    FacilityGroupId = 1,
                },
                 new Facility()
                {
                    Id = 13,
                    FacilityName = "Barber/beauty shop",
                    FacilityGroupId = 1,
                },
                 new Facility()
                {
                    Id = 14,
                    FacilityName = "Airport shuttle",
                    FacilityGroupId = 1,
                },
                 new Facility()
                {
                    Id = 15,
                    FacilityName = "Additional charge",
                    FacilityGroupId = 1,
                },
                 new Facility()
                {
                    Id = 16,
                    FacilityName = "Non-smoking rooms",
                    FacilityGroupId = 1,
                },
                 new Facility()
                {
                    Id = 17,
                    FacilityName = "Wake up service/Alarm clock",
                    FacilityGroupId = 1,
                },
                 new Facility()
                {
                    Id = 18,
                    FacilityName = "Room service",
                    FacilityGroupId = 1,
                },
                 new Facility()
                {
                    Id = 19,
                    FacilityName = "Upper floors accessible by stairs only",
                    FacilityGroupId = 2,
                },
                 new Facility()
                {
                    Id = 20,
                    FacilityName = "Upper floors accessible by elevator",
                    FacilityGroupId = 2,
                },
                 new Facility()
                {
                    Id = 21,
                    FacilityName = "English",
                    FacilityGroupId = 3,
                },
                 new Facility()
                {
                    Id = 22,
                    FacilityName = "German",
                    FacilityGroupId = 3,
                },
                 new Facility()
                {
                    Id = 23,
                    FacilityName = "Ukrainian",
                    FacilityGroupId = 3,
                },
                 new Facility()
                {
                    Id = 24,
                    FacilityName = "Parking garage",
                    FacilityGroupId = 4,
                },
                 new Facility()
                {
                    Id = 25,
                    FacilityName = "Fire extinguishers",
                    FacilityGroupId = 5,
                },
                  new Facility()
                {
                    Id = 26,
                    FacilityName = "CCTV outside property",
                    FacilityGroupId = 5,
                },
                  new Facility()
                {
                    Id = 27,
                    FacilityName = "CCTV in common areas",
                    FacilityGroupId = 5,
                },
                  new Facility()
                {
                    Id = 28,
                    FacilityName = "Smoke alarms",
                    FacilityGroupId = 5,
                },
                  new Facility()
                {
                    Id = 29,
                    FacilityName = "Security alarm",
                    FacilityGroupId = 5,
                },
                  new Facility()
                {
                    Id = 30,
                    FacilityName = "Key card access",
                    FacilityGroupId = 5,
                },
                  new Facility()
                {
                    Id = 31,
                    FacilityName = "24-hour security",
                    FacilityGroupId = 5,
                },
                  new Facility()
                {
                    Id = 32,
                    FacilityName = "Safety deposit box",
                    FacilityGroupId = 5,
                },
                  new Facility()
                {
                    Id = 33,
                    FacilityName = "Daily housekeeping",
                    FacilityGroupId = 6,
                },
                  new Facility()
                {
                    Id = 34,
                    FacilityName = "Laundry",
                    FacilityGroupId = 6,
                },
                  new Facility()
                {
                    Id = 35,
                    FacilityName = "Additional charge",
                    FacilityGroupId = 6,
                },
                  new Facility()
                {
                    Id = 36,
                    FacilityName = "Kids' outdoor play equipment",
                    FacilityGroupId = 7,
                },
                  new Facility()
                {
                    Id = 37,
                    FacilityName = "Invoice provided",
                    FacilityGroupId = 8,
                },
                  new Facility()
                {
                    Id = 38,
                    FacilityName = "Private check-in/check-out",
                    FacilityGroupId = 8,
                },
                  new Facility()
                {
                    Id = 39,
                    FacilityName = "Concierge service",
                    FacilityGroupId = 8,
                },
                  new Facility()
                {
                    Id = 40,
                    FacilityName = "Luggage storage",
                    FacilityGroupId = 8,
                },
                  new Facility()
                {
                    Id = 41,
                    FacilityName = "Express check-in/check-out",
                    FacilityGroupId = 8,
                },
                  new Facility()
                {
                    Id = 42,
                    FacilityName = "24-hour front desk",
                    FacilityGroupId = 8,
                },
                  new Facility()
                {
                    Id = 43,
                    FacilityName = "Toilet paper",
                    FacilityGroupId = 9,
                },
                  new Facility()
                {
                    Id = 44,
                    FacilityName = "Towels",
                    FacilityGroupId = 9,
                },
                  new Facility()
                {
                    Id = 45,
                    FacilityName = "Slippers",
                    FacilityGroupId = 9,
                },
                  new Facility()
                {
                    Id = 46,
                    FacilityName = "Private bathroom",
                    FacilityGroupId = 9,
                },
                  new Facility()
                {
                    Id = 47,
                    FacilityName = "Toilet",
                    FacilityGroupId = 9,
                },
                  new Facility()
                {
                    Id = 48,
                    FacilityName = "Free toiletries",
                    FacilityGroupId = 9,
                },
                  new Facility()
                {
                    Id = 49,
                    FacilityName = "Hairdryer",
                    FacilityGroupId = 9,
                },
                  new Facility()
                {
                    Id = 50,
                    FacilityName = "Shower",
                    FacilityGroupId = 9,
                },
                  new Facility()
                {
                    Id = 51,
                    FacilityName = "Linen",
                    FacilityGroupId = 10,
                },
                  new Facility()
                {
                    Id = 52,
                    FacilityName = "Electric kettle",
                    FacilityGroupId = 11,
                },
                  new Facility()
                {
                    Id = 53,
                    FacilityName = "Clothes rack",
                    FacilityGroupId = 12,
                },
                  new Facility()
                {
                    Id = 54,
                    FacilityName = "Pets are allowed. No extra charges",
                    FacilityGroupId = 13,
                },
                  new Facility()
                {
                    Id = 55,
                    FacilityName = "Flat-screen TV",
                    FacilityGroupId = 14,
                },
                  new Facility()
                {
                    Id = 56,
                    FacilityName = "Cable channels",
                    FacilityGroupId = 15,
                },
                  new Facility()
                {
                    Id = 57,
                    FacilityName = "Coffee house on site",
                    FacilityGroupId = 16,
                },
                  new Facility()
                {
                    Id = 58,
                    FacilityName = "Internet access available",
                    FacilityGroupId = 17,
                },


            });
          
        }
    }
}
