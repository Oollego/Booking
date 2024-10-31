using AutoMapper;
using Booking.Application.Resources;
using Booking.Domain.Dto.Currency;
using Booking.Domain.Dto.Topic;
using Booking.Domain.Dto.UserTopicDto;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Booking.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        public readonly ILogger _logger = null!;
        public readonly IBaseRepository<Currency> _currencyRepository = null!;
        public readonly IMapper _mapper = null!;

        public CurrencyService(IMapper mapper, IBaseRepository<Currency> currencyRepository, ILogger logger)
        {
            _mapper = mapper;
            _currencyRepository = currencyRepository;
            _logger = logger;
        }

        public async Task<BaseResult<CurrencyDto>> CreatCurrencyAsync(CurrencyDto dto)
        {
            if (dto.CurrencyCode == null || dto.CurrencyCode == "" || dto.CurrencyChar == "" 
                || dto.CurrencyChar == null || dto.CurrencyName == null || dto.CurrencyName == "" || dto.ExchangeRate > 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<CurrencyDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var currency = await _currencyRepository.GetAll()
                .Where(c => c.CurrencyCode == dto.CurrencyCode || c.CurrencyName == dto.CurrencyName)
                .FirstOrDefaultAsync();


            if (currency != null)
            {
                _logger.Warning(ErrorMessage.CurrencyAlreadyExists);
                return new BaseResult<CurrencyDto>
                {
                    ErrorCode = (int)ErrorCodes.CurrencyAlreadyExists,
                    ErrorMessage = ErrorMessage.CurrencyAlreadyExists
                };
            }

            var newCurrency = new Currency()
            {
                CurrencyCode = dto.CurrencyCode,
                CurrencyName = dto.CurrencyName,
                ExchangeRate = dto.ExchangeRate,
                CurrencyChar = dto.CurrencyChar
            };
            
            currency = await _currencyRepository.CreateAsync(newCurrency);
            await _currencyRepository.SaveChangesAsync();

            return new BaseResult<CurrencyDto>()
            {
                Data = _mapper.Map<CurrencyDto>(currency)
            };

        }

        public async Task<BaseResult<CurrencyDto>> DeleteCurrencyAsync(string code)
        {
            if (code == null || (code.Length != 3 ))
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<CurrencyDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var currency = await _currencyRepository.GetAll()
                .Where( c => c.CurrencyCode == code )
                .FirstOrDefaultAsync();


            if (currency == null)
            {
                _logger.Warning(ErrorMessage.CurrencyNotFound);
                return new BaseResult<CurrencyDto>
                {
                    ErrorCode = (int)ErrorCodes.CurrencyNotFound,
                    ErrorMessage = ErrorMessage.CurrencyNotFound
                };
            }
  
            currency = _currencyRepository.Remove(currency);
            await _currencyRepository.SaveChangesAsync();

            return new BaseResult<CurrencyDto>()
            {
                Data = _mapper.Map<CurrencyDto>(currency)
            };
        }

        public async Task<CollectionResult<CurrencyDto>> GetAllCurrenciesAsync()
        {
           var currencies = await _currencyRepository.GetAll()
                .Select(c => new CurrencyDto()
                   {
                       CurrencyCode = c.CurrencyCode,
                       CurrencyName = c.CurrencyName,
                       CurrencyChar = c.CurrencyChar,
                       ExchangeRate = c.ExchangeRate,
                   }).ToListAsync();

            if (currencies == null)
            {
                _logger.Warning(ErrorMessage.CurrencyNotFound);
                return new CollectionResult<CurrencyDto>
                {
                    ErrorCode = (int)ErrorCodes.CurrencyNotFound,
                    ErrorMessage = ErrorMessage.CurrencyNotFound
                };
            }

            return new CollectionResult<CurrencyDto>()
            {
                Count = currencies.Count,
                Data = currencies
            };
        }

        public async Task<BaseResult<CurrencyDto>> GetCurrencyByIdAsync(string code)
        {
            if (code == null || (code.Length != 3))
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<CurrencyDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var currency = await _currencyRepository.GetAll()
                .Where(c => c.CurrencyCode == code)
                .Select(c => new CurrencyDto
                {
                    CurrencyCode = c.CurrencyCode,
                    CurrencyName = c.CurrencyName,
                    CurrencyChar = c.CurrencyChar,
                    ExchangeRate = c.ExchangeRate
                })
                .FirstOrDefaultAsync();

            if (currency == null)
            {
                _logger.Warning(ErrorMessage.CurrencyNotFound);
                return new BaseResult<CurrencyDto>
                {
                    ErrorCode = (int)ErrorCodes.CurrencyNotFound,
                    ErrorMessage = ErrorMessage.CurrencyNotFound
                };
            }

            return new BaseResult<CurrencyDto>()
            {
                Data = currency
            };
        }

        public async Task<BaseResult<CurrencyDto>> UpdateCurrencyRateAsync(UpdateCurrencyDto dto)
        {
            if (dto.ExchangeRate <= 0 || dto.CurrencyCode == null || dto.CurrencyCode.Length != 3)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<CurrencyDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var currency = await _currencyRepository.GetAll()
                .Where(c => c.CurrencyCode == dto.CurrencyCode).FirstOrDefaultAsync();

            if (currency == null)
            {
                _logger.Warning(ErrorMessage.CurrencyNotFound);
                return new BaseResult<CurrencyDto>
                {
                    ErrorCode = (int)ErrorCodes.CurrencyNotFound,
                    ErrorMessage = ErrorMessage.CurrencyNotFound
                };
            }

            currency.ExchangeRate = dto.ExchangeRate;

            currency = _currencyRepository.Update(currency);
            await _currencyRepository.SaveChangesAsync();

            return new BaseResult<CurrencyDto>()
            {
                Data = _mapper.Map<CurrencyDto>(currency)
            };
        }


    }
}
