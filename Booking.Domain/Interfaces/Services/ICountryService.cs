using Booking.Domain.Dto.Country;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface ICountryService
    {
        Task<CollectionResult<CountryDto>> GetAllCountriesAsync();
        Task<BaseResult<CountryDto>> GetCountryByIdAsync(long id);
        Task<BaseResult<CountryDto>> CreatCountryAsync(CreatCountryDto dto);
        Task<BaseResult<CountryDto>> DeleteCountryAsync(long id);
        Task<BaseResult<CountryDto>> UpdateCountryAsync(CountryDto dto);
    }
}
