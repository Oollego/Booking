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
    internal class HotelFacilityConfiguration : IEntityTypeConfiguration<HotelFacility>
    {
        public void Configure(EntityTypeBuilder<HotelFacility> builder)
        {
            builder.ToTable("hotel_facilities");
            builder.Property(x => x.FacilityId).IsRequired();
            builder.Property(x => x.HotelId).IsRequired();

            builder.HasData(GenerateHotelFacilities());
        }

        private List<HotelFacility> GenerateHotelFacilities()
        {
       
            var hotelFacilities = new List<HotelFacility>();
            for( int i = 1; i <= 15; i++ )
            {
                int j = 1;
                if( i == 1)
                {
                    j = 6;
                }
                else
                {
                    if (i % 2 == 0) j = 3;
                }
                
                for(; j <= 58; j++ )
                {
                    hotelFacilities.Add(new HotelFacility
                    {
                        HotelId = i,
                        FacilityId = j
                    });
                }
            }

            return hotelFacilities;
        }
    }
}

