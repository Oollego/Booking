using Booking.Domain.Dto.Facility;
using Booking.Domain.Dto.UserProfile;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface IUserProfileService
    {
        Task<BaseResult<UserProfileDto>> GetUserProfileAsync(string? email);
        Task<BaseResult> UpdateUserProfileAsync(FileUserProfileDto dto, string? email);
        Task<BaseResult> CreateUserProfileAsync(FileUserProfileDto dto, string? email);
        Task<BaseResult> UpdateUserPhoneNumber(string phoneNumber, string? email);
      //Task<BaseResult> DeleteUserProfileAsync(long id, string? email);
    }
}
