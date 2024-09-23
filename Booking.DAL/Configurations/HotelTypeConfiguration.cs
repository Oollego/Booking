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
    internal class HotelTypeConfiguration : IEntityTypeConfiguration<HotelType>
    {
        void IEntityTypeConfiguration<HotelType>.Configure(EntityTypeBuilder<HotelType> builder)
        {
            builder.ToTable("hotelTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.HotelTypeName).HasMaxLength(254);

            builder.HasMany<Hotel>(x => x.Hotels)
                .WithOne(x => x.HotelType)
                .HasForeignKey(x => x.HotelTypeId)
                .HasPrincipalKey(x => x.Id);

            builder.HasData(new List<HotelType>
            {
                new HotelType{ Id = 1, HotelTypeName = "Motel"},
                new HotelType{ Id = 2, HotelTypeName = "Resort"},
                new HotelType{ Id = 3, HotelTypeName = "Inn"},
                new HotelType{ Id = 4, HotelTypeName = "All-suite"},
                new HotelType{ Id = 5, HotelTypeName = "Conference center"},
                new HotelType{ Id = 6, HotelTypeName = "Extended stay"},
                new HotelType{ Id = 7, HotelTypeName = "Boutique"},
                new HotelType{ Id = 8, HotelTypeName = "Bunkhous"},
                new HotelType{ Id = 9, HotelTypeName = "Bed and breakfasts"}
            });
        }
    }
}
