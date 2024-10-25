using Booking.Domain.Dto.Bed;
using Booking.Domain.Dto.RoomComfort;
using Booking.Domain.Dto.RoomImage;
using Booking.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Room
{
    public class RoomsDateResponseDto
    {
        public long Id { get; set; }
        public string RoomName { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal CancelationPrice { get; set; }
        public int FixedDays { get; set; }
        public string CurrencyChar { get; set; } = DefaultValues.DefaultCurrancyChar;
        public int RoomsQuantity { get; set; }
        public int FreeRoomsQuantity { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public List<RoomImageDto> Images { get; set; } = null!;
        public List<BedDto> Beds { get; set; } = null!;
        public List<RoomComfortDto> RoomComforts { get; set; } = null!;
    }
}
