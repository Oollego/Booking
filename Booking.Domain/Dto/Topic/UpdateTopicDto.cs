﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Topic
{
    public class UpdateTopicDto
    {
        public long Id { get; set; }
        public string Titel { get; set; } = null!;
        public string Text { get; set; } = null!;
        public IFormFile? Image { get; set; }
    }
}
