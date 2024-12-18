﻿using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Configurations
{
    internal class PayMethodConfiguration : IEntityTypeConfiguration<PayMethod>
    {
        public void Configure(EntityTypeBuilder<PayMethod> builder)
        {
            builder.ToTable("pay_methods");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CardNumber).HasMaxLength(16).IsRequired();
            builder.Property(x => x.CardDate).IsRequired();
            builder.Property(x => x.CardTypeId).IsRequired();
            builder.Property(x => x.UserProfileId).IsRequired();

            //builder.HasMany<UserProfile>(x => x.UserProfile)
            //    .WithOne(x => x.PayMethod)
            //    .HasForeignKey(x => x.PayMethodId)
            //    .HasPrincipalKey(x => x.Id);

            builder.HasData(new List<PayMethod>
            {
                new PayMethod
                {
                    Id = 1,
                    CardNumber = "1111222233334444",
                    CardDate = DateTime.Now.AddDays(-10),
                    CardTypeId = 1,
                    UserProfileId = 1
                },

                new PayMethod
                {
                    Id = 2,
                    CardNumber = "2222222233334444",
                    CardDate = DateTime.Now.AddDays(-3),
                    CardTypeId = 2,
                    UserProfileId = 1
                },
                new PayMethod
                {
                    Id = 3,
                    CardNumber = "3333222233334444",
                    CardDate = DateTime.Now.AddDays(-5),
                    CardTypeId = 1,
                    UserProfileId = 2
                },
                 new PayMethod
                {
                    Id = 4,
                    CardNumber = "4444222233334444",
                    CardDate = DateTime.Now.AddDays(-10),
                    CardTypeId = 1,
                    UserProfileId = 2
                }

            });
        }
    }
}
