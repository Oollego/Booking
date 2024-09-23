using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Booking.Domain.Entity;

namespace Booking.DAL.Configurations
{
    internal class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("countries");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CountryName).HasMaxLength(254).IsRequired();

            builder.HasMany<City>(x => x.Cities)
               .WithOne(x => x.Country)
               .HasForeignKey(x => x.CountryId)
               .HasPrincipalKey(x => x.Id);

            builder.HasData(new List<Country>()
            {
                new Country()
                {
                    Id = 1,
                    CountryName = "Albania",
                    Flag = "albania.png"
                },
                new Country()
                {
                    Id = 2,
                    CountryName = "Andorra",
                    Flag = "andorra.png"
                },
                new Country()
                {
                    Id = 3,
                    CountryName = "Armenia",
                    Flag = "armenia.png"
                },
                new Country()
                {
                    Id = 4,
                    CountryName = "Austria",
                    Flag = "austria.png"
                },
                new Country()
                {
                    Id = 5,
                    CountryName = "Azerbaijan",
                    Flag = "azerbaijan.png"
                },
                new Country()
                {
                    Id = 6,
                    CountryName = "Belgium",
                    Flag = "belgium.png"
                },
                new Country()
                {
                    Id = 7,
                    CountryName = "Bosnia and Herzegovina",
                    Flag = "bosnia_and_herzegovina.png"
                },
                new Country()
                {
                    Id = 8,
                    CountryName = "Bulgaria",
                    Flag = "bulgaria.png"
                },
                new Country()
                {
                    Id = 9,
                    CountryName = "Croatia",
                    Flag = "croatia.png"
                },
                new Country()
                {
                    Id = 10,
                    CountryName = "Cyprus",
                    Flag = "cyprus.png"
                },
                new Country()
                {
                    Id = 11,
                    CountryName = "Czechia",
                    Flag = "czechia.png"
                },
                new Country()
                {
                    Id = 12,
                    CountryName = "Denmark",
                    Flag = "denmark.png"
                },
                new Country()
                {
                    Id = 13,
                    CountryName = "Estonia",
                    Flag = "estonia.png"
                },
                new Country()
                {
                    Id = 14,
                    CountryName = "Finland",
                    Flag = "finland.png"
                },
                new Country()
                {
                    Id = 15,
                    CountryName = "France",
                    Flag = "france.png"
                },
                new Country()
                {
                    Id = 16,
                    CountryName = "Georgia",
                    Flag = "georgia.png"
                },
                new Country()
                {
                    Id = 17,
                    CountryName = "Germany",
                    Flag = "germany.png"
                },
                new Country()
                {
                    Id = 18,
                    CountryName = "Greece",
                    Flag = "greece.png"
                },
                new Country()
                {
                    Id = 19,
                    CountryName = "Hungary",
                    Flag = "hungary.png"
                },
                new Country()
                {
                    Id = 20,
                    CountryName = "Iceland",
                    Flag = "iceland.png"
                },
                new Country()
                {
                    Id = 21,
                    CountryName = "Ireland",
                    Flag = "ireland.png"
                },
                new Country()
                {
                    Id = 22,
                    CountryName = "Italy",
                    Flag = "italy.png"
                },
                new Country()
                {
                    Id = 23,
                    CountryName = "Latvia",
                    Flag = "latvia.png"
                },
                new Country()
                {
                    Id = 24,
                    CountryName = "Liechtenstein",
                    Flag = "liechtenstein.png"
                },
                new Country()
                {
                    Id = 25,
                    CountryName = "Lithuania",
                    Flag = "lithuania.png"
                },
                new Country()
                {
                    Id = 26,
                    CountryName = "Luxembourg",
                    Flag = "luxembourg.png"
                },
                new Country()
                {
                    Id = 27,
                    CountryName = "Malta",
                    Flag = "malta.png"
                },

                new Country()
                {
                    Id = 28,
                    CountryName = "Monaco",
                    Flag = "monaco.png"
                },
                new Country()
                {
                    Id = 29,
                    CountryName = "Montenegro",
                    Flag = "montenegro.png"
                },
                new Country()
                {
                    Id = 30,
                    CountryName = "Netherlands",
                    Flag = "netherlands.png"
                },
                new Country()
                {
                    Id = 31,
                    CountryName = "North Macedonia",
                    Flag = "north_macedonia.png"
                },
                new Country()
                {
                    Id = 32,
                    CountryName = "Norway",
                    Flag = "norway.png"
                },
                new Country()
                {
                    Id = 33,
                    CountryName = "Poland",
                    Flag = "poland.png"
                },
                new Country()
                {
                    Id = 34,
                    CountryName = "Portugal",
                    Flag = "portugal.png"
                },
                new Country()
                {
                    Id = 35,
                    CountryName = "Romania",
                    Flag = "romania.png"
                },
                new Country()
                {
                    Id = 36,
                    CountryName = "San Marino",
                    Flag = "san_marino.png"
                },
                new Country()
                {
                    Id = 37,
                    CountryName = "Slovak Republic",
                    Flag = "slovak_republic.png"
                },
                new Country()
                {
                    Id = 38,
                    CountryName = "Slovenia",
                    Flag = "slovenia.png"
                },
                new Country()
                {
                    Id = 39,
                    CountryName = "Spain",
                    Flag = "spain.png"
                },
                new Country()
                {
                    Id = 40,
                    CountryName = "Sweden",
                    Flag = "sweden.png"
                },
                new Country()
                {
                    Id = 41,
                    CountryName = "Switzerland",
                    Flag = "switzerland.png"
                },
                new Country()
                {
                    Id = 42,
                    CountryName = "Türkiye",
                    Flag = "turkiye.png"
                },
                new Country()
                {
                    Id = 43,
                    CountryName = "Ukraine",
                    Flag = "ukraine.png"
                },
                new Country()
                {
                    Id = 44,
                    CountryName = "United Kingdom",
                    Flag = "united_kingdom.png"
                },
                new Country()
                {
                    Id = 45,
                    CountryName = "Vatican",
                    Flag = "vatican.png"
                },

                new Country()
                {
                    Id = 46,
                    CountryName = "Republic of Moldova",
                    Flag = "moldova.png"
                },
            });
        }
    }
}
