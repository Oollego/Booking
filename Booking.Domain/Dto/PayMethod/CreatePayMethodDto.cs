
namespace Booking.Domain.Dto.PayMethod
{
    public class CreatePayMethodDto
    {
        public string CardNumber { get; set; } = null!;
        public DateTime CardDate { get; set; }
        public long CardTypeId { get; set; }
    }
}
