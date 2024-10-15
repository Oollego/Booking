using Booking.Domain.Dto.Currency;
using Booking.Domain.Entity;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface ICurrencyService
    {
        Task<BaseResult<CurrencyDto>> GetCurrencyByIdAsync(string code);
        Task<CollectionResult<CurrencyDto>> GetAllCurrenciesAsync();
        Task<BaseResult<CurrencyDto>> CreatCurrencyAsync(CurrencyDto dto);
        Task<BaseResult<CurrencyDto>> DeleteCurrencyAsync(string code);
        Task<BaseResult<CurrencyDto>> UpdateCurrencyRateAsync(UpdateCurrencyDto dto);
    }
}
