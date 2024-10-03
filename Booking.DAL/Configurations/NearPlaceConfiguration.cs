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
    internal class NearPlaceConfiguration : IEntityTypeConfiguration<NearPlace>
    {
        public void Configure(EntityTypeBuilder<NearPlace> builder)
        {
            builder.ToTable("near_places");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PlaceName).IsRequired().HasMaxLength(254);
            builder.Property(x => x.Distance).IsRequired();
            builder.Property(x => x.DistanceMetric).HasDefaultValue(false);
            builder.Property(x => x.NearPlacesGroupId).IsRequired();

            builder.HasData(GenerateNearPlaceData());
        }

        private List<NearPlace> GenerateNearPlaceData()
        {
            var nearPlaces = new List<NearPlace>();
            int id = 1;

            int[] s = {1, 2, 9, 12, 14};
            int[] g = { 3, 4, 10, 13, 15 };
            int[] u = { 5, 6, 7, 8, 11 };

            foreach (int i in s) 
            {
                var temp = new List<NearPlace>{
                    new NearPlace { Id = id++, PlaceName = "The city center", Distance = 800, DistanceMetric = false, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "La Sagrada Familia", Distance = 3.2, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Park Güell", Distance = 4.5, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Casa Batlló", Distance = 2.9, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Picasso Museum", Distance = 1.5, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Magic Fountain of Montjuïc", Distance = 3.6, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Barceloneta Beach", Distance = 700, DistanceMetric = false, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Gothic Quarter", Distance = 1.8, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Montjuïc Castle", Distance = 4.3, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Palau de la Música", Distance = 2.1, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "La Rambla", Distance = 2.0, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Camp Nou", Distance = 5.6, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Poble Espanyol", Distance = 3.8, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Tibidabo Amusement Park", Distance = 6.9, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Ciutadella Park", Distance = 1.4, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Barcelona Zoo", Distance = 1.5, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },

                    new NearPlace { Id = id++, PlaceName = "Tickets Tapas Bar", Distance = 2.5, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Cervecería Catalana", Distance = 2.1, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Can Culleretes", Distance = 1.7, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "7 Portes", Distance = 1.4, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Quimet & Quimet", Distance = 2.9, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "El Xampanyet", Distance = 1.3, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "La Paradeta", Distance = 900, DistanceMetric = false, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Bar Mut", Distance = 2.6, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },

                    new NearPlace { Id = id++, PlaceName = "Passeig de Gràcia Metro", Distance = 2.3, DistanceMetric = true, NearPlacesGroupId = 3, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Barceloneta Metro", Distance = 800, DistanceMetric = false, NearPlacesGroupId = 3, HotelId = i },

                    new NearPlace { Id = id++, PlaceName = "Barcelona El Prat Airport", Distance = 13.0, DistanceMetric = true, NearPlacesGroupId = 4, HotelId = i },

                    new NearPlace { Id = id++, PlaceName = "Sants Train Station", Distance = 4.0, DistanceMetric = true, NearPlacesGroupId = 6, HotelId = i },

                    new NearPlace { Id = id++, PlaceName = "Barceloneta Beach", Distance = 700, DistanceMetric = false, NearPlacesGroupId = 5, HotelId = i }
                };

                nearPlaces = nearPlaces.Concat(temp).ToList();
            }

            foreach (int i in g)
            {
                var temp = new List<NearPlace>{
                    new NearPlace { Id = id++, PlaceName = "The city center", Distance = 1.5, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Brandenburg Gate", Distance = 1.2, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Berlin Wall Memorial", Distance = 2.5, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Museum Island", Distance = 0.9, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Alexanderplatz", Distance = 1.5, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Checkpoint Charlie", Distance = 0.7, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Potsdamer Platz", Distance = 1.3, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Berlin Cathedral", Distance = 0.8, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Reichstag Building", Distance = 1.1, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Gendarmenmarkt", Distance = 0.5, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Tiergarten Park", Distance = 2.0, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Jewish Museum", Distance = 1.7, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Victory Column", Distance = 3.0, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Berlin TV Tower", Distance = 1.5, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Berlin Zoological Garden", Distance = 4.0, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "East Side Gallery", Distance = 3.5, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },

                    new NearPlace { Id = id++, PlaceName = "Curry 36", Distance = 2.0, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Prater Garten", Distance = 3.5, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Zur Letzten Instanz", Distance = 1.2, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Tim Raue", Distance = 1.0, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Monsieur Vuong", Distance = 0.9, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Rutz", Distance = 1.6, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Grill Royal", Distance = 0.8, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Facil", Distance = 1.2, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },

                    new NearPlace { Id = id++, PlaceName = "Friedrichstrasse Station", Distance = 0.5, DistanceMetric = true, NearPlacesGroupId = 3, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Alexanderplatz Metro", Distance = 1.5, DistanceMetric = true, NearPlacesGroupId = 3, HotelId = i },

                    new NearPlace { Id = id++, PlaceName = "Hauptbahnhof Central Station", Distance = 1.8, DistanceMetric = true, NearPlacesGroupId = 6, HotelId = i },

                    new NearPlace { Id = id++, PlaceName = "Berlin Brandenburg Airport", Distance = 18.0, DistanceMetric = true, NearPlacesGroupId = 4, HotelId = i }
                };
                nearPlaces = nearPlaces.Concat(temp).ToList();
            }

            foreach (int i in u)
            {
                var temp = new List<NearPlace>
                {
                    new NearPlace { Id = id++, PlaceName = "The city center", Distance = 500, DistanceMetric = false, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "St. Sophia's Cathedral", Distance = 0.8, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Kyiv Pechersk Lavra", Distance = 4.0, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Golden Gate", Distance = 1.0, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Khreshchatyk Street", Distance = 0.2, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Independence Square", Distance = 0.1, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Andriyivskyy Descent", Distance = 1.5, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Motherland Monument", Distance = 5.0, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Mariyinsky Palace", Distance = 1.8, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "National Art Museum", Distance = 0.9, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "National Opera House", Distance = 1.0, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "St. Michael's Golden-Domed Monastery", Distance = 1.2, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Besarabsky Market", Distance = 0.5, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Taras Shevchenko Park", Distance = 1.5, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Kyiv Funicular", Distance = 1.4, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Holosiivskyi National Park", Distance = 6.0, DistanceMetric = true, NearPlacesGroupId = 1, HotelId = i },

                    new NearPlace { Id = id++, PlaceName = "Kanapa Restaurant", Distance = 1.6, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Osteria Pantagruel", Distance = 0.9, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Puzata Hata", Distance = 0.2, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "The Last Barricade", Distance = 0.1, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Spotykach", Distance = 1.3, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Kyivska Perepichka", Distance = 0.5, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Zheltoe Café", Distance = 0.8, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },
                    new NearPlace { Id = id++, PlaceName = "Under Wonder", Distance = 1.1, DistanceMetric = true, NearPlacesGroupId = 2, HotelId = i },

                    new NearPlace { Id = id++, PlaceName = "Maidan Nezalezhnosti Metro Station", Distance = 0.1, DistanceMetric = true, NearPlacesGroupId = 3, HotelId = i },

                    new NearPlace { Id = id++, PlaceName = "Kyiv-Pasazhyrskyi Train Station", Distance = 3.0, DistanceMetric = true, NearPlacesGroupId = 6, HotelId = i },

                    new NearPlace { Id = id++, PlaceName = "Boryspil International Airport", Distance = 35.0, DistanceMetric = true, NearPlacesGroupId = 4, HotelId = i }
                };
                nearPlaces = nearPlaces.Concat(temp).ToList();
            }

            return nearPlaces;
        }
    }
}
