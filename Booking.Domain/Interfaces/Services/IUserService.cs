using Booking.Domain.Dto.User;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<BaseResult> UpdateUserEmailAsync(string newEmail, string? email);
        Task<BaseResult> ConfirmNewEmailAsync(ConfirmRegisterDto dto, string? email);
        Task<BaseResult> ChangeUserPasswordAsync(PassDto dto, string? email);
        
    }
}
