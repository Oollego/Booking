using Booking.Domain.Dto.Faq;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface IFaqService
    {
        Task<CollectionResult<FaqDto>> GetAllHotelFaqs(long hotelId);
        Task<BaseResult<FaqFullDto>> GetFaq(long id);
        Task<BaseResult<FaqFullDto>> UpdateFaq(FaqFullDto dto);
        Task<BaseResult> DeleteFaq(long id);
        Task<BaseResult<long>> CreateFaq(CreateFaqDto dto);
    }
}
