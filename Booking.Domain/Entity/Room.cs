using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class Room : IEntityId<long>
    {
        public long Id { get; set; }
        public string RoomName { get; set; } = null!;
        public decimal RoomPrice { get; set; }
        public decimal Cancellation { get; set; }
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public List<RoomImage> RoomImages { get; set; } = null!;
        public List<BedType> BedTypes { get; set; } = null!;
        public List<Book> Books { get; set; } = null!;
        public List<RoomComfortIconType> RoomComfortIconTypes { get; set; } = null!;
    }
}
