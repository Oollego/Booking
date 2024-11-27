namespace Booking.Application.Services.ServiceDto
{
    public class ConfirmationEmailData
    {
        public string Address { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public DateTime ChangeDate { get; set; }
        public int RoomQuantity { get; set; }
        public decimal RoomPrice { get; set; } = default!;
        public int Adults { get; set; }
        public int Children { get; set; }
        public string Image { get; set; } = default!; 
    }
}
