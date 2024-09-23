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
    internal class RoomComfortIconConfiguration : IEntityTypeConfiguration<RoomComfortIcon>
    {
        public void Configure(EntityTypeBuilder<RoomComfortIcon> builder)
        {
            builder.ToTable("room_comfort_icons");
            builder.Property(x => x.RoomId).IsRequired();
            builder.Property(x => x.RoomComfortIconTypeId).IsRequired();

            builder.HasData(new List<RoomComfortIcon>
            {
                // Comfort icons for Single Room (RoomId = 1)
                new RoomComfortIcon { RoomId = 1, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 1, RoomComfortIconTypeId = 2 }, // bath

                // Comfort icons for Double Room (RoomId = 2)
                new RoomComfortIcon { RoomId = 2, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 2, RoomComfortIconTypeId = 2 }, // bath
                new RoomComfortIcon { RoomId = 2, RoomComfortIconTypeId = 3 }, // private pool

                // Comfort icons for Suite (RoomId = 3)
                new RoomComfortIcon { RoomId = 3, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 3, RoomComfortIconTypeId = 3 }, // private pool

                // Comfort icons for Deluxe Room (RoomId = 4)
                new RoomComfortIcon { RoomId = 4, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 4, RoomComfortIconTypeId = 2 }, // bath

                // Comfort icons for Family Room (RoomId = 5)
                new RoomComfortIcon { RoomId = 5, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 5, RoomComfortIconTypeId = 3 }, // private pool

                // Comfort icons for Standard Room (RoomId = 6)
                new RoomComfortIcon { RoomId = 6, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 6, RoomComfortIconTypeId = 2 }, // bath

                // Comfort icons for Executive Suite (RoomId = 7)
                new RoomComfortIcon { RoomId = 7, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 7, RoomComfortIconTypeId = 3 }, // private pool

                // Comfort icons for Mountain View Room (RoomId = 8)
                new RoomComfortIcon { RoomId = 8, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 8, RoomComfortIconTypeId = 2 }, // bath

                // Comfort icons for Ski Suite (RoomId = 9)
                new RoomComfortIcon { RoomId = 9, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 9, RoomComfortIconTypeId = 2 }, // bath
                new RoomComfortIcon { RoomId = 9, RoomComfortIconTypeId = 3 }, // private pool

                // Comfort icons for Oceanfront Room (RoomId = 10)
                new RoomComfortIcon { RoomId = 10, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 10, RoomComfortIconTypeId = 2 }, // bath

                // Comfort icons for Luxury Suite (RoomId = 11)
                new RoomComfortIcon { RoomId = 11, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 11, RoomComfortIconTypeId = 2 }, // bath
                new RoomComfortIcon { RoomId = 11, RoomComfortIconTypeId = 3 }, // private pool

                // Comfort icons for Classic Room (RoomId = 12)
                new RoomComfortIcon { RoomId = 12, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 12, RoomComfortIconTypeId = 2 }, // bath

                // Comfort icons for Royal Suite (RoomId = 13)
                new RoomComfortIcon { RoomId = 13, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 13, RoomComfortIconTypeId = 3 }, // private pool

                // Comfort icons for River View Room (RoomId = 14)
                new RoomComfortIcon { RoomId = 14, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 14, RoomComfortIconTypeId = 2 }, // bath

                // Comfort icons for Deluxe River Suite (RoomId = 15)
                new RoomComfortIcon { RoomId = 15, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 15, RoomComfortIconTypeId = 3 }, // private pool

                // Comfort icons for Park View Room (RoomId = 16)
                new RoomComfortIcon { RoomId = 16, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 16, RoomComfortIconTypeId = 2 }, // bath

                // Comfort icons for Luxury Park Suite (RoomId = 17)
                new RoomComfortIcon { RoomId = 17, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 17, RoomComfortIconTypeId = 2 }, // bath
                new RoomComfortIcon { RoomId = 17, RoomComfortIconTypeId = 3 }, // private pool

                // Comfort icons for Beachfront Room (RoomId = 18)
                new RoomComfortIcon { RoomId = 18, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 18, RoomComfortIconTypeId = 2 }, // bath

                // Comfort icons for Seaside Suite (RoomId = 19)
                new RoomComfortIcon { RoomId = 19, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 19, RoomComfortIconTypeId = 2 }, // bath
                new RoomComfortIcon { RoomId = 19, RoomComfortIconTypeId = 3 }, // private pool

                // Comfort icons for Grand Room (RoomId = 20)
                new RoomComfortIcon { RoomId = 20, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 20, RoomComfortIconTypeId = 2 }, // bath

                // Comfort icons for Historic Suite (RoomId = 21)
                new RoomComfortIcon { RoomId = 21, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 21, RoomComfortIconTypeId = 2 }, // bath
                new RoomComfortIcon { RoomId = 21, RoomComfortIconTypeId = 3 }, // private pool

                // Comfort icons for City View Room (RoomId = 22)
                new RoomComfortIcon { RoomId = 22, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 22, RoomComfortIconTypeId = 2 }, // bath

                // Comfort icons for Palace Suite (RoomId = 23)
                new RoomComfortIcon { RoomId = 23, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 23, RoomComfortIconTypeId = 2 }, // bath
                new RoomComfortIcon { RoomId = 23, RoomComfortIconTypeId = 3 }, // private pool

                // Comfort icons for Boutique Room (RoomId = 24)
                new RoomComfortIcon { RoomId = 24, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 24, RoomComfortIconTypeId = 2 }, // bath

                // Comfort icons for Gran Via Suite (RoomId = 25)
                new RoomComfortIcon { RoomId = 25, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 25, RoomComfortIconTypeId = 2 }, // bath
                new RoomComfortIcon { RoomId = 25, RoomComfortIconTypeId = 3 }, // private pool

                // Comfort icons for Lakeview Room (RoomId = 26)
                new RoomComfortIcon { RoomId = 26, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 26, RoomComfortIconTypeId = 2 }, // bath

                // Comfort icons for Lakefront Suite (RoomId = 27)
                new RoomComfortIcon { RoomId = 27, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 27, RoomComfortIconTypeId = 2 }, // bath
                new RoomComfortIcon { RoomId = 27, RoomComfortIconTypeId = 3 }, // private pool

                // Comfort icons for Ocean Breeze Room (RoomId = 28)
                new RoomComfortIcon { RoomId = 28, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 28, RoomComfortIconTypeId = 2 }, // bath

                // Comfort icons for Business Suite (RoomId = 29)
                new RoomComfortIcon { RoomId = 29, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 29, RoomComfortIconTypeId = 2 }, // bath
                new RoomComfortIcon { RoomId = 29, RoomComfortIconTypeId = 3 }, // private pool

                // Comfort icons for Countryside Room (RoomId = 30)
                new RoomComfortIcon { RoomId = 30, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 30, RoomComfortIconTypeId = 2 }, // bath

                // Comfort icons for Rustic Suite (RoomId = 31)
                new RoomComfortIcon { RoomId = 31, RoomComfortIconTypeId = 1 }, // free wi-fi
                new RoomComfortIcon { RoomId = 31, RoomComfortIconTypeId = 2 }, // bath
                new RoomComfortIcon { RoomId = 31, RoomComfortIconTypeId = 3 }, // private pool
            });
        }
    }
}
