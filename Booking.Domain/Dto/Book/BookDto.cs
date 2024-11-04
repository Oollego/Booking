using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Book
{
    public class BookDto
    {
        public long Id { get; set; }
        public string BookingCode { get; set; } = null!;
        public string? BookComment { get; set; } = null!;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Adult { get; set; }
        public int? Children { get; set; }
        public int RoomQuantity { get; set; }
        public string? BookingEmail { get; set; } = null!;
        public bool IsPhoneCall { get; set; } = false;
        public bool IsEmail { get; set; } = false;
        public decimal RoomPrice { get; set; }
        public DateTime? DateUntilChange { get; set; }
        public long RoomId { get; set; }
        public long HotelId { get; set; }
        public string HotelName { get; set; } = null!;
        public long CityId { get; set; }
        public string CityName { get; set; } = null!;
        public long UserId { get; set; }
    }
}
