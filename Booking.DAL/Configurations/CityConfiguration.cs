using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Booking.Domain.Entity;

namespace Booking.DAL.Configurations
{
    internal class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("cities");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CityName).HasMaxLength(254).IsRequired();
            builder.Property(x => x.CountryId).IsRequired();

            builder.HasMany<UserProfile>(x => x.UserProfiles)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId)
                .HasPrincipalKey(x => x.Id);
            builder.HasMany<OwnerProfile>(x => x.OwnerProfiles)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId)
                .HasPrincipalKey(x => x.Id);
            builder.HasMany<Hotel>(x => x.Hotels)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId)
                .HasPrincipalKey(x => x.Id);

            builder.HasData(new List<City>
            {
                // Albania
                new City { Id = 1, CityName = "Tirana", CountryId = 1 },
                new City { Id = 2, CityName = "Durrës", CountryId = 1 },
                new City { Id = 3, CityName = "Vlorë", CountryId = 1 },
                new City { Id = 4, CityName = "Shkodër", CountryId = 1 },
                new City { Id = 5, CityName = "Berat", CountryId = 1 },

                // Andorra
                new City { Id = 6, CityName = "Andorra la Vella", CountryId = 2 },
                new City { Id = 7, CityName = "Escaldes-Engordany", CountryId = 2 },
                new City { Id = 8, CityName = "Encamp", CountryId = 2 },
                new City { Id = 9, CityName = "La Massana", CountryId = 2 },

                // Armenia
                new City { Id = 10, CityName = "Yerevan", CountryId = 3 },
                new City { Id = 11, CityName = "Gyumri", CountryId = 3 },
                new City { Id = 12, CityName = "Vanadzor", CountryId = 3 },
                new City { Id = 13, CityName = "Dilijan", CountryId = 3 },
                new City { Id = 14, CityName = "Sevan", CountryId = 3 },

                // Austria
                new City { Id = 15, CityName = "Vienna", CountryId = 4 },
                new City { Id = 16, CityName = "Salzburg", CountryId = 4 },
                new City { Id = 17, CityName = "Innsbruck", CountryId = 4 },
                new City { Id = 18, CityName = "Graz", CountryId = 4 },
                new City { Id = 19, CityName = "Linz", CountryId = 4 },

                // Azerbaijan
                new City { Id = 20, CityName = "Baku", CountryId = 5 },
                new City { Id = 21, CityName = "Ganja", CountryId = 5 },
                new City { Id = 22, CityName = "Sheki", CountryId = 5 },
                new City { Id = 23, CityName = "Sumqayit", CountryId = 5 },
                new City { Id = 24, CityName = "Quba", CountryId = 5 },

                // Belgium
                new City { Id = 25, CityName = "Brussels", CountryId = 6 },
                new City { Id = 26, CityName = "Antwerp", CountryId = 6 },
                new City { Id = 27, CityName = "Ghent", CountryId = 6 },
                new City { Id = 28, CityName = "Bruges", CountryId = 6 },
                new City { Id = 29, CityName = "Liège", CountryId = 6 },

                // Bosnia and Herzegovina
                new City { Id = 30, CityName = "Sarajevo", CountryId = 7 },
                new City { Id = 31, CityName = "Mostar", CountryId = 7 },
                new City { Id = 32, CityName = "Banja Luka", CountryId = 7 },
                new City { Id = 33, CityName = "Tuzla", CountryId = 7 },
                new City { Id = 34, CityName = "Trebinje", CountryId = 7 },

                // Bulgaria
                new City { Id = 35, CityName = "Sofia", CountryId = 8 },
                new City { Id = 36, CityName = "Plovdiv", CountryId = 8 },
                new City { Id = 37, CityName = "Varna", CountryId = 8 },
                new City { Id = 38, CityName = "Burgas", CountryId = 8 },
                new City { Id = 39, CityName = "Veliko Tarnovo", CountryId = 8 },

                // Croatia
                new City { Id = 40, CityName = "Zagreb", CountryId = 9 },
                new City { Id = 41, CityName = "Split", CountryId = 9 },
                new City { Id = 42, CityName = "Dubrovnik", CountryId = 9 },
                new City { Id = 43, CityName = "Rijeka", CountryId = 9 },
                new City { Id = 44, CityName = "Zadar", CountryId = 9 },

                // Cyprus
                new City { Id = 45, CityName = "Nicosia", CountryId = 10 },
                new City { Id = 46, CityName = "Limassol", CountryId = 10 },
                new City { Id = 47, CityName = "Larnaca", CountryId = 10 },
                new City { Id = 48, CityName = "Paphos", CountryId = 10 },
                new City { Id = 49, CityName = "Famagusta", CountryId = 10 },

                // Czechia
                new City { Id = 50, CityName = "Prague", CountryId = 11 },
                new City { Id = 51, CityName = "Brno", CountryId = 11 },
                new City { Id = 52, CityName = "Ostrava", CountryId = 11 },
                new City { Id = 53, CityName = "Plzeň", CountryId = 11 },
                new City { Id = 54, CityName = "Karlovy Vary", CountryId = 11 },

                // Denmark
                new City { Id = 55, CityName = "Copenhagen", CountryId = 12 },
                new City { Id = 56, CityName = "Aarhus", CountryId = 12 },
                new City { Id = 57, CityName = "Odense", CountryId = 12 },
                new City { Id = 58, CityName = "Aalborg", CountryId = 12 },
                new City { Id = 59, CityName = "Esbjerg", CountryId = 12 },

                // Estonia
                new City { Id = 60, CityName = "Tallinn", CountryId = 13 },
                new City { Id = 61, CityName = "Tartu", CountryId = 13 },
                new City { Id = 62, CityName = "Narva", CountryId = 13 },
                new City { Id = 63, CityName = "Pärnu", CountryId = 13 },
                new City { Id = 64, CityName = "Viljandi", CountryId = 13 },

                // Finland
                new City { Id = 65, CityName = "Helsinki", CountryId = 14 },
                new City { Id = 66, CityName = "Espoo", CountryId = 14 },
                new City { Id = 67, CityName = "Tampere", CountryId = 14 },
                new City { Id = 68, CityName = "Turku", CountryId = 14 },
                new City { Id = 69, CityName = "Oulu", CountryId = 14 },

                // France
                new City { Id = 70, CityName = "Paris", CountryId = 15 },
                new City { Id = 71, CityName = "Marseille", CountryId = 15 },
                new City { Id = 72, CityName = "Lyon", CountryId = 15 },
                new City { Id = 73, CityName = "Nice", CountryId = 15 },
                new City { Id = 74, CityName = "Bordeaux", CountryId = 15 },
                new City { Id = 75, CityName = "Toulouse", CountryId = 15 },

                // Georgia
                new City { Id = 76, CityName = "Tbilisi", CountryId = 16 },
                new City { Id = 77, CityName = "Batumi", CountryId = 16 },
                new City { Id = 78, CityName = "Kutaisi", CountryId = 16 },
                new City { Id = 79, CityName = "Rustavi", CountryId = 16 },
                new City { Id = 80, CityName = "Zugdidi", CountryId = 16 },

                // Germany
                new City { Id = 81, CityName = "Berlin", CountryId = 17 },
                new City { Id = 82, CityName = "Munich", CountryId = 17 },
                new City { Id = 83, CityName = "Frankfurt", CountryId = 17 },
                new City { Id = 84, CityName = "Hamburg", CountryId = 17 },
                new City { Id = 85, CityName = "Cologne", CountryId = 17 },
                new City { Id = 86, CityName = "Stuttgart", CountryId = 17 },

                // Greece
                new City { Id = 87, CityName = "Athens", CountryId = 18 },
                new City { Id = 88, CityName = "Thessaloniki", CountryId = 18 },
                new City { Id = 89, CityName = "Heraklion", CountryId = 18 },
                new City { Id = 90, CityName = "Patras", CountryId = 18 },
                new City { Id = 91, CityName = "Rhodes", CountryId = 18 },

                // Hungary
                new City { Id = 92, CityName = "Budapest", CountryId = 19 },
                new City { Id = 93, CityName = "Debrecen", CountryId = 19 },
                new City { Id = 94, CityName = "Szeged", CountryId = 19 },
                new City { Id = 95, CityName = "Miskolc", CountryId = 19 },
                new City { Id = 96, CityName = "Pécs", CountryId = 19 },

                // Iceland
                new City { Id = 97, CityName = "Reykjavik", CountryId = 20 },
                new City { Id = 98, CityName = "Akureyri", CountryId = 20 },
                new City { Id = 99, CityName = "Keflavik", CountryId = 20 },
                new City { Id = 100, CityName = "Hafnarfjörður", CountryId = 20 },
                new City { Id = 101, CityName = "Egilsstaðir", CountryId = 20 },

                // Ireland
                new City { Id = 102, CityName = "Dublin", CountryId = 21 },
                new City { Id = 103, CityName = "Cork", CountryId = 21 },
                new City { Id = 104, CityName = "Galway", CountryId = 21 },
                new City { Id = 105, CityName = "Limerick", CountryId = 21 },
                new City { Id = 106, CityName = "Waterford", CountryId = 21 },

                // Italy
                new City { Id = 107, CityName = "Rome", CountryId = 22 },
                new City { Id = 108, CityName = "Milan", CountryId = 22 },
                new City { Id = 109, CityName = "Venice", CountryId = 22 },
                new City { Id = 110, CityName = "Florence", CountryId = 22 },
                new City { Id = 111, CityName = "Naples", CountryId = 22 },
                new City { Id = 112, CityName = "Turin", CountryId = 22 },

                // Latvia
                new City { Id = 113, CityName = "Riga", CountryId = 23 },
                new City { Id = 114, CityName = "Jurmala", CountryId = 23 },
                new City { Id = 115, CityName = "Daugavpils", CountryId = 23 },
                new City { Id = 116, CityName = "Liepaja", CountryId = 23 },
                new City { Id = 117, CityName = "Ventspils", CountryId = 23 },

                // Liechtenstein
                new City { Id = 118, CityName = "Vaduz", CountryId = 24 },
                new City { Id = 119, CityName = "Schaan", CountryId = 24 },
                new City { Id = 120, CityName = "Balzers", CountryId = 24 },
                new City { Id = 121, CityName = "Triesen", CountryId = 24 },
                new City { Id = 122, CityName = "Eschen", CountryId = 24 },

                // Lithuania
                new City { Id = 123, CityName = "Vilnius", CountryId = 25 },
                new City { Id = 124, CityName = "Kaunas", CountryId = 25 },
                new City { Id = 125, CityName = "Klaipeda", CountryId = 25 },
                new City { Id = 126, CityName = "Šiauliai", CountryId = 25 },
                new City { Id = 127, CityName = "Panevėžys", CountryId = 25 },

                // Luxembourg
                new City { Id = 128, CityName = "Luxembourg City", CountryId = 26 },
                new City { Id = 129, CityName = "Esch-sur-Alzette", CountryId = 26 },
                new City { Id = 130, CityName = "Differdange", CountryId = 26 },
                new City { Id = 131, CityName = "Dudelange", CountryId = 26 },
                new City { Id = 132, CityName = "Ettelbruck", CountryId = 26 },

                // Malta
                new City { Id = 133, CityName = "Valletta", CountryId = 27 },
                new City { Id = 134, CityName = "Sliema", CountryId = 27 },
                new City { Id = 135, CityName = "St. Julian's", CountryId = 27 },
                new City { Id = 136, CityName = "Mellieha", CountryId = 27 },
                new City { Id = 137, CityName = "Mdina", CountryId = 27 },

                // Monaco
                new City { Id = 138, CityName = "Monaco", CountryId = 28 },
                new City { Id = 139, CityName = "Monte Carlo", CountryId = 28 },
                new City { Id = 140, CityName = "La Condamine", CountryId = 28 },

                // Montenegro
                new City { Id = 141, CityName = "Podgorica", CountryId = 29 },
                new City { Id = 142, CityName = "Kotor", CountryId = 29 },
                new City { Id = 143, CityName = "Budva", CountryId = 29 },
                new City { Id = 144, CityName = "Herceg Novi", CountryId = 29 },
                new City { Id = 145, CityName = "Bar", CountryId = 29 },

                // Netherlands
                new City { Id = 146, CityName = "Amsterdam", CountryId = 30 },
                new City { Id = 147, CityName = "Rotterdam", CountryId = 30 },
                new City { Id = 148, CityName = "The Hague", CountryId = 30 },
                new City { Id = 149, CityName = "Utrecht", CountryId = 30 },
                new City { Id = 150, CityName = "Eindhoven", CountryId = 30 },

                // North Macedonia
                new City { Id = 151, CityName = "Skopje", CountryId = 31 },
                new City { Id = 152, CityName = "Ohrid", CountryId = 31 },
                new City { Id = 153, CityName = "Bitola", CountryId = 31 },
                new City { Id = 154, CityName = "Tetovo", CountryId = 31 },
                new City { Id = 155, CityName = "Kumanovo", CountryId = 31 },

                // Norway
                new City { Id = 156, CityName = "Oslo", CountryId = 32 },
                new City { Id = 157, CityName = "Bergen", CountryId = 32 },
                new City { Id = 158, CityName = "Trondheim", CountryId = 32 },
                new City { Id = 159, CityName = "Stavanger", CountryId = 32 },
                new City { Id = 160, CityName = "Tromsø", CountryId = 32 },

                // Poland
                new City { Id = 161, CityName = "Warsaw", CountryId = 33 },
                new City { Id = 162, CityName = "Krakow", CountryId = 33 },
                new City { Id = 163, CityName = "Gdansk", CountryId = 33 },
                new City { Id = 164, CityName = "Wroclaw", CountryId = 33 },
                new City { Id = 165, CityName = "Poznan", CountryId = 33 },

                // Portugal
                new City { Id = 166, CityName = "Lisbon", CountryId = 34 },
                new City { Id = 167, CityName = "Porto", CountryId = 34 },
                new City { Id = 168, CityName = "Faro", CountryId = 34 },
                new City { Id = 169, CityName = "Braga", CountryId = 34 },
                new City { Id = 170, CityName = "Coimbra", CountryId = 34 },

                // Romania
                new City { Id = 171, CityName = "Bucharest", CountryId = 35 },
                new City { Id = 172, CityName = "Cluj-Napoca", CountryId = 35 },
                new City { Id = 173, CityName = "Timișoara", CountryId = 35 },
                new City { Id = 174, CityName = "Iași", CountryId = 35 },
                new City { Id = 175, CityName = "Constanța", CountryId = 35 },

                 //Republic of Moldova
                new City { Id = 176, CityName = "Chișinău", CountryId = 46 },
                new City { Id = 177, CityName = "Bălți", CountryId = 46 },
                new City { Id = 178, CityName = "Tiraspol", CountryId = 46 },
                new City { Id = 179, CityName = "Cahul", CountryId = 46 },
                new City { Id = 180, CityName = "Orhei", CountryId = 46 },

                // Serbia
                new City { Id = 181, CityName = "Belgrade",     CountryId = 36 },
                new City { Id = 182, CityName = "Novi Sad",     CountryId = 36 },
                new City { Id = 183, CityName = "Niš",          CountryId = 36 },
                new City { Id = 184, CityName = "Kragujevac",   CountryId = 36 },
                new City { Id = 185, CityName = "Subotica",     CountryId = 36 },

                // Slovakia
                new City { Id = 186, CityName = "Bratislava",   CountryId = 37 },
                new City { Id = 187, CityName = "Košice",       CountryId = 37 },
                new City { Id = 188, CityName = "Prešov",       CountryId = 37 },
                new City { Id = 189, CityName = "Žilina",       CountryId = 37 },
                new City { Id = 190, CityName = "Nitra",        CountryId = 37 },

                // Slovenia
                new City { Id = 191, CityName = "Ljubljana",    CountryId = 38 },
                new City { Id = 192, CityName = "Maribor",      CountryId = 38 },
                new City { Id = 193, CityName = "Kranj",        CountryId = 38 },
                new City { Id = 194, CityName = "Celje",        CountryId = 38 },
                new City { Id = 195, CityName = "Koper",        CountryId = 38 },

                // Spain
                new City { Id = 196, CityName = "Madrid",       CountryId = 39 },
                new City { Id = 197, CityName = "Barcelona",    CountryId = 39 },
                new City { Id = 198, CityName = "Seville",      CountryId = 39 },
                new City { Id = 199, CityName = "Valencia",     CountryId = 39 },
                new City { Id = 200, CityName = "Bilbao",       CountryId = 39 },

                // Sweden
                new City { Id = 201, CityName = "Stockholm",    CountryId = 40 },
                new City { Id = 202, CityName = "Gothenburg",   CountryId = 40 },
                new City { Id = 203, CityName = "Malmö",        CountryId = 40 },
                new City { Id = 204, CityName = "Uppsala",      CountryId = 40 },
                new City { Id = 205, CityName = "Västerås",     CountryId = 40 },

                // Switzerland
                new City { Id = 206, CityName = "Bern",         CountryId = 41 },
                new City { Id = 207, CityName = "Zurich",       CountryId = 41 },
                new City { Id = 208, CityName = "Geneva",       CountryId = 41 },
                new City { Id = 209, CityName = "Basel",        CountryId = 41 },
                new City { Id = 210, CityName = "Lausanne",     CountryId = 41 },

                // Turkey
                new City { Id = 211, CityName = "Ankara", CountryId = 42 },
                new City { Id = 212, CityName = "Istanbul", CountryId = 42 },
                new City { Id = 213, CityName = "Izmir", CountryId = 42 },
                new City { Id = 214, CityName = "Antalya", CountryId = 42 },
                new City { Id = 215, CityName = "Bursa", CountryId = 42 },

                // Ukraine
                new City { Id = 216, CityName = "Kyiv", CountryId = 43 },
                new City { Id = 217, CityName = "Lviv", CountryId = 43 },
                new City { Id = 218, CityName = "Odessa", CountryId = 43 },
                new City { Id = 219, CityName = "Kharkiv", CountryId = 43 },
                new City { Id = 220, CityName = "Dnipro", CountryId = 43 },

                // United Kingdom
                new City { Id = 221, CityName = "London", CountryId = 44 },
                new City { Id = 222, CityName = "Edinburgh", CountryId = 44 },
                new City { Id = 223, CityName = "Manchester", CountryId = 44 },
                new City { Id = 224, CityName = "Birmingham", CountryId = 44 },
                new City { Id = 225, CityName = "Liverpool", CountryId = 44 },
                new City { Id = 226, CityName = "Glasgow", CountryId = 44 },

                // Vatican City
                new City { Id = 227, CityName = "Vatican", CountryId = 45 },

               
            });



        }
    }
}
