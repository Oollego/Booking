using Booking.Domain.Dto.PayMethod;
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
    public interface IPayMethodService
    {
        Task<CollectionResult<PayMethodDto>> GetAllUserPayMethodAsync(string? email);
        Task<BaseResult<PayMethodDto>> GetUserPayMethodByIdAsync(long id, string? email);
        Task<BaseResult<PayMethodDto>> CreatUserPayMethodAsync(CreatePayMethodDto dto, string? email);
        Task<BaseResult<PayMethodDto>> DeleteUserUserPayMethodAsync(long id, string? email);
        Task<BaseResult<PayMethodDto>> UpdatePayMethodAsync(PayMethodDto dto, string? email);
    }
}
