using Booking.Domain.Dto.FacilityGroup;
using Booking.Domain.Result;

namespace Booking.Domain.Interfaces.Services
{
    public interface IFacilityGroupService
    {
        Task<BaseResult<FacilityGroupDto>> GetFacilityGroupByIdAsync(long id);
        Task<CollectionResult<FacilityGroupDto>> GetAllFacilityGroupsAsync();
        Task<BaseResult<FacilityGroupDto>> CreatFacilityGroupAsync(CreateFacilityGroupDto dto);
        Task<BaseResult<FacilityGroupDto>> DeleteFacilityGroupAsync(long id);
        Task<BaseResult<FacilityGroupDto>> UpdateFacilityGroupAsync(UpdateFacilityGroupDto dto);
    }
}
