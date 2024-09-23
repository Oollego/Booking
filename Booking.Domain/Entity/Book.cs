using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class Book
    {
        public long Id { get; set; }
        public string? BookComment { get; set; } = null!;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool IsPhoneCall { get; set; } = false;
        public bool IsEmail { get; set; } = false;
        public decimal RoomBookPrice {  get; set; }
        public DateTime? DateUntilChange { get; set; }
        public DateTime BookDate { get; set; } = DateTime.Now;
        public long RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public long UserId { get; set; }
        public User User { get; set; } = null!;

    }
}
