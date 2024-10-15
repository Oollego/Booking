using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.CardType
{
    public class CardTypeDto
    {
        public long Id { get; set; }
        public string CardName { get; set; } = null!;
    }
}
