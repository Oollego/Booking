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
    internal class NearStationNameConfiguration : IEntityTypeConfiguration<NearStationName>
    {
        public void Configure(EntityTypeBuilder<NearStationName> builder)
        {
            builder.ToTable("near_place_names");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(254);
            builder.Property(x => x.Icon).HasMaxLength(254);

            builder.HasMany<NearStation>(x => x.NearStations)
             .WithOne(x => x.NearStationName)
             .HasForeignKey(x => x.NearPlaceNameId)
             .HasPrincipalKey(x => x.Id);
            builder.HasData(new List<NearStationName>()
            {
                new NearStationName()
                {
                    Id = 1,
                    Name = "airport"
                },
                new NearStationName()
                {
                    Id = 2,
                    Name = "railway station"
                },
                new NearStationName()
                {
                    Id = 3,
                    Name = "bus station"
                },
                new NearStationName()
                {
                    Id = 4,
                    Name = "the city center"
                }
            });
        }
    }
}
