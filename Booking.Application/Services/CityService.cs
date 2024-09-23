using AutoMapper;
using Booking.Application.Resources;
using Booking.Domain.Dto.City;
using Booking.Domain.Dto.Country;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class CityService : ICityService
    {
        private readonly IBaseRepository<City> _cityRepository = null!;
        private readonly IBaseRepository<Country> _countryRepository = null!;
        private readonly IMapper _mapper = null!;

        public CityService(IBaseRepository<Country> countryRepository, IBaseRepository<City> cityRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<BaseResult<CityDto>> CreatCityAsync(CreateCityDto dto)
        {
            if (dto == null || dto.CityName == null || dto.CountryId <= 0)
            {
                return new BaseResult<CityDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var country = _countryRepository.GetAll().FirstOrDefault(c => c.Id == dto.CountryId);

            if (country == null)
            {
                return new BaseResult<CityDto>
                {
                    ErrorCode = (int)ErrorCodes.CountryNotFound,
                    ErrorMessage = ErrorMessage.CountryNotFound
                };
            }

            var city = _cityRepository.GetAll()
                .Where(c => c.CountryId == dto.CountryId && c.CityName == dto.CityName)
                .FirstOrDefault();

            if (city != null)
            {
                return new BaseResult<CityDto>
                {
                    ErrorCode = (int)ErrorCodes.CityAlreadyExists,
                    ErrorMessage = ErrorMessage.CityAlreadyExists
                };
            }

            City newCity = new City
            {
               CityName = dto.CityName,
               CountryId = dto.CountryId,
            };

            newCity = await _cityRepository.CreateAsync(newCity);
            await _cityRepository.SaveChangesAsync();

            return new BaseResult<CityDto>
            {
                Data = _mapper.Map<CityDto>(newCity)
            };

        }

        public async Task<BaseResult<CityDto>> DeleteCityAsync(long id)
        {
            if (id <= 0)
            {
                return new BaseResult<CityDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var city = await _cityRepository.GetAll().FirstOrDefaultAsync(c => c.Id == id);

            if (city == null)
            {
                return new BaseResult<CityDto>
                {
                    ErrorCode = (int)ErrorCodes.CityNotFound,
                    ErrorMessage = ErrorMessage.CityNotFound
                };
            }

            _cityRepository.Remove(city);
            await _cityRepository.SaveChangesAsync();

            return new BaseResult<CityDto>
            {
                Data = _mapper.Map<CityDto>(city)
            };

        }

        public async Task<CollectionResult<CityDto>> GetCitiesByCountryIdAsync(long id)
        {
            if (id <= 0)
            {
                return new CollectionResult<CityDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var cities = await _cityRepository.GetAll()
                .Where(c => c.CountryId == id)
                .Select(c => new CityDto
                {
                    Id = c.Id,
                    CityName = c.CityName,
                    CountryId = c.CountryId
                })
                .ToListAsync();

            if (cities == null || cities.Count == 0)
            {
                return new CollectionResult<CityDto>
                {
                    ErrorCode = (int)ErrorCodes.CityNotFound,
                    ErrorMessage = ErrorMessage.CityNotFound
                };
            }

            return new CollectionResult<CityDto>
            {
                Data = cities,
                Count = cities.Count
                
            };
        }

        public async Task<BaseResult<CityDto>> GetCityByIdAsync(long id)
        {
            if (id <= 0)
            {
                return new BaseResult<CityDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var city = await _cityRepository.GetAll()
                .Where(c => c.Id == id)
                .Select(c => new CityDto
                {
                    Id = c.Id,
                    CityName = c.CityName,
                    CountryId = c.CountryId
                })
                .FirstOrDefaultAsync();

            if (city == null)
            {
                return new BaseResult<CityDto>
                {
                    ErrorCode = (int)ErrorCodes.CityNotFound,
                    ErrorMessage = ErrorMessage.CityNotFound
                };
            }

            return new BaseResult<CityDto>
            {
                Data = city
            };
        }

        public async Task<CollectionResult<CityDto>> SearchCitiesByName(string cityName)
        {
            if( cityName == null)
            {
                return new CollectionResult<CityDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var cities = await _cityRepository.GetAll()
                .Where(c => EF.Functions.Like(c.CityName, $"%{cityName}%"))
                .Select(c => new CityDto
                {
                    Id = c.Id,
                    CityName = c.CityName,
                    CountryId = c.CountryId
                })
                .ToListAsync();

            if (cities == null || cities.Count == 0)
            {
                return new CollectionResult<CityDto>
                {
                    ErrorCode = (int)ErrorCodes.CityNotFound,
                    ErrorMessage = ErrorMessage.CityNotFound
                };
            }

            return new CollectionResult<CityDto>
            {
                Data = cities,
                Count = cities.Count
            };
        }

        public async Task<BaseResult<CityDto>> UpdateCityAsync(CityDto dto)
        {
            if (dto == null || dto.Id <= 0 || dto.CountryId <= 0 || dto.CityName == null)
            {
                return new BaseResult<CityDto>
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var city = await _cityRepository.GetAll()
                .FirstOrDefaultAsync(c => c.Id == dto.Id);

            if (city == null)
            {
                return new BaseResult<CityDto>
                {
                    ErrorMessage = ErrorMessage.CityNotFound,
                    ErrorCode = (int)ErrorCodes.CityNotFound
                };
            }

            city.CityName = dto.CityName;
            city.CountryId = dto.CountryId;

            var updatedCity = _cityRepository.Update(city);
            await _cityRepository.SaveChangesAsync();

            return new BaseResult<CityDto>
            {
                Data = _mapper.Map<CityDto>(updatedCity)
            };
        }
    }
}
