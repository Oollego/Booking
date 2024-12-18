﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.SearchFilter
{
    public class HotelChainFilterDto
    {
        public long Id { get; set; }
        public string ChainName { get; set; } = null!;
        public int Matches { get; set; }
    }
}
