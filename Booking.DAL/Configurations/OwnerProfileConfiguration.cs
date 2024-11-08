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
    internal class OwnerProfileConfiguration : IEntityTypeConfiguration<OwnerProfile>
    {
        public void Configure(EntityTypeBuilder<OwnerProfile> builder)
        {
            builder.ToTable("OwnerProfiles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CompanyName).IsRequired().HasMaxLength(254);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(254);
            builder.Property(x => x.CurrencyCodeId).HasDefaultValue("UAH");
            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.CityId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            

            builder.HasOne<User>(x => x.User)
                .WithOne(x => x.OwnerProfile)
                .HasForeignKey<OwnerProfile>(x => x.UserId);

            builder.HasMany<OwnerPayMethod>(x => x.OwnerPayMethods)
               .WithOne(x => x.OwnerProfile)
               .HasForeignKey(x => x.OwnerProfileId)
               .HasPrincipalKey(x => x.Id);

            builder.HasMany<Hotel>(x => x.Hotels)
               .WithOne(x => x.OwnerProfile)
               .HasForeignKey(x => x.OwnerProfileId)
               .HasPrincipalKey(x => x.Id);

            builder.HasData(new List<OwnerProfile>
            {
                new OwnerProfile
                {
                    Id = 1,
                    CompanyName = "Mercury corporation",
                    Address = "Mira Avenu",
                    Phone = "+38986543232",
                    CityId = 218,
                    CurrencyCodeId = "UAH",
                    UserId = 3,
                },
                new OwnerProfile
                {
                    Id = 2,
                    CompanyName = "Oleg & Company",
                    Address = "Deribasovskaya str.",
                    Phone = "+38986546464",
                    CityId = 218,
                    CurrencyCodeId = "USD",
                    UserId = 4,
                },
                new OwnerProfile
                {
                    Id = 3,
                    CompanyName = "Samsung",
                    Address = "Deribasovskaya str.",
                    Phone = "+38986547474",
                    CityId = 218,
                    CurrencyCodeId = "EUR",
                    UserId = 5,
                }
            });
        }
    }
}
