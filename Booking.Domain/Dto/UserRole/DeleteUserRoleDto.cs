using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.UserRole
{
    public class DeleteUserRoleDto
    {
        public string Email { get; set; } = null!;
        public long RoleId { get; set; }
    }
}
