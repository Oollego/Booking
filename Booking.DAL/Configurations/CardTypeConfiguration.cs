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
    internal class CardTypeConfiguration : IEntityTypeConfiguration<CardType>
    {
        public void Configure(EntityTypeBuilder<CardType> builder)
        {
            builder.ToTable("card_types");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CardName).IsRequired().HasMaxLength(100);

            builder.HasMany<PayMethod>(x => x.PayMethods)
                .WithOne(x => x.CardType)
                .HasForeignKey(x => x.CardTypeId)
                .HasPrincipalKey(x => x.Id);
            builder.HasData(new List<CardType>()
            {
                new CardType()
                {
                    Id = 1,
                    CardName = "Visa",
                },
                new CardType()
                {
                    Id = 2,
                    CardName = "MasterCard",
                }

            });
        }
    }
}
