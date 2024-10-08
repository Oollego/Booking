using Booking.Domain.Dto.Country;
using Booking.Domain.Dto.Reason;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface IReasonService
    {
        Task<CollectionResult<ReasonDto>> GetAllReasonsAsync();
        Task<BaseResult<ReasonDto>> GetReasonByIdAsync(long id);
        Task<BaseResult<ReasonDto>> CreatReasonAsync(string reason);
        Task<BaseResult<ReasonDto>> DeleteReasonAsync(long id);
        Task<BaseResult<ReasonDto>> UpdateReasonAsync(ReasonDto dto);
    }
}
