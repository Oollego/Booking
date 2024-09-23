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
    internal class RoomBedConfiguration : IEntityTypeConfiguration<RoomBed>
    {
        public void Configure(EntityTypeBuilder<RoomBed> builder)
        {
            builder.ToTable("room_beds");
            builder.Property(x => x.BedTypeId).IsRequired();
            builder.Property(x => x.RoomId).IsRequired();

            builder.HasData(new List<RoomBed>()
            {
                new RoomBed { RoomId = 1, BedTypeId = 1 },
                new RoomBed { RoomId = 1, BedTypeId = 2 },
                new RoomBed { RoomId = 2, BedTypeId = 2 },
                new RoomBed { RoomId = 2, BedTypeId = 3 },
                new RoomBed { RoomId = 3, BedTypeId = 3 },
                new RoomBed { RoomId = 4, BedTypeId = 3 },
                new RoomBed { RoomId = 4, BedTypeId = 4 },
                new RoomBed { RoomId = 5, BedTypeId = 4 },
                new RoomBed { RoomId = 6, BedTypeId = 1 },
                new RoomBed { RoomId = 7, BedTypeId = 4 },
                new RoomBed { RoomId = 8, BedTypeId = 2 },
                new RoomBed { RoomId = 9, BedTypeId = 3 },
                new RoomBed { RoomId = 9, BedTypeId = 4 },
                new RoomBed { RoomId = 10, BedTypeId = 2 },
                new RoomBed { RoomId = 11, BedTypeId = 4 },
                new RoomBed { RoomId = 12, BedTypeId = 1 },
                new RoomBed { RoomId = 13, BedTypeId = 4 },
                new RoomBed { RoomId = 14, BedTypeId = 2 },
                new RoomBed { RoomId = 15, BedTypeId = 4 },
                new RoomBed { RoomId = 16, BedTypeId = 2 },
                new RoomBed { RoomId = 17, BedTypeId = 4 },
                new RoomBed { RoomId = 18, BedTypeId = 3 },
                new RoomBed { RoomId = 19, BedTypeId = 4 },
                new RoomBed { RoomId = 20, BedTypeId = 3 },
                new RoomBed { RoomId = 21, BedTypeId = 4 },
                new RoomBed { RoomId = 22, BedTypeId = 2 },
                new RoomBed { RoomId = 23, BedTypeId = 4 },
                new RoomBed { RoomId = 24, BedTypeId = 2 },
                new RoomBed { RoomId = 25, BedTypeId = 4 },
                new RoomBed { RoomId = 26, BedTypeId = 2 },
                new RoomBed { RoomId = 27, BedTypeId = 4 },
                new RoomBed { RoomId = 28, BedTypeId = 3 },
                new RoomBed { RoomId = 29, BedTypeId = 4 },
                new RoomBed { RoomId = 30, BedTypeId = 2 },
                new RoomBed { RoomId = 31, BedTypeId = 4 }
            });
        }


    }
}
