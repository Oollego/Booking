using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Configurations
{
    internal class FacilityGroupConfiguration : IEntityTypeConfiguration<FacilityGroup>
    {
        public void Configure(EntityTypeBuilder<FacilityGroup> builder)
        {
            builder.ToTable("facility_groups");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FacilityGroupName).HasMaxLength(254).IsRequired();
            builder.Property(x => x.FacilityGroupIcon).HasMaxLength(254).HasDefaultValue("");

            builder.HasMany<Facility>(x => x.Facilities)
                .WithOne(x => x.FacilityGroup)
                .HasForeignKey(x => x.FacilityGroupId)
                .HasPrincipalKey(x => x.Id);

            builder.HasData(new List<FacilityGroup> 
            {
                new FacilityGroup
                {
                    Id = 1,
                    FacilityGroupName = "General",
                    FacilityGroupIcon = "general.png"
                },
                 new FacilityGroup
                {
                    Id = 2,
                    FacilityGroupName = "Accessibility",
                    FacilityGroupIcon = "accessibility.png"
                },
                  new FacilityGroup
                {
                    Id = 3,
                    FacilityGroupName = "Languages spoken",
                    FacilityGroupIcon = "language.png"
                },
                  new FacilityGroup
                {
                    Id = 4,
                    FacilityGroupName = "Parking",
                    FacilityGroupIcon = "parking.png"
                },
                  new FacilityGroup
                {
                    Id = 5,
                    FacilityGroupName = "Reception services",
                    FacilityGroupIcon = "reception.png"
                },
                  new FacilityGroup
                {
                    Id = 6,
                    FacilityGroupName = "Cleaning services",
                    FacilityGroupIcon = "cleaning.png"
                },
                  new FacilityGroup
                {
                    Id = 7,
                    FacilityGroupName = "Entertainment and family services",
                    FacilityGroupIcon = "entertainment.png"
                },
                  new FacilityGroup
                {
                    Id = 8,
                    FacilityGroupName = "Safety & security",
                    FacilityGroupIcon = "safety.png"
                },
                  new FacilityGroup
                {
                    Id = 9,
                    FacilityGroupName = "Bathroom",
                    FacilityGroupIcon = "bathroom.png"
                },
                  new FacilityGroup
                {
                    Id = 10,
                    FacilityGroupName = "Bedroom",
                    FacilityGroupIcon = "bedroom.png"
                },
                  new FacilityGroup
                {
                    Id = 11,
                    FacilityGroupName = "Kitchen",
                    FacilityGroupIcon = "kitchen.png"
                },
                  new FacilityGroup
                {
                    Id = 12,
                    FacilityGroupName = "Room Amenities",
                    FacilityGroupIcon = "room.png"
                },
                  new FacilityGroup
                {
                    Id = 13,
                    FacilityGroupName = "Pets",
                    FacilityGroupIcon = "pets.png"
                },
                  new FacilityGroup
                {
                    Id = 14,
                    FacilityGroupName = "Media & Technology",
                    FacilityGroupIcon = "media.png"
                },
                  new FacilityGroup
                {
                    Id = 15,
                    FacilityGroupName = "Media & Technology",
                    FacilityGroupIcon = "media.png"
                },
                  new FacilityGroup
                {
                    Id = 16,
                    FacilityGroupName = "Food & Drinks",
                    FacilityGroupIcon = "food.png"
                },
                  new FacilityGroup
                {
                    Id = 17,
                    FacilityGroupName = "Internet",
                    FacilityGroupIcon = "internet.png"
                },
            });
        }
    }
}
