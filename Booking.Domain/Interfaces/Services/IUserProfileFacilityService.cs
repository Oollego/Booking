using Booking.Domain.Dto.Facility;
using Booking.Domain.Dto.Topic;
using Booking.Domain.Dto.UserTopicDto;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface IUserProfileFacilityService
    {
        Task<CollectionResult<GroupFacilitiesDto>> GetAllUserFacilitiesAsync(string? email);
        Task<BaseResult<GroupFacilityDto>> GetUserFacilityByIdAsync(long facilityId, string? email);
        Task<CollectionResult<UserFacilityDto>> CreateRangeUserFacilityAsync(IdFacilityDto dto, string? email);
        Task<CollectionResult<UserFacilityDto>> DeleteRangeUserFacilitAsync(IdFacilityDto dto, string? email);
    }
}
