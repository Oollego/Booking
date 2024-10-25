using Amazon.S3.Model;
using AutoMapper;
using Booking.Application.Resources;
using Booking.Application.Services.ServiceEntity;
using Booking.Domain.Dto.City;
using Booking.Domain.Dto.Hotel;
using Booking.Domain.Dto.HotelComfort;
using Booking.Domain.Dto.NearObject;
using Booking.Domain.Dto.Review;
using Booking.Domain.Dto.SearchFilter;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Converters;
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
using System.Runtime.InteropServices;
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
        private readonly IImageToLinkConverter _imageToLinkConverter = null!;

        private readonly ILogger _logger = null!;

        public HotelService(IBaseRepository<Hotel> hotelRepository, IBaseRepository<User> userRepository,
            IBaseRepository<Room> roomRepository, IMapper mapper, IHotelUnitOfWork hotelUnitOfWork, ILogger logger,
            IHotelCreateUpdateValidator hotelCreateUpdateValidator, IImageToLinkConverter imageToLinkConverter)
        {
            _hotelRepository = hotelRepository;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
            _hotelUnitOfWork = hotelUnitOfWork;
            _logger = logger;
            _hotelCreateUpdateValidator = hotelCreateUpdateValidator;
            _imageToLinkConverter = imageToLinkConverter;
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
                    HotelImage = _imageToLinkConverter.ConvertImageToLink(x.HotelImage, S3Folders.HotelsImg),
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

                var currancy = await GetUserCurrencyAsync(email);

                if (currancy != null)
                {
                    hotel.CheapestRoom = Math.Round(hotel.CheapestRoom / (decimal)currancy.ExchangeRate);
                    hotel.CurrencyChar = currancy.CurrencyChar;
                }
            }

            hotel.Images = _imageToLinkConverter.ConvertImagesToLink(hotel.Images, S3Folders.HotelsImg);

            return new BaseResult<InfoHotelDto>()
            {
                Data = hotel,
            };
        }
        public async Task<CollectionResult<TopHotelDto>> GetHotelsByCityNameAsync(int qty, string cityName, string? email)
        {
            if (cityName == null || cityName == "" || qty < 1)
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
                .Where(h => h.City.CityName == cityName)
                .Include(place => place.NearObjects.Where(np => np.NearObjectNameId == 4)).ThenInclude(pln => pln.NearObjectName)
                .Include(room => room.Rooms)
                .Select(h => new TopHotelDto
                {
                    HotelId = h.Id,
                    Rating =
                        Math.Round(h.Reviews.Average(g =>
                        (g.FacilityScore + g.StaffScore + g.CleanlinessScore + g.ComfortScore + g.LocationScore + g.ValueScore) / 6), 1),
                    ReviewsQt = h.Reviews.Count(),
                    HotelName = h.HotelName,
                    HotelCity = h.City.CityName,
                    HotelCountry = h.City.Country.CountryName,
                    Stars = h.Stars,
                    MinPrice =
                      h.Rooms.Min(x => x.RoomPrice),
                    HotelImage = _imageToLinkConverter.ConvertImageToLink(h.HotelImage, S3Folders.HotelsImg),
                    DistanceToCityCenter = h.NearObjects.Where(no => no.NearObjectName.Name == "the city center").FirstOrDefault()!.Distance,
                    DistanceMetric = h.NearObjects.Where(no => no.NearObjectName.Name == "the city center").FirstOrDefault()!.DistanceMetric

                })
                .OrderByDescending(x => x.ReviewsQt)
                .Take(qty)
                .ToListAsync();


            if (hotels.Count == 0)
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

            if (email != null)
            {
                var currancy = await GetUserCurrencyAsync(email);

                if (currancy != null)
                {
                    hotels.ForEach(x => 
                    { 
                        x.MinPrice = Math.Round(x.MinPrice / (decimal)currancy.ExchangeRate);
                        x.CurrencyChar = currancy.CurrencyChar;
                    });
                }
            }

            return new CollectionResult<TopHotelDto>()
            {
                Data = hotels,
                Count = hotels.Count
            };
        }
        public async Task<CollectionResult<TopHotelDto>> GetTopHotelsAsync(int qty, int avgReview, string? email)
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
                .Include(place => place.NearObjects.Where(np => np.NearObjectNameId == 4)).ThenInclude(pln => pln.NearObjectName)
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
                    HotelImage = _imageToLinkConverter.ConvertImageToLink(h.HotelImage, S3Folders.HotelsImg),
                    DistanceToCityCenter = h.NearObjects.Where(no => no.NearObjectName.Name == "the city center").FirstOrDefault()!.Distance,
                    DistanceMetric = h.NearObjects.Where(no => no.NearObjectName.Name == "the city center").FirstOrDefault()!.DistanceMetric

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

            if (email != null)
            {
                var currancy = await GetUserCurrencyAsync(email);

                if (currancy != null)
                {
                    hotels.ForEach(x =>
                    {
                        x.MinPrice = Math.Round(x.MinPrice / (decimal)currancy.ExchangeRate);
                        x.CurrencyChar = currancy.CurrencyChar;
                    });
                }
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



            //////var price = await queryHotels.Select(x => new
            //////{
            //////    MinPrice = x.Rooms.Any() ? x.Rooms.Min(r => (decimal?)r.RoomPrice) : null,
            //////    MaxPrice = x.Rooms.Any() ? x.Rooms.Max(r => (decimal?)r.RoomPrice) : null
            //////}).FirstOrDefaultAsync();

            //var queryHotels = _roomRepository.GetAllAsSplitQuery().AsNoTracking()
            //   .Include(r => r.BedTypes)
            //   .Include(b => b.Books)
            //   .Include(r => r.Hotel)
            //       .ThenInclude(h => h.City)
            //       .ThenInclude(c => c.Country)
            //   .Include(r => r.Hotel)
            //       .ThenInclude(h => h.HotelData)
            //   .Include(r => r.Hotel)
            //       .ThenInclude(h => h.HotelLabelTypes)
            //   .Include(r => r.Hotel)
            //       .ThenInclude(h => h.NearObjects)
            //       .ThenInclude(no => no.NearObjectName)
            //   .Where(r =>
            //        (dto.Place == null || dto.Place == "" || r.Hotel.City.CityName == dto.Place || r.Hotel.City.Country.CountryName == dto.Place))
            //   .Where(r => ((r.BedTypes.Sum(bd => bd.Adult) * dto.Rooms) >= dto.Adults && (r.BedTypes.Sum(bd => bd.Children) * dto.Rooms) >= dto.Children) ||
            //        (r.BedTypes.Sum(bd => bd.Adult) * dto.Rooms) >= (dto.Adults + dto.Children))
            //   .Where(r => r.RoomQuantity - r.Books.Count(b => b.RoomId == r.Id && dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn) >= dto.Rooms);

            //var minPrice = await queryHotels.MinAsync(x => x.RoomPrice);
            //var maxPrice = await queryHotels.MaxAsync(x => x.RoomPrice);

            //var rating = await queryHotels.GroupBy(r => Math.Floor(r.Hotel.HotelData.Rating))
            //    .Select(g => new RatingFilterDto
            //    {
            //        Rating = g.Key,
            //        Matches = g.Count()
            //    }).OrderByDescending(x => x.Rating).ToListAsync();

            //var labels = await queryHotels.SelectMany(r => r.Hotel.HotelLabelTypes)
            //    .GroupBy(l => l)
            //    .Select(g => new LabelFilterDto
            //    {
            //        Id = g.Key.Id,
            //        LabelName = g.Key.LabelName,
            //        Matches = g.Count()
            //    }).OrderBy(x => x.LabelName).ToListAsync();

            //var stars = await queryHotels.GroupBy(r => r.Hotel.Stars)
            //    .Select(g => new StarFilterDto
            //    {
            //        Star = g.Key,
            //        Matches = g.Count()
            //    }).OrderByDescending(x => x.Star).ToListAsync();


            //var nearPlaces = await queryHotels.SelectMany(r => r.Hotel.NearObjects)
            //    .GroupBy(ns => ns.NearObjectName)
            //    .Select(g => new NearPlaceFilterDto
            //    {
            //        Id = g.Key.Id,
            //        PlaceName = g.Key.Name,
            //        Matches = g.Count()
            //    }).OrderBy(x => x.PlaceName).ToListAsync();

            //var facilities = await queryHotels.SelectMany(r => r.Hotel.Facilities)
            //    .GroupBy(f => f)
            //    .Select(g => new FacilityFilterDto
            //    {
            //        Id = g.Key.Id,
            //        FacilityName = g.Key.FacilityName,
            //        Matches = g.Count()
            //    }).OrderBy(x => x.FacilityName).ToListAsync();

            //var hotelTypes = await queryHotels.GroupBy(r => r.Hotel.HotelType)
            //    .Select(g => new HotelTypeFilterDto
            //    {
            //        Id = g.Key.Id,
            //        TypeName = g.Key.HotelTypeName,
            //        Matches = g.Count()
            //    }).OrderBy(x => x.TypeName).ToListAsync();

            //var chains = await queryHotels.GroupBy(f => f.Hotel.HotelChain)
            //    .Select(g => new HotelChainFilterDto
            //    {
            //        Id = g.Key.Id,
            //        ChainName = g.Key.HotelChainName,
            //        Matches = g.Count()
            //    }).OrderBy(x => x.ChainName).ToListAsync();



            //var queryHotels = _hotelRepository.GetAll().AsNoTracking()
            //  .Include(h => h.Rooms)
            //      .ThenInclude(r => r.BedTypes)
            //  .Include(h => h.Rooms)
            //      .ThenInclude(r => r.Books)
            //  .Include(h => h.City)
            //      .ThenInclude(c => c.Country)
            //  .Include(h => h.HotelData)
            //  .Include(h => h.HotelLabelTypes)
            //  .Include(h => h.NearObjects)
            //      .ThenInclude(ns => ns.NearObjectName)
            //   .Where(h =>
            //      (dto.Place == null || dto.Place == "" || h.City.CityName == dto.Place || h.City.Country.CountryName == dto.Place) &&
            //      h.Rooms.Any(r =>
            //          (r.BedTypes.Sum(bd => bd.Adult) >= dto.Adults && r.BedTypes.Sum(bd => bd.Children) >= dto.Children) ||
            //          r.BedTypes.Sum(bd => bd.Adult) >= (dto.Adults + dto.Children)))
            //    .Where(h => h.Rooms.Sum(r => r.RoomQuantity) -
            //           h.Rooms.Sum(r =>
            //           r.Books.Where(b => dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn).Sum(b => b.RoomQuantity)) >= dto.Rooms

            //   //h.Rooms.Count(r => !r.Books.Any(b => dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn)) >= dto.Rooms
            //   );
            var queryHotels = _hotelRepository.GetAllAsSplitQuery().AsNoTracking()
               .Include(h => h.Rooms)
                   .ThenInclude(r => r.BedTypes)
               .Include(h => h.Rooms)
                   .ThenInclude(r => r.Books)
               .Include(h => h.City)
                   .ThenInclude(c => c.Country)
               .Include(h => h.HotelData)
               .Include(h => h.HotelLabelTypes)
               .Include(h => h.NearObjects)
                   .ThenInclude(ns => ns.NearObjectName)
                .Where(h =>
                   (dto.Place == null || dto.Place == "" || h.City.CityName == dto.Place || h.City.Country.CountryName == dto.Place))
                 .Where(h => h.Rooms.Any(r => (r.BedTypes.Sum(bd => bd.Adult) * dto.Rooms) >= dto.Adults && (r.BedTypes.Sum(bd => bd.Children) * dto.Rooms) >= dto.Children ||
                               (r.BedTypes.Sum(bd => bd.Adult) * dto.Rooms) >= (dto.Adults + dto.Children)))
                .Where(h => h.Rooms.Any(r => r.RoomQuantity - r.Books.Count(b => b.RoomId == r.Id && dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn) >= dto.Rooms))
                .Where(h => h.Rooms.Count(r => !r.Books.Any(b => dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn)) >= dto.Rooms);


            var minPrice = await queryHotels.Select(h => h.HotelData.HotelMinRoomPrice).MinAsync();
            var maxPrice = await queryHotels.Select(h => h.HotelData.HotelMinRoomPrice).MaxAsync();

            var rating = await queryHotels.GroupBy(h => Math.Floor(h.HotelData.Rating))
                .Select(g => new RatingFilterDto
                {
                    Rating = g.Key,
                    Matches = g.Count()
                }).OrderByDescending(x => x.Rating).ToListAsync();

            var labels = await queryHotels.SelectMany(h => h.HotelLabelTypes)
                .GroupBy(l => l)
                .Select(g => new LabelFilterDto
                {
                    Id = g.Key.Id,
                    LabelName = g.Key.LabelName,
                    Matches = g.Count()
                }).OrderBy(x => x.LabelName).ToListAsync();

            var stars = await queryHotels.GroupBy(h => h.Stars)
                .Select(g => new StarFilterDto
                {
                    Star = g.Key,
                    Matches = g.Count()
                }).OrderByDescending(x => x.Star).ToListAsync();

            var nearPlaces = await queryHotels.SelectMany(h => h.NearObjects)
                .GroupBy(ns => ns.NearObjectName)
                .Select(g => new NearPlaceFilterDto
                {
                    Id = g.Key.Id,
                    PlaceName = g.Key.Name,
                    Matches = g.Count()
                }).OrderBy(x => x.PlaceName).ToListAsync();

            var facilities = await queryHotels.SelectMany(h => h.Facilities)
                .GroupBy(f => f)
                .Select(g => new FacilityFilterDto
                {
                    Id = g.Key.Id,
                    FacilityName = g.Key.FacilityName,
                    Matches = g.Count()
                }).OrderBy(x => x.FacilityName).ToListAsync();

            var hotelTypes = await queryHotels.GroupBy(h => h.HotelType)
                .Select(g => new HotelTypeFilterDto
                {
                    Id = g.Key.Id,
                    TypeName = g.Key.HotelTypeName,
                    Matches = g.Count()
                }).OrderBy(x => x.TypeName).ToListAsync();

            var chains = await queryHotels.GroupBy(f => f.HotelChain)
                .Select(g => new HotelChainFilterDto
                {
                    Id = g.Key.Id,
                    ChainName = g.Key.HotelChainName,
                    Matches = g.Count()
                }).OrderBy(x => x.ChainName).ToListAsync();

            var filters = new SearchFilterResponseDto();


            //    filters.MinPrice = Math.Round(price.MinPrice ?? 0, 2);
            //    filters.MaxPrice = Math.Round(price.MaxPrice ?? 0, 2);


            filters.MinPrice = Math.Round(minPrice, 2);
            filters.MaxPrice = Math.Round(maxPrice, 2);


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
        public async Task<BaseResult<SearchHotelResponseDto>> SearchHotelAsync(SearchHotelDto dto, string? email)
        {
            if (dto.CheckIn.Date < DateTime.UtcNow.Date || dto.CheckIn > dto.CheckOut ||
                dto.Adults < 1 || dto.Children < 0 || dto.Rooms < 1 || dto.Page < 1 || 
                dto.MinPrice < 0)
            {
                return new BaseResult<SearchHotelResponseDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            if (dto.MaxPrice < dto.MinPrice)
            {
                dto.MaxPrice = int.MaxValue;
            }
            //    var queryHotels = _roomRepository.GetAllAsSplitQuery().AsNoTracking()
            //        .Include(r => r.BedTypes)
            //        .Include(b => b.Books)
            //        .Include(r => r.Hotel)
            //            .ThenInclude(h => h.City)
            //            .ThenInclude(c => c.Country)
            //        .Include(r => r.Hotel)
            //            .ThenInclude(h => h.HotelData)
            //        .Include(r => r.Hotel)
            //            .ThenInclude(h => h.HotelLabelTypes)
            //        .Include(r => r.Hotel)
            //            .ThenInclude(h => h.NearObjects)
            //            .ThenInclude(no => no.NearObjectName)
            //        .Where(r =>
            //             (dto.Place == null || dto.Place == "" || r.Hotel.City.CityName == dto.Place || r.Hotel.City.Country.CountryName == dto.Place))
            //        .Where(r => ((r.BedTypes.Sum(bd => bd.Adult) * dto.Rooms) >= dto.Adults && (r.BedTypes.Sum(bd => bd.Children) * dto.Rooms) >= dto.Children) ||
            //             (r.BedTypes.Sum(bd => bd.Adult) * dto.Rooms) >= (dto.Adults + dto.Children))
            //        .Where(r => r.RoomQuantity - r.Books.Count(b => b.RoomId == r.Id && dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn) >= dto.Rooms)
            //        //.Where(r => (r.RoomQuantity - r.Books.Count(b => b.RoomId == r.Id && dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn)) >= dto.Rooms)
            //        .Where(r => (dto.Stars == null || dto.Stars.Count == 0 || dto.Stars[0] == 0 || dto.Stars.Contains(r.Hotel.Stars)))
            //        .Where(r => dto.Facilities == null || dto.Facilities.Count == 0 || dto.Facilities[0] == 0 || r.Hotel.Facilities.Any(f => dto.Facilities.Contains(f.Id)))
            //        .Where(r => dto.Rating == null || dto.Rating.Count == 0 || dto.Rating[0] == 0 || (r.Hotel.HotelData != null && dto.Rating.Contains(Math.Floor(r.Hotel.HotelData.Rating))))
            //        .Where(r => dto.NearPlaces == null || dto.NearPlaces.Count == 0 || dto.NearPlaces[0] == 0 || r.Hotel.NearObjects.Any(s => dto.NearPlaces.Contains(s.NearObjectNameId)))
            //        .Where(r => dto.HotelTypes == null || dto.HotelTypes.Count == 0 || dto.HotelTypes[0] == 0 || dto.HotelTypes.Any(t => t == r.Hotel.HotelTypeId))
            //        .Where(r => dto.HotelChains == null || dto.HotelChains.Count == 0 || dto.HotelChains[0] == 0 || dto.HotelChains.Any(c => c == r.Hotel.HotelChainId))
            //        .Where(r => dto.MinPrice == 0 || dto.MaxPrice == 0 || (r.Hotel.HotelData.HotelMinRoomPrice >= dto.MinPrice && r.Hotel.HotelData.HotelMinRoomPrice <= dto.MaxPrice))
            //        .Where(r => dto.HotelLabels == null || dto.HotelLabels.Count == 0 || dto.HotelLabels[0] == 0 || r.Hotel.HotelLabelTypes.Any(f => dto.HotelLabels.Contains(f.Id)))
            //.Select(r => new HotelDto
            //{
            //    Id = r.HotelId,
            //    HotelName = r.Hotel.HotelName,
            //    HotelAddress = r.Hotel.HotelAddress,
            //    HotelPhone = r.Hotel.HotelPhone,
            //    HotelImage = _imageToLinkConverter.ConvertImageToLink(r.Hotel.HotelImage, S3Folders.HotelsImg),
            //    Description = r.Hotel.Description,
            //    Star = r.Hotel.Stars,
            //    Rating = r.Hotel.HotelData.Rating,
            //    ReviewQty = r.Hotel.HotelData.ReviewCount,
            //    RoomPrice = r.RoomPrice,
            //    RoomQty = r.RoomQuantity,
            //    FreeRoomQty = r.RoomQuantity - r.Books.Count(b => b.RoomId == r.Id && dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn),
            //    HotelLabels = r.Hotel.HotelLabelTypes.Select(hct => new HotelInfoLabelDto
            //    {
            //        LabelName = hct.LabelName,
            //        LabelIcon = _imageToLinkConverter.ConvertImageToLink(hct.LabelIcon, S3Folders.LabelImg)
            //    }).ToList(),
            //    NearObjects = r.Hotel.NearObjects.Select(s => new NearObjectDto
            //    {
            //        Id = s.Id,
            //        StationName = s.NearObjectName.Name,
            //        Distance = s.Distance,
            //        DistanceMetric = s.DistanceMetric,
            //        StationIcon = _imageToLinkConverter.ConvertImageToLink(s.NearObjectName.Icon!, S3Folders.LabelImg)
            //    }).ToList(),
            //}).OrderByDescending(h => h.Rating)
            //         .ThenBy(h => h.RoomPrice);

            //    var hotels = await queryHotels.Skip((dto.Page - 1) * dto.HotelQty).Take(dto.HotelQty).ToListAsync();


            var queryHotels = _hotelRepository.GetAllAsSplitQuery().AsNoTracking()
                .Include(h => h.Rooms)
                    .ThenInclude(r => r.BedTypes)
                .Include(h => h.Rooms)
                    .ThenInclude(r => r.Books)
                .Include(h => h.City)
                    .ThenInclude(c => c.Country)
                .Include(h => h.HotelData)
                .Include(h => h.HotelLabelTypes)
                .Include(h => h.NearObjects)
                    .ThenInclude(ns => ns.NearObjectName)
                 .Where(h =>
                    (dto.Place == null || dto.Place == "" || h.City.CityName == dto.Place || h.City.Country.CountryName == dto.Place))
                  .Where(h => h.Rooms.Any(r => (r.BedTypes.Sum(bd => bd.Adult) * dto.Rooms) >= dto.Adults && (r.BedTypes.Sum(bd => bd.Children) * dto.Rooms) >= dto.Children ||
                                (r.BedTypes.Sum(bd => bd.Adult) * dto.Rooms) >= (dto.Adults + dto.Children))
                        //(r.BedTypes.Sum(bd => bd.Adult) >= dto.Adults && r.BedTypes.Sum(bd => bd.Children) >= dto.Children) ||
                        //r.BedTypes.Sum(bd => bd.Adult) >= (dto.Adults + dto.Children))
                 )
                // .Where(r => r.RoomQuantity - r.Books.Count(b => b.RoomId == r.Id && dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn) >= dto.Rooms)
                 .Where(h => h.Rooms.Any(r => r.RoomQuantity - r.Books.Count(b => b.RoomId == r.Id && dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn) >= dto.Rooms))
                 .Where(h => h.Rooms.Count(r => !r.Books.Any(b => dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn)) >= dto.Rooms)
                // .Where(r => r.RoomQuantity - r.Books.Count(b => b.RoomId == r.Id && dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn) >= dto.Rooms)
                 .Where(h => (dto.Stars == null || dto.Stars.Count == 0 || dto.Stars[0] == 0 || dto.Stars.Contains(h.Stars)))
                 .Where(h => dto.Facilities == null || dto.Facilities.Count == 0 || dto.Facilities[0] == 0 || h.Facilities.Any(f => dto.Facilities.Contains(f.Id)))
                 .Where(h => dto.Rating == null || dto.Rating.Count == 0 || dto.Rating[0] == 0 || (h.HotelData != null && dto.Rating.Contains(Math.Floor(h.HotelData.Rating))))
                 .Where(h => dto.NearPlaces == null || dto.NearPlaces.Count == 0 || dto.NearPlaces[0] == 0 || h.NearObjects.Any(s => dto.NearPlaces.Contains(s.NearObjectNameId)))
                 .Where(h => dto.HotelTypes == null || dto.HotelTypes.Count == 0 || dto.HotelTypes[0] == 0 || dto.HotelTypes.Any(t => t == h.HotelTypeId))
                 .Where(h => dto.HotelChains == null || dto.HotelChains.Count == 0 || dto.HotelChains[0] == 0 || dto.HotelChains.Any(c => c == h.HotelChainId))
                 .Where(h => dto.HotelLabels == null || dto.HotelLabels.Count == 0 || dto.HotelLabels[0] == 0 || h.HotelLabelTypes.Any(l => dto.HotelLabels.Contains(l.Id)))
                 .Where(h => dto.MinPrice == 0 || dto.MaxPrice == 0 || (h.HotelData.HotelMinRoomPrice >= dto.MinPrice && h.HotelData.HotelMinRoomPrice <= dto.MaxPrice))
                 .Select(x => new HotelDto
                 {
                     Id = x.Id,
                     HotelName = x.HotelName,
                     HotelAddress = x.HotelAddress,
                     HotelPhone = x.HotelPhone,
                     HotelImage = x.HotelImage,
                     Description = x.Description,
                     Star = x.Stars,
                     Rating = x.HotelData.Rating,
                     ReviewQty = x.HotelData.ReviewCount,
                     RoomPrice = x.HotelData.HotelMinRoomPrice,
                     HotelLabels = x.HotelLabelTypes.Select(hct => new HotelInfoLabelDto
                     {
                         LabelName = hct.LabelName,
                         LabelIcon = _imageToLinkConverter.ConvertImageToLink(hct.LabelIcon, S3Folders.LabelImg)
                     }).ToList(),
                     NearObjects = x.NearObjects.Select(s => new NearObjectDto
                     {
                         Id = s.Id,
                         StationName = s.NearObjectName.Name,
                         Distance = s.Distance,
                         DistanceMetric = s.DistanceMetric,
                         StationIcon = _imageToLinkConverter.ConvertImageToLink(s.NearObjectName.Icon!, S3Folders.LabelImg)
                     }).ToList(),
                 }).OrderBy(h => h.Rating).ThenBy(h => h.RoomPrice);

            var hotels = await queryHotels.Skip((dto.Page - 1) * dto.HotelQty).Take(dto.HotelQty).ToListAsync();

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


            if (email != null)
            {
                var currancy = await GetUserCurrencyAsync(email);

                if (currancy != null)
                {
                    hotels.ForEach(x =>
                    {
                        x.RoomPrice = Math.Round(x.RoomPrice / (decimal)currancy.ExchangeRate);
                        x.CurrencyChar = currancy.CurrencyChar;
                    });
                }
            }

            SearchHotelResponseDto hotelResponse = new SearchHotelResponseDto
            {
                Matches = queryHotels.Count(),
                Count = hotels.Count(),
                Hotels = hotels
            };


            return new BaseResult<SearchHotelResponseDto>()
            {
                Data = hotelResponse
            };
        }
        public async Task<BaseResult<CreateUpdateHotelDto>> CreateHotelAsync(CreateHotelDto dto)
        {
            if (dto.Stars < 0 || dto.CityId < 0 || dto == null)
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

        private async Task<UserCurrency?> GetUserCurrencyAsync(string? email)
        {
            if (email != null)
            {
                var currancy = await _userRepository.GetAll()
                   .Where(u => u.UserEmail == email)
                   .Include(x => x.UserProfile).ThenInclude(x => x.Currency)
                   .Select(x => new UserCurrency{
                        CurrencyChar = x.UserProfile.Currency!.CurrencyChar, 
                        ExchangeRate = x.UserProfile.Currency.ExchangeRate 
                   })
                   .FirstOrDefaultAsync();

                return currancy;
            }

            return null;
         }

    }
}
