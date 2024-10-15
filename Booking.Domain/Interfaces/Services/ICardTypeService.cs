using Booking.Domain.Dto.CardType;
using Booking.Domain.Result;

namespace Booking.Domain.Interfaces.Services
{
    public interface ICardTypeService
    {
        Task<BaseResult<CardTypeDto>> GetCardTypeByIdAsync(long id);
        Task<CollectionResult<CardTypeDto>> GetAllCardTypesAsync();
        Task<BaseResult<CardTypeDto>> CreatCardTypeAsync(CreateCardTypeDto dto);
        Task<BaseResult<CardTypeDto>> DeleteCardTypeAsync(long id);
        Task<BaseResult<CardTypeDto>> UpdateCardTypeAsync(CardTypeDto dto);
    }
}
