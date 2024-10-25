using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Book
{
    public class CreateBookDto
    {
        public string? BookComment { get; set; } = null!;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Adult { get; set; }
        public int? Children { get; set; }
        public int RoomQuantity { get; set; }
        public bool IsPhoneCall { get; set; } = false;
        public bool IsEmail { get; set; } = false;
        public decimal RoomPrice { get; set; }
        //public DateTime? DateUntilChange { get; set; }
        public long RoomId { get; set; }

    }
}
