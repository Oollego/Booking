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
    internal class NearObjectNameConfiguration : IEntityTypeConfiguration<NearObjectName>
    {
        public void Configure(EntityTypeBuilder<NearObjectName> builder)
        {
            builder.ToTable("near_object_names");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(254);
            builder.Property(x => x.Icon).HasMaxLength(254);

            builder.HasMany<NearObject>(x => x.NearObjects)
             .WithOne(x => x.NearObjectName)
             .HasForeignKey(x => x.NearObjectNameId)
             .HasPrincipalKey(x => x.Id);
            builder.HasData(new List<NearObjectName>()
            {
                new NearObjectName()
                {
                    Id = 1,
                    Name = "airport",

                },
                new NearObjectName()
                {
                    Id = 2,
                    Name = "railway station",

                },
                new NearObjectName()
                {
                    Id = 3,
                    Name = "bus station",

                },
                new NearObjectName()
                {
                    Id = 4,
                    Name = "the city center",

                }
            });
        }
    }
}
