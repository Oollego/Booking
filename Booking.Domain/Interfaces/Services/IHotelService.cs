using Booking.Domain.Dto.Hotel;
using Booking.Domain.Dto.SearchFilter;
using Booking.Domain.Result;

namespace Booking.Domain.Interfaces.Services
{
    public interface IHotelService
    {
        Task<CollectionResult<TopHotelDto>> GetTopHotelsAsync(int qty, int avgReview, string? email);
        Task<BaseResult<InfoHotelDto>> GetHotelInfoAsync(long hotelId, string? email);
        Task<BaseResult<SearchHotelResponseDto>> SearchHotelAsync(SearchHotelDto dto, string? email);
        Task<BaseResult<CreateUpdateHotelDto>> CreateHotelAsync(CreateHotelDto dto);
        Task<BaseResult<SearchFilterResponseDto>> GetSearchFiltersAsync(SearchFilterDto dto);
        Task<BaseResult<CreateUpdateHotelDto>> UpdateHotelAsync(CreateUpdateHotelDto dto);
        Task<CollectionResult<TopHotelDto>> GetHotelsByCityNameAsync(int qty, string cityName, string? email);
    }
}
