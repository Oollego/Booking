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
    internal class OwnerPayMethodConfiguration: IEntityTypeConfiguration<OwnerPayMethod>
    {
        public void Configure(EntityTypeBuilder<OwnerPayMethod> builder)
        {
            builder.ToTable("owner_pay_methods");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CardNumber).HasMaxLength(16).IsRequired();
            builder.Property(x => x.CardDate).IsRequired();
            builder.Property(x => x.CardTypeId).IsRequired();
            builder.Property(x => x.OwnerProfileId).IsRequired();

            //builder.HasMany<UserProfile>(x => x.UserProfile)
            //    .WithOne(x => x.PayMethod)
            //    .HasForeignKey(x => x.PayMethodId)
            //    .HasPrincipalKey(x => x.Id);

            builder.HasData(new List<OwnerPayMethod>
            {
                new OwnerPayMethod
                {
                    Id = 1,
                    CardNumber = "1111222233334555",
                    CardDate = DateTime.Now.AddDays(-10),
                    CardTypeId = 1,
                    OwnerProfileId = 1
                },

                new OwnerPayMethod
                {
                    Id = 2,
                    CardNumber = "2222222233334555",
                    CardDate = DateTime.Now.AddDays(-3),
                    CardTypeId = 2,
                    OwnerProfileId = 1
                },
                new OwnerPayMethod
                {
                    Id = 3,
                    CardNumber = "3333222233334555",
                    CardDate = DateTime.Now.AddDays(-5),
                    CardTypeId = 1,
                    OwnerProfileId = 2
                },
                 new OwnerPayMethod
                {
                    Id = 4,
                    CardNumber = "4444222233334555",
                    CardDate = DateTime.Now.AddDays(-10),
                    CardTypeId = 1,
                    OwnerProfileId = 3
                }

            });
        }
    }
}
