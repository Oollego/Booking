using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class CardType
    {
        public long Id { get; set; }
        public string CardName { get; set; } = null!;
        public List<PayMethod> PayMethods { get; set; } = null!;
    }
}
