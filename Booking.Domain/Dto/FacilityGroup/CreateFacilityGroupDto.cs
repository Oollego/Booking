using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.FacilityGroup
{
    public class CreateFacilityGroupDto
    {
        public string FacilityGroupName { get; set; } = null!;
        public IFormFile FacilityGroupIcon { get; set; } = null!;
    }
}
