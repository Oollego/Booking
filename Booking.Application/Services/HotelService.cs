using AutoMapper;
using Booking.Application.Resources;
using Booking.Domain.Dto.City;
using Booking.Domain.Dto.Hotel;
using Booking.Domain.Dto.HotelComfort;
using Booking.Domain.Dto.NearStation;
using Booking.Domain.Dto.Review;
using Booking.Domain.Dto.SearchFilter;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Interfaces.UnitsOfWork;
using Booking.Domain.Interfaces.Validations;
using Booking.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Booking.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IBaseRepository<Hotel> _hotelRepository = null!;
        private readonly IBaseRepository<User> _userRepository = null!;
        private readonly IBaseRepository<Room> _roomRepository = null!;
        private readonly IMapper _mapper = null!;
        private readonly IHotelUnitOfWork _hotelUnitOfWork = null!;
        private readonly IHotelCreateUpdateValidator _hotelCreateUpdateValidator = null!;

        private readonly ILogger _logger = null!;

        public HotelService(IBaseRepository<Hotel> hotelRepository, IBaseRepository<User> userRepository,
            IBaseRepository<Room> roomRepository, IMapper mapper, IHotelUnitOfWork hotelUnitOfWork, ILogger logger, 
            IHotelCreateUpdateValidator hotelCreateUpdateValidator)
        {
            _hotelRepository = hotelRepository;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
            _hotelUnitOfWork = hotelUnitOfWork;
            _logger = logger;
            _hotelCreateUpdateValidator = hotelCreateUpdateValidator;
        }



        public async Task<BaseResult<InfoHotelDto>> GetHotelInfoAsync(long hotelId, string? email)
        {
            if (hotelId < 1)
            {
                return new BaseResult<InfoHotelDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var hotel = await _hotelRepository.GetAll()
                .Where(x => x.Id == hotelId)
                .Include(x => x.Reviews)
                .Include(x => x.Rooms)
                    .ThenInclude(x => x.RoomImages)
                .Include(x => x.HotelLabelTypes)
                .Select(x => new InfoHotelDto
                {
                    Id = x.Id,
                    HotelName = x.HotelName,
                    HotelAddress = x.HotelAddress,
                    HotelPhone = x.HotelPhone,
                    Stars = x.Stars,
                    HotelImage = x.HotelImage,
                    Description = x.Description,
                    ReviewQty = x.Reviews.Count(),
                    CheapestRoom = x.Rooms.Min(x => x.RoomPrice),
                    HotelLabels = x.HotelLabelTypes.Select(x => new HotelInfoLabelDto
                    {
                        LabelName = x.LabelName,
                        LabelIcon = x.LabelIcon,
                    }).ToList(),
                    Rating =
                        Math.Round(x.Reviews.Average(r =>
                            (r.FacilityScore + r.StaffScore + r.CleanlinessScore + r.ComfortScore + r.LocationScore + r.ValueScore) / 6), 1),
                    Score = new ScoreDto {
                        FacilityScore = Math.Round(x.Reviews.Average(x => x.FacilityScore), 1),
                        StaffScore = Math.Round(x.Reviews.Average(x => x.StaffScore), 1),
                        CleanlinessScore = Math.Round(x.Reviews.Average(x => x.CleanlinessScore), 1),
                        ComfortScore = Math.Round(x.Reviews.Average(x => x.ComfortScore), 1),
                        LocationScore = Math.Round(x.Reviews.Average(x => x.LocationScore), 1),
                        ValueScore = Math.Round(x.Reviews.Average(x => x.ValueScore), 1)
                    },
                    Images = x.Rooms.SelectMany(r => r.RoomImages.Select(ri => ri.ImageName)).ToList(),
                }).FirstOrDefaultAsync();

            if (hotel == null)
            {
                _logger.Warning(ErrorMessage.HotelNotFound);
                return new BaseResult<InfoHotelDto>()
                {
                    ErrorMessage = ErrorMessage.HotelNotFound,
                    ErrorCode = (int)ErrorCodes.HotelNotFound
                };
            }

            if (email != null)
            {
               
                 var currancy = await _userRepository.GetAll()
                    .Where(u => u.UserEmail == email)
                    .Include(x => x.UserProfile).ThenInclude(x => x.Currency)
                    .Select(x => new { x.UserProfile.Currency!.CurrencyChar, x.UserProfile.Currency.ExchangeRate })
                    .FirstOrDefaultAsync();
                if (currancy != null)
                {
                    hotel.CheapestRoom = Math.Round(hotel.CheapestRoom / (decimal)currancy.ExchangeRate);
                    hotel.CurrencyChar = currancy.CurrencyChar;
                }
            }

            return new BaseResult<InfoHotelDto>()
            {
                Data = hotel,
            };
        }
        public async Task<CollectionResult<TopHotelDto>> GetTopHotelsAsync(int qty, int avgReview)
        {
            if (avgReview < 1 || avgReview > 10 || qty < 1 )
            {
                return new CollectionResult<TopHotelDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var hotels = await _hotelRepository.GetAll()
                .Include(rev => rev.Reviews)
                .Include(city => city.City).ThenInclude(city => city.Country)
                .Include(place => place.NearStations.Where(np => np.NearPlaceNameId == 4)).ThenInclude(pln => pln.NearStationName)
                .Include(room => room.Rooms)
                .Select(h => new TopHotelDto
                {
                    HotelId = h.Id,
                    Rating =
                        Math.Round(h.Reviews.Average(g => 
                        (g.FacilityScore + g.StaffScore + g.CleanlinessScore + g.ComfortScore + g.LocationScore + g.ValueScore)/6 ), 1),
                    ReviewsQt = h.Reviews.Count(),
                    HotelName = h.HotelName,
                    HotelCity = h.City.CityName,
                    HotelCountry = h.City.Country.CountryName,
                    Stars = h.Stars,
                    MinPrice =
                      h.Rooms.Min(x => x.RoomPrice),
                    HotelImage = h.HotelImage,
                    DistanceToCityCenter = h.NearStations.FirstOrDefault()!.Distance,
                    DistanceMetric = h.NearStations.FirstOrDefault()!.DistanceMetric

                })
                .OrderByDescending(x => x.ReviewsQt)
                .Take(qty).Where(x => x.Rating > avgReview)
                .ToListAsync();

 
            if ( hotels.Count == 0 )
            {
                _logger.Warning(ErrorMessage.HotelNotFound, hotels.Count);
                return new CollectionResult<TopHotelDto>()
                {
                    ErrorMessage = ErrorMessage.HotelNotFound,
                    ErrorCode = (int)ErrorCodes.HotelNotFound
                };
            }

            if (hotels == null)
            {
                _logger.Warning(ErrorMessage.HotelNotFound);
                return new CollectionResult<TopHotelDto>()
                {
                    ErrorMessage = ErrorMessage.HotelNotFound,
                    ErrorCode = (int)ErrorCodes.HotelNotFound
                };
            }

            return new CollectionResult<TopHotelDto>()
            {
                Data = hotels,
                Count = hotels.Count
            };
        }
        public async Task<BaseResult<SearchFilterResponseDto>> GetSearchFiltersAsync(SearchFilterDto dto)
        {
            if (dto.CheckIn.Date < DateTime.UtcNow.Date || dto.CheckIn > dto.CheckOut ||
               dto.Adults < 1 || dto.Children < 0 || dto.Rooms < 1)
            {
                return new BaseResult<SearchFilterResponseDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var queryHotels = _hotelRepository.GetAll().AsNoTracking()
              .Include(h => h.Rooms)
                  .ThenInclude(r => r.BedTypes)
              .Include(h => h.Rooms)
                  .ThenInclude(r => r.Books)
              .Include(h => h.City)
                  .ThenInclude(c => c.Country)
              .Include(h => h.HotelData)
              .Include(h => h.HotelLabelTypes)
              .Include(h => h.NearStations)
                  .ThenInclude(ns => ns.NearStationName)
               .Where(h =>
                  (dto.Place == null || dto.Place == "" || h.City.CityName == dto.Place || h.City.Country.CountryName == dto.Place) &&
                  h.Rooms.Any(r =>
                      (r.BedTypes.Sum(bd => bd.Adult) >= dto.Adults && r.BedTypes.Sum(bd => bd.Children) >= dto.Children) ||
                      r.BedTypes.Sum(bd => bd.Adult) >= (dto.Adults + dto.Children)) &&
                  h.Rooms.Count(r => !r.Books.Any(b => dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn)) >= dto.Rooms
               );

            var rating = await queryHotels.GroupBy(h => Math.Floor(h.HotelData.Rating))
                .Select(g => new RatingFilterDto
                {
                    Rating = g.Key,               
                    Matches = g.Count()    
                }).OrderByDescending(x => x.Rating).ToListAsync();

            var labels = await queryHotels.SelectMany(h => h.HotelLabelTypes)
                .GroupBy(l => l.LabelName)
                .Select(g => new LabelFilterDto
                {
                    LabelName = g.Key,                
                    Matches = g.Count()
                }).OrderBy(x => x.LabelName).ToListAsync();

            var stars = await queryHotels.GroupBy(h => h.Stars)
                .Select(g => new StarFilterDto
                {
                    Star = g.Key,
                    Matches = g.Count()
                }).OrderByDescending(x => x.Star).ToListAsync();

            var nearPlaces = await queryHotels.SelectMany(h => h.NearStations)
                .GroupBy(ns => ns.NearStationName.Name)
                .Select(g => new NearPlaceFilterDto
                {
                    PlaceName = g.Key,
                    Matches = g.Count()
                }).OrderBy(x => x.PlaceName).ToListAsync();

            var facilities = await queryHotels.SelectMany(h => h.Facilities)
                .GroupBy(f => f.FacilityName)
                .Select(g => new FacilityFilterDto
                {
                    FacilityName = g.Key,
                    Matches = g.Count()
                }).OrderBy(x => x.FacilityName).ToListAsync();

            var hotelTypes = await queryHotels.GroupBy(h => h.HotelType.HotelTypeName)
                .Select(g => new HotelTypeFilterDto
                {
                    TypeName = g.Key,
                    Matches = g.Count()
                }).OrderBy(x => x.TypeName).ToListAsync();

            var chains = await queryHotels.GroupBy(f => f.HotelChain.HotelChainName)
                .Select(g => new HotelChainFilterDto
                {
                    ChainName = g.Key,
                    Matches = g.Count()
                }).OrderBy(x => x.ChainName).ToListAsync();

            var filters = new SearchFilterResponseDto();

            filters.Ratings = rating;
            filters.Labels = labels;
            filters.NearPlaces = nearPlaces;
            filters.Stars = stars;
            filters.Facilities = facilities;
            filters.HotelChains = chains;
            filters.HotelTypes = hotelTypes;

             return new BaseResult<SearchFilterResponseDto>()
            {
                Data = filters
            };
        }
        public async Task<BaseResult<SearchHotelResponseDto>> SearchHotelAsync(SearchHotelDto dto)
        {
            if (dto.CheckIn.Date < DateTime.UtcNow.Date || dto.CheckIn > dto.CheckOut ||
                dto.Adults < 1 || dto.Children < 0 || dto.Rooms < 1 || dto.Page < 1)
            {
                return new BaseResult<SearchHotelResponseDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var queryHotels = _hotelRepository.GetAllAsSplitQuery().AsNoTracking()
                .Include(h => h.Rooms)
                    .ThenInclude(r => r.BedTypes)
                .Include(h => h.Rooms)
                    .ThenInclude(r => r.Books)
                .Include(h => h.City)
                    .ThenInclude(c => c.Country)
                .Include(h => h.HotelData)
                .Include(h => h.HotelLabelTypes)
                .Include(h => h.NearStations)
                    .ThenInclude(ns => ns.NearStationName)
                 .Where(h =>
                    (dto.Place == null || dto.Place == "" || h.City.CityName == dto.Place || h.City.Country.CountryName == dto.Place) &&
                    h.Rooms.Any(r =>
                        (r.BedTypes.Sum(bd => bd.Adult) >= dto.Adults && r.BedTypes.Sum(bd => bd.Children) >= dto.Children) ||
                        r.BedTypes.Sum(bd => bd.Adult) >= (dto.Adults + dto.Children)) &&
                    h.Rooms.Count(r => !r.Books.Any(b => dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn)) >= dto.Rooms)
                 .Where(h => (dto.Stars == null || dto.Stars.Count == 0 || dto.Stars[0] == 0 || dto.Stars.Contains(h.Stars)))
                 .Where(h => dto.Facilities == null || dto.Facilities.Count == 0 || dto.Facilities[0] == 0 || h.Facilities.Any(f => dto.Facilities.Contains(f.Id)))
                 .Where(h => dto.Rating == null || dto.Rating.Count == 0 || dto.Rating[0] == 0 || (h.HotelData != null && dto.Rating.Contains(Math.Floor(h.HotelData.Rating))))
                 .Where(h => dto.NearPlaces == null || dto.NearPlaces.Count == 0 || dto.NearPlaces[0] == 0 || h.NearStations.Any(s => dto.NearPlaces.Contains(s.NearPlaceNameId)))
                 .Where(h => dto.HotelTypes == null || dto.HotelTypes.Count == 0 || dto.HotelTypes[0] == 0 || dto.HotelTypes.Any(t => t == h.HotelTypeId))
                 .Where(h => dto.HotelChains == null || dto.HotelChains.Count == 0 || dto.HotelChains[0] == 0 || dto.HotelChains.Any(c => c == h.HotelChainId))
                 .Where(h => dto.HotelLabels == null || dto.HotelLabels.Count == 0 || dto.HotelLabels[0] == 0 || h.HotelLabelTypes.Any(f => dto.HotelLabels.All(x => x == h.Id))
                ).Select(x => new HotelDto
                {
                    Id = x.Id,
                    HotelName = x.HotelName,
                    HotelAddress = x.HotelAddress,
                    HotelPhone = x.HotelPhone,
                    HotelImage = x.HotelImage,
                    Description = x.Description,
                    FixedDays = x.FixedDays,
                    Star = x.Stars,
                    Rating = x.HotelData.Rating,
                    ReviewQty = x.HotelData.ReviewCount,
                    MinRoomPrice = x.HotelData.HotelMinRoomPrice,
                    HotelLabels = x.HotelLabelTypes.Select(hct => new HotelInfoLabelDto
                    {
                        LabelName = hct.LabelName,
                        LabelIcon = hct.LabelIcon
                    }).ToList(),
                    NearPlaces = x.NearStations.Select(s => new NearStationDto
                    {
                        Id = s.Id,
                        StationName = s.NearStationName.Name,
                        Distance = s.Distance,
                        DistanceMetric = s.DistanceMetric,
                        StationIcon = s.NearStationName.Icon!
                    }).ToList(),
                }).OrderBy(h => h.HotelName);
            //var hotels = await queryHotels.ToListAsync();
            var hotels = await queryHotels.Skip((dto.Page - 1) * dto.HotelQty).Take(dto.HotelQty).ToListAsync();

            //    var queryHotels = _hotelRepository.GetAll().AsNoTracking()
            //.Include(h => h.Rooms)
            //    .ThenInclude(r => r.BedTypes)
            //.Include(h => h.Rooms)
            //    .ThenInclude(r => r.Books)
            //.Include(h => h.City)
            //    .ThenInclude(c => c.Country)
            //.Include(h => h.HotelData)
            //.Include(h => h.HotelLabelTypes)
            //.Include(h => h.NearStations)
            //    .ThenInclude(ns => ns.NearStationName)
            // .Where(h =>
            //    (dto.Place == null || dto.Place == "" || h.City.CityName == dto.Place || h.City.Country.CountryName == dto.Place) &&
            //    h.Rooms.Any(r =>
            //        (r.BedTypes.Sum(bd => bd.Adult) >= dto.Adults && r.BedTypes.Sum(bd => bd.Children) >= dto.Children) ||
            //        r.BedTypes.Sum(bd => bd.Adult) >= (dto.Adults + dto.Children)) &&
            //    h.Rooms.Count(r => !r.Books.Any(b => dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn)) >= dto.Rooms)
            // .Where(h => (dto.Stars == null || dto.Stars.Count == 0 || dto.Stars[0] == 0 || dto.Stars.Contains(h.Stars)))
            // //.Where(h => dto.Facilities == null || dto.Facilities.Count == 0 || dto.Facilities[0] == 0 || h.Facilities.Any(f => dto.Facilities.Contains(f.Id)))
            // .Where(h => dto.Rating == null || dto.Rating.Count == 0 || dto.Rating[0] == 0 || (h.HotelData != null && dto.Rating.Contains(h.HotelData.Rating)))
            // .Where(h => dto.NearPlaces == null || dto.NearPlaces.Count == 0 || dto.NearPlaces[0] == 0 || h.NearStations.Any(s => dto.NearPlaces.Contains(s.NearPlaceNameId)))
            // .Where(h => dto.HotelTypes == null || dto.HotelTypes.Count == 0 || dto.HotelTypes[0] == 0 || dto.HotelTypes.Any(t => t == h.HotelTypeId))
            // .Where(h => dto.HotelChains == null || dto.HotelChains.Count == 0 || dto.HotelChains[0] == 0 || dto.HotelChains.Any(c => c == h.HotelChainId))
            // .Where(h => dto.HotelLabels == null || dto.HotelLabels.Count == 0 || dto.HotelLabels[0] == 0 || h.HotelLabelTypes.Any(f => dto.HotelLabels.All(x => x == h.Id))
            //).OrderBy(h => h.HotelName);


            //queryHotels = queryHotels.Where(h => dto.Facilities == null || _hotelRepository.FromSqlRaw(
            //       @"
            //        SELECT h.ID FROM booking.Hotels h
            //        JOIN near_stations ns ON h.Id = ns.HotelId
            //        WHERE h.Id = {0}",
            //        h.Id, string.Join("AND ns.NearPlaceNameId == ", dto.NearPlaces)
            //       ).Any());
            //.Where(h => h.Id == null || _hotelRepository.FromSqlRaw(
            //@"SELECT h.Id 
            //  FROM Hotels h
            //  JOIN NearStations ns ON h.Id = ns.HotelId
            //  WHERE h.Id = {0}
            //  GROUP BY h.Id
            //  HAVING COUNT(DISTINCT ns.Id) = (SELECT COUNT(*) FROM NearStations ns2 WHERE ns2.Id IN ({1}))",
            //  h.Id, string.Join(",", dto.NearPlaces)) // Converting dto.NearPlaces list to comma-separated IDs for raw SQL
            //.Any())

            // .Select(x => new HotelDto
            //{
            //    Id = x.Id,
            //    HotelName = x.HotelName,
            //    HotelAddress = x.HotelAddress,
            //    HotelPhone = x.HotelPhone,
            //    HotelImage = x.HotelImage,
            //    Description = x.Description,
            //    FixedDays = x.FixedDays,
            //    Star = x.Stars,
            //    Rating = x.HotelData.Rating,
            //    ReviewQty = x.HotelData.ReviewCount,
            //    MinRoomPrice = x.HotelData.HotelMinRoomPrice,
            //    HotelLabels = x.HotelLabelTypes.Select(hct => new HotelInfoLabelDto
            //    {
            //        LabelName = hct.LabelName,
            //        LabelIcon = hct.LabelIcon
            //    }).ToList(),
            //    NearPlaces = x.NearStations.Select(s => new NearStationDto
            //    {
            //        Id = s.Id,
            //        StationName = s.NearStationName.Name,
            //        Distance = s.Distance,
            //        DistanceMetric = s.DistanceMetric,
            //        StationIcon = s.NearStationName.Icon!
            //    }).ToList(),
            //});

            //var hotels = await queryHotels.Skip(dto.PageNumber * dto.PageQty).Take(dto.PageQty).ToListAsync();


            if (hotels.Count == 0)
            {
                _logger.Warning(ErrorMessage.HotelNotFound, hotels.Count);
                return new BaseResult<SearchHotelResponseDto>()
                {
                    ErrorMessage = ErrorMessage.HotelNotFound,
                    ErrorCode = (int)ErrorCodes.HotelNotFound
                };
            }

            if (hotels == null)
            {
                _logger.Warning(ErrorMessage.HotelNotFound);
                return new BaseResult<SearchHotelResponseDto>()
                {
                    ErrorMessage = ErrorMessage.HotelNotFound,
                    ErrorCode = (int)ErrorCodes.HotelNotFound
                };
            }

            SearchHotelResponseDto hotelResponse = new SearchHotelResponseDto
            {
                Matches = queryHotels.Count(),
                Hotels = hotels,
                Count = hotels.Count()
            };


            return new BaseResult<SearchHotelResponseDto>()
            {
                Data = hotelResponse
            };
        }
        public async Task<BaseResult<CreateUpdateHotelDto>> CreateHotelAsync(CreateHotelDto dto)
        {
            if (dto.Stars < 0 || dto.FixedDays < 0 || dto.CityId < 0 || dto == null)
            {
                return new BaseResult<CreateUpdateHotelDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var hotel = await _hotelRepository.GetAll().Where(h => h.HotelName == dto.HotelName).FirstOrDefaultAsync();

            if (hotel != null)
            {
                return new BaseResult<CreateUpdateHotelDto>()
                {
                    ErrorMessage = ErrorMessage.HotelAlreadyExists,
                    ErrorCode = (int)ErrorCodes.HotelAlreadyExists
                };
            }

            hotel = new Hotel
            {
                HotelName = dto.HotelName,
                HotelAddress = dto.HotelAddress,
                HotelPhone = dto.HotelPhone,
                HotelImage = dto.HotelImage,
                CityGuide = dto.CityGuide,
                Description = dto.Description,
                Stars = dto.Stars,
                FixedDays = dto.FixedDays,
                IsPet = dto.IsPet,
                CityId = dto.CityId,
                HotelTypeId = dto.HotelTypeId,
                HotelChainId = dto.HotelChainId
                
            };
            using (var transaction = await _hotelUnitOfWork.BeginTransactionAsync()) 
            {
                try
                {
                    hotel = await _hotelUnitOfWork.Hotels.CreateAsync(hotel);
                    await _hotelUnitOfWork.SaveChangesAsync();

                    HotelData hotelData = new HotelData
                    {
                        HotelId = hotel.Id
                    };

                    await _hotelUnitOfWork.HotelsData.CreateAsync(hotelData);
                    await _hotelUnitOfWork.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch(Exception)
                {
                   await transaction.RollbackAsync();
                }
            } 
            

            return new BaseResult<CreateUpdateHotelDto>()
            {
                Data = _mapper.Map<CreateUpdateHotelDto>(hotel)
            };
        }
        public async Task<BaseResult<CreateUpdateHotelDto>> UpdateHotelAsync(CreateUpdateHotelDto dto)
        {
           
            var validatorResult = _hotelCreateUpdateValidator.CreateUpdateDtoValidator(dto);

            if (!validatorResult.IsSuccess)
            {
                return new BaseResult<CreateUpdateHotelDto>()
                {
                    ErrorMessage = validatorResult.ErrorMessage,
                    ErrorCode = validatorResult.ErrorCode,
                };
            }

            var hotel = await _hotelRepository.GetAll()
                .FirstOrDefaultAsync(c => c.Id == dto.Id);

            if (hotel == null)
            {
                return new BaseResult<CreateUpdateHotelDto>
                {
                    ErrorMessage = ErrorMessage.HotelNotFound,
                    ErrorCode = (int)ErrorCodes.HotelNotFound
                };
            }

            hotel.HotelName = dto.HotelName;
            hotel.HotelAddress = dto.HotelAddress;
            hotel.HotelPhone = dto.HotelPhone;
            hotel.HotelImage = dto.HotelImage;
            hotel.CityGuide = dto.CityGuide;
            hotel.Description = dto.Description;
            hotel.FixedDays = dto.FixedDays;
            hotel.IsPet = dto.IsPet;
            hotel.CityId = dto.CityId;
            hotel.HotelChainId = hotel.HotelChainId;
            hotel.HotelTypeId = dto.HotelTypeId;

            var updatedHotel = _hotelRepository.Update(hotel);
            await _hotelRepository.SaveChangesAsync();

            return new BaseResult<CreateUpdateHotelDto>
            {
                Data = _mapper.Map<CreateUpdateHotelDto>(updatedHotel)
            };
        }
    }
}
