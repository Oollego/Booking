using AutoMapper;
using Booking.Application.Resources;
using Booking.Domain.Dto.Country;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.EntityFrameworkCore;
using MimeKit.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Booking.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IBaseRepository<Country> _countryRepository = null!;
        private readonly IMapper _mapper = null!;

        public CountryService(IBaseRepository<Country> countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }


        public async Task<BaseResult<CountryDto>> CreatCountryAsync(CreatCountryDto dto)
        {
            if (dto == null || dto.Name == null)
            {
                return new BaseResult<CountryDto>
                {
                     ErrorCode = (int)ErrorCodes.InvalidParameters,
                     ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var country = _countryRepository.GetAll()
                .Where(c => c.CountryName == dto.Name)
                .FirstOrDefault();

            if (country != null)
            {
                return new BaseResult<CountryDto>
                {
                    ErrorCode = (int)ErrorCodes.CountryAlreadyExists,
                    ErrorMessage = ErrorMessage.CountryAlreadyExists
                };
            }

            Country newCountry = new Country
            {
                CountryName = dto.Name,
                Flag = dto.Icon
            };

            newCountry = await _countryRepository.CreateAsync(newCountry);
            await _countryRepository.SaveChangesAsync();

            return new BaseResult<CountryDto>
            {
                Data = _mapper.Map<CountryDto>(newCountry)
            };

        }

        public async Task<BaseResult<CountryDto>> DeleteCountryAsync(long id)
        {
           if(id <= 0)
            {
                return new BaseResult<CountryDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

           var country = await _countryRepository.GetAll().FirstOrDefaultAsync(c => c.Id == id);

            if (country == null)
            {
                return new BaseResult<CountryDto>
                {
                    ErrorCode = (int)ErrorCodes.CountryNotFound,
                    ErrorMessage = ErrorMessage.CountryNotFound
                };
            }

            _countryRepository.Remove(country);
            await _countryRepository.SaveChangesAsync();

            return new BaseResult<CountryDto>
            {
                Data = _mapper.Map<CountryDto>(country)
            };

        }

        public async Task<CollectionResult<CountryDto>> GetAllCountriesAsync()
        {
            var countries = await _countryRepository.GetAll().Select(c => new CountryDto
            {
                Id = c.Id,
                CountryName = c.CountryName,
                Flag = c.Flag
            }).ToListAsync();

            if (countries == null || countries.Count == 0)
            {
                return new CollectionResult<CountryDto>
                {
                    ErrorCode = (int)ErrorCodes.CountryNotFound,
                    ErrorMessage = ErrorMessage.CountryNotFound
                };
            }

            return new CollectionResult<CountryDto>
            {
                Data = countries,
                Count = countries.Count               
            };
        }

        public async Task<BaseResult<CountryDto>> GetCountryByIdAsync(long id)
        {
            if(id <= 0)
            {
                return new BaseResult<CountryDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var country = await _countryRepository.GetAll()
                .Where(c => c.Id == id)
                .Select(c => new CountryDto
            {
                Id = c.Id,
                CountryName = c.CountryName,
                Flag = c.Flag
            }).FirstOrDefaultAsync();

            if (country == null)
            {
                return new BaseResult<CountryDto>
                {
                    ErrorCode = (int)ErrorCodes.CountryNotFound,
                    ErrorMessage = ErrorMessage.CountryNotFound
                };
            }

            return new BaseResult<CountryDto>
            {
                Data = country
            };
        }

        public async Task<BaseResult<CountryDto>> UpdateCountryAsync(CountryDto dto)
        {
            if(dto == null || dto.Id <=0 || dto.CountryName == null)
            {
                return new BaseResult<CountryDto>
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var country = await _countryRepository.GetAll()
                .Where(c => c.Id == dto.Id).FirstOrDefaultAsync();

            if (country == null) 
            {
                return new BaseResult<CountryDto>
                {
                    ErrorMessage = ErrorMessage.CountryNotFound,
                    ErrorCode = (int)ErrorCodes.CountryNotFound
                };
            }

            country.CountryName = dto.CountryName;
            country.Flag = dto.Flag;

            var updatedCountry = _countryRepository.Update(country);
            await _countryRepository.SaveChangesAsync();

            return new BaseResult<CountryDto>
            {
                Data = _mapper.Map<CountryDto>(updatedCountry)
            };
        }
    }
}
