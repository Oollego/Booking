using Booking.Domain.Dto.City;
using Booking.Domain.Result;

namespace Booking.Domain.Interfaces.Services
{
    public interface ICityService
    {
        Task<CollectionResult<CityDto>> GetCitiesByCountryIdAsync(long id);
        Task<CollectionResult<CityDto>> SearchCitiesByName(string cityName);
        Task<BaseResult<CityDto>> GetCityByIdAsync(long id);
        Task<BaseResult<CityDto>> CreatCityAsync(CreateCityDto dto);
        Task<BaseResult<CityDto>> DeleteCityAsync(long id);
        Task<BaseResult<CityDto>> UpdateCityAsync(CityDto dto);
    }
}
