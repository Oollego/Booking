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
    internal class InfoIconConfigurations : IEntityTypeConfiguration<InfoIcon>
    {
        public void Configure(EntityTypeBuilder<InfoIcon> builder)
        {
            builder.ToTable("info_icons");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.IconFileName).HasMaxLength(254);

            builder.HasMany<HotelInfoCell>(x => x.HotelInfoCells)
               .WithOne(x => x.InfoIcon)
               .HasForeignKey(x => x.InfoIconId)
               .HasPrincipalKey(x => x.Id);

            builder.HasData(new List<InfoIcon>()
            {
                new InfoIcon()
                {
                    Id = 1,
                    IconFileName = "clock.png"
                },
                new InfoIcon()
                {
                    Id = 2,
                    IconFileName = "bed.png"
                },
                new InfoIcon()
                {
                    Id = 3,
                    IconFileName = "center.png"
                },
                new InfoIcon()
                {
                    Id = 4,
                    IconFileName = "train.png"
                },
                 new InfoIcon()
                {
                    Id = 5,
                    IconFileName = "pet.png"
                }
            });
        }
    }
}
