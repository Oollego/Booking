
namespace Booking.Domain.Dto.PayMethod
{
    public class PayMethodDto
    {
        public long Id { get; set; }
        public string CardNumber { get; set; } = null!;
        public DateTime CardDate { get; set; }
        public long CardTypeId { get; set; }
    }
}
