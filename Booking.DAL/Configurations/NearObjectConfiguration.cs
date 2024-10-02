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
    internal class NearObjectConfiguration : IEntityTypeConfiguration<NearObject>
    {
        public void Configure(EntityTypeBuilder<NearObject> builder)
        {
            builder.ToTable("near_objects");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Distance).IsRequired();
            builder.Property(x => x.DistanceMetric).HasDefaultValue(false);
            builder.Property(x => x.NearObjectNameId).IsRequired();

            builder.HasData(GenerateNearPlaceData());
        }

        private List<NearObject> GenerateNearPlaceData()
        {
            var nearPlaces = new List<NearObject>();
 
            long idCounter = 1;

            for(long i = 1; i <= 15; i++)
            {
                for (long j = 1;  j <= 4; j++)
                {
                    bool isMetric = (idCounter % 2 == 0);
                    int distance = isMetric ? new Random().Next(1, 11) : new Random().Next(300, 801);

                    nearPlaces.Add(new NearObject()
                    {
                        Id = idCounter++,
                        Distance = distance,
                        DistanceMetric = isMetric,
                        HotelId = i,
                        NearObjectNameId = j
                    });
                }
            }
            return nearPlaces;
        }
    }
}
