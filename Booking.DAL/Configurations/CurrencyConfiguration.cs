using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Configurations
{
    internal class CurrencyConfiguration: IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("currencies");
            builder.HasKey(x => x.CurrencyCode);
            builder.Property(x => x.CurrencyCode).IsRequired().HasMaxLength(3);
            builder.Property(x => x.CurrencyChar).IsRequired().HasMaxLength(1);
            builder.Property(x => x.CurrencyName).HasMaxLength(100);
            builder.Property(x => x.ExchangeRate).IsRequired();

            builder.HasMany<UserProfile>(x => x.UserProfiles)
                .WithOne(x => x.Currency)
                .HasForeignKey(x => x.CurrencyCodeId)
                .HasPrincipalKey(x => x.CurrencyCode);
            builder.HasData(new List<Currency>
            {
                new Currency()
                {
                    CurrencyCode = "USD",
                    CurrencyChar = "$",
                    CurrencyName = "United States Dollar",
                    ExchangeRate = 41.2881
                },
                 new Currency()
                {
                    CurrencyCode = "EUR",
                    CurrencyChar = "€",
                    CurrencyName = "Euro Member Countries",
                    ExchangeRate = 45.9826
                },
                 new Currency()
                {
                    CurrencyCode = "UAH",
                    CurrencyChar = "₴",
                    CurrencyName = "Ukraine Hryvnia",
                    ExchangeRate = 1
                },
                 new Currency()
                 {
                     CurrencyCode = "GBP",
                     CurrencyChar = "£",
                     CurrencyName = "United Kingdom Pound",
                     ExchangeRate = 54.137
                 }
            });
        }
    }
}
