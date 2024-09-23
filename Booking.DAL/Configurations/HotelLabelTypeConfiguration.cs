using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Configurations
{
    internal class HotelLabelTypeConfiguration : IEntityTypeConfiguration<HotelLabelType>
    {
        public void Configure(EntityTypeBuilder<HotelLabelType> builder)
        {
            builder.ToTable("hotel_label_types");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.LabelName).HasMaxLength(254);
            builder.Property(x => x.LabelIcon).HasMaxLength(254).IsRequired();

            builder.HasMany(x => x.Hotels)
              .WithMany(x => x.HotelLabelTypes)
              .UsingEntity<HotelLabel>(
              l => l.HasOne<Hotel>().WithMany().HasForeignKey(x => x.HotelId),
              l => l.HasOne<HotelLabelType>().WithMany().HasForeignKey(x => x.HotelLabelTypeId)
              );
            builder.HasData( new List<HotelLabelType>()
            {
                new HotelLabelType()
                {
                    Id = 1,
                    LabelIcon = "crown.png",
                    LabelName = "Popular"
                },
                new HotelLabelType()
                {
                    Id = 2,
                    LabelIcon = "center.png",
                    LabelName = "City center"
                },
                new HotelLabelType()
                {
                    Id = 3,
                    LabelIcon = "coffee.png",
                    LabelName = "Comfortable"
                },
                new HotelLabelType()
                {
                    Id = 4,
                    LabelIcon = "bestrating.png",
                    LabelName = "Best Rating"
                }
                
            });
        }
    }
}