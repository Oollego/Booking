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
    internal class NearPlacesGroupConfiguration : IEntityTypeConfiguration<NearPlacesGroup>
    {
        public void Configure(EntityTypeBuilder<NearPlacesGroup> builder)
        {
            builder.ToTable("near_places_groups");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PlaceGroupName).IsRequired().HasMaxLength(254);
            builder.Property(x => x.GroupIcon).HasMaxLength(254);

            builder.HasMany<NearPlace>(x => x.NearPlaces)
             .WithOne(x => x.NearPlacesGroup)
             .HasForeignKey(x => x.NearPlacesGroupId)
             .HasPrincipalKey(x => x.Id);
            builder.HasData(new List<NearPlacesGroup>()
            {
                new NearPlacesGroup()
                {
                    Id = 1,
                    PlaceGroupName = "Attractions",
                    GroupIcon = "attractions.png"

                },
                new NearPlacesGroup()
                {
                    Id = 2,
                    PlaceGroupName = "Restaurants & cafes",
                    GroupIcon = "cafe.png"
                },
                new NearPlacesGroup()
                {
                    Id = 3,
                    PlaceGroupName = "Public transport",
                    GroupIcon = "transport.png"
                },
                 new NearPlacesGroup()
                {
                    Id = 4,
                    PlaceGroupName = "Airport",
                    GroupIcon = "airport.png"
                },
                new NearPlacesGroup()
                {
                    Id = 5,
                    PlaceGroupName = "Beaches in the neighbourhood",
                    GroupIcon = "beaches.png"
                },
                 new NearPlacesGroup()
                {
                     Id = 6,
                     PlaceGroupName = "Railway station",
                     GroupIcon = "train.png"
                },

            });
        }
    }
}
