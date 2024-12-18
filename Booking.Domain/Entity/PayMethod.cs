﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class PayMethod
    {
        public long Id { get; set; }
        public string CardNumber { get; set; } = null!;
        public DateTime CardDate { get; set; }
        public long CardTypeId { get; set; }
        public long UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; } = null!;
        public CardType CardType { get; set; } = null!;
     }
}
