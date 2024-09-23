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
    internal class HotelChainConfiguration : IEntityTypeConfiguration<HotelChain>
    {
        void IEntityTypeConfiguration<HotelChain>.Configure(EntityTypeBuilder<HotelChain> builder)
        {
            builder.ToTable("hotelChains");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.HotelChainName).HasMaxLength(254);

            builder.HasMany<Hotel>(x => x.Hotels)
                .WithOne(x => x.HotelChain)
                .HasForeignKey(x => x.HotelChainId)
                .HasPrincipalKey(x => x.Id);

            builder.HasData(new List<HotelChain>
            {
                new HotelChain{ Id = 1, HotelChainName = "Unbranded hotel"},
                new HotelChain{ Id = 2, HotelChainName = "Sapphire Suites"},
                new HotelChain{ Id = 3, HotelChainName = "Ocean View Hotel"},
                new HotelChain{ Id = 4, HotelChainName = "Sunflower Resort"},
                new HotelChain{ Id = 5, HotelChainName = "Starlight Lodge"},
                new HotelChain{ Id = 6, HotelChainName = "Paradise Plaza"},
                new HotelChain{ Id = 7, HotelChainName = "Golden Sands Inn"},
                new HotelChain{ Id = 8, HotelChainName = "Moonlight Manor"},
                new HotelChain{ Id = 9, HotelChainName = "Sunset Suites"},
                new HotelChain{ Id = 10, HotelChainName = "Skyline Hotel"}
            });
        }
    }
}
