using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Entity;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Booking.Application.Resources;
using Booking.Domain.Enum;
using Booking.Domain.Dto.Room;
using Booking.Domain.Interfaces.Validations;
using System.Runtime.CompilerServices;
using AutoMapper;
using Booking.Domain.Dto.User;
using Booking.Domain.Dto.RoomImage;
using Booking.Domain.Dto.Bed;
using Booking.Domain.Dto.RoomComfort;
using Booking.Domain.Dto.Hotel;
using Booking.Domain.Interfaces.Converters;
using Booking.Application.Services.ServiceEntity;

namespace Booking.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IBaseRepository<Room> _roomRepository = null!;
        private readonly IBaseRepository<Hotel> _hotelRepository = null!;
        private readonly IBaseRepository<User> _userRepository = null!;
        private readonly IRoomValidator _roomValidator;
        private readonly IMapper _mapper;
        private readonly ILogger _logger = null!;
        private readonly IImageToLinkConverter _imageToLinkConverter;


        public RoomService(IBaseRepository<Room> roomRepository, ILogger logger,
            IBaseRepository<Hotel> hotelRepository, IRoomValidator roomValidator, IMapper mapper, 
            IImageToLinkConverter imageToLinkConverter, IBaseRepository<User> userRepository)
        {
            _roomRepository = roomRepository;
            _logger = logger;
            _hotelRepository = hotelRepository;
            _roomValidator = roomValidator;
            _mapper = mapper;
            _imageToLinkConverter = imageToLinkConverter;
            _userRepository = userRepository;
        }

        /// < inheritdoc />
        public async Task<BaseResult<RoomResponseDto>> CreatRoomAsync(CreateRoomDto dto)
        {
  
            var hotel = await _hotelRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.HotelId);
            var room = await _roomRepository.GetAll().FirstOrDefaultAsync(x => x.RoomName == dto.RoomName);


            if ( room != null )
            {
                return new BaseResult<RoomResponseDto>()
                {
                    ErrorMessage = ErrorMessage.RoomAlreadyExists,
                    ErrorCode = (int)ErrorCodes.RoomAlreadyExists
                };
            }

            if (hotel == null)
            {
                return new BaseResult<RoomResponseDto>()
                {
                    ErrorMessage = ErrorMessage.HotelNotFound,
                    ErrorCode = (int)ErrorCodes.HotelNotFound
                };
            }

            room = new Room()
            {
                RoomName = dto.RoomName,
                RoomPrice = dto.RoomPrice,
                Cancellation = dto.CancellationPrice,
                HotelId = dto.HotelId,
            };

            room = await _roomRepository.CreateAsync(room);
            await _roomRepository.SaveChangesAsync();


            return new BaseResult<RoomResponseDto>()
            {
                Data = _mapper.Map<RoomResponseDto>(room)
            };

        }

        /// < inheritdoc />
        public async Task<BaseResult<RoomResponseDto>> DeleteRoomAsync(long roomId)
        {
            var room = await _roomRepository.GetAll().FirstOrDefaultAsync(x => x.Id == roomId);
            //var result = _roomValidator.ValidateOnNull(room);
            //if (!result.IsSuccess)
            //{
            //    return new BaseResult<RoomDto>()
            //    {
            //        ErrorMessage = result.ErrorMessage,
            //        ErrorCode = result.ErrorCode,
            //    };
            //}
            if (room == null)
            {
                return new BaseResult<RoomResponseDto>()
                {
                    ErrorMessage = ErrorMessage.RoomNotFound,
                    ErrorCode = (int)ErrorCodes.RoomNotFound,
                };
            }

            _roomRepository.Remove(room);
            await _roomRepository.SaveChangesAsync();

            return new BaseResult<RoomResponseDto>()
            {
                Data = _mapper.Map<RoomResponseDto>(room)
            };
        }

        public async Task<BaseResult<RoomDto>> GetRoomByIdAsync(long roomId, string? email)
        {
            if(roomId < 0)
            {
                return new BaseResult<RoomDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var room = await _roomRepository.GetAll()
               .Where(x => x.Id == roomId)
               .Include(x => x.RoomComfortIconTypes)
               .Include(x => x.RoomImages)
               .Include(x => x.BedTypes)
               .Select(x => new RoomDto
               {
                   Id = x.Id,
                   RoomName = x.RoomName,
                   Price = x.RoomPrice,
                   CancelationPrice = x.Cancellation,
                   Adults = x.BedTypes.Sum(x => x.Adult),
                   Children = x.BedTypes.Sum(x => x.Children),
                   Images = x.RoomImages.Select(x => new RoomImageDto
                   {
                       Id = x.Id,
                       ImageName = _imageToLinkConverter.ConvertImageToLink(x.ImageName, S3Folders.HotelsImg)
                   }).ToList(),
                   Beds = x.BedTypes.Select(x => new BedDto
                   {
                       Id = x.Id,
                       BedName = x.BedTypeName,
                       Adult = x.Adult,
                       Children = x.Children
                   }).ToList(),
                   RoomComforts = x.RoomComfortIconTypes.Select(x => new RoomComfortDto
                   {
                       Id = x.Id,
                       ComfortIcon = _imageToLinkConverter.ConvertImageToLink(x.ComfortIcon, S3Folders.RoomComfortImg),
                       ComfortName = x.ComfortName
                   }).ToList()
               })
               .FirstOrDefaultAsync();
               

            //var room = _roomRepository.GetAll()
            //    .Where(x => x.Id == roomId)
            //    .Select(x => new RoomDto(x.Id, x.RoomName, x.RoomPrice, x.CancellationPrice))
            //    .AsEnumerable()
            //    .FirstOrDefault(x => x.Id == roomId);


            if (room == null) 
            {
                _logger.Warning( $"Room id:{roomId} not found");
                return new BaseResult<RoomDto>()
                {
                    ErrorMessage = ErrorMessage.RoomNotFound,
                    ErrorCode = (int)ErrorCodes.RoomNotFound
                };
            }

            if (email != null)
            {
                var currancy = await GetUserCurrencyAsync(email);

                if (currancy != null)
                {
                    room.CancelationPrice = Math.Round(room.CancelationPrice / (decimal)currancy.ExchangeRate);
                    room.Price = Math.Round(room.Price / (decimal)currancy.ExchangeRate);
                    room.CurrencyChar = currancy.CurrencyChar;
                }
            }

            return new BaseResult<RoomDto>()
            {
                Data = room
            };

            //return Task.FromResult(new BaseResult<RoomDto>()
            //{
            //    Data = room
            //});
        }

        /// < inheritdoc />
        public async Task<CollectionResult<RoomDto>> GetRoomsAsync(long hotelId, string? email)
        {
            if (hotelId < 1)
            {
                return new CollectionResult<RoomDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }
            //var rooms = await _roomRepository.GetAll().Where(x => x.Hotel.Id == hotelId)
            //    .Include(x => x.)

            var rooms = await _roomRepository.GetAll()
                .Where(x => x.HotelId == hotelId)
                .Include(x => x.RoomComfortIconTypes)
                .Include(x => x.RoomImages)
                .Include(x => x.BedTypes)
                .Select(x => new RoomDto
                {
                    Id = x.Id,
                    RoomName = x.RoomName,
                    Price = x.RoomPrice,
                    CancelationPrice = x.Cancellation,
                    Adults = x.BedTypes.Sum(x => x.Adult),
                    Children = x.BedTypes.Sum( x => x.Children ),
                    Images = x.RoomImages.Select(x => new RoomImageDto
                    { 
                        Id = x.Id, 
                        ImageName = _imageToLinkConverter.ConvertImageToLink(x.ImageName, S3Folders.HotelsImg) 
                    }).ToList(),
                    Beds = x.BedTypes.Select(x => new BedDto 
                    { 
                        Id = x.Id, 
                        BedName = x.BedTypeName, 
                        Adult = x.Adult, 
                        Children = x.Children 
                    }).ToList(),
                    RoomComforts = x.RoomComfortIconTypes.Select(x => new RoomComfortDto 
                    { 
                        Id = x.Id, 
                        ComfortIcon = _imageToLinkConverter.ConvertImageToLink(x.ComfortIcon, S3Folders.RoomComfortImg),
                        ComfortName = x.ComfortName 
                    }).ToList()
                })
                .ToListAsync();

            //var rooms = await _roomRepository.GetAll()
            //         .Where(x => x.Hotel.Id == hotelId)
            //         .Select(x => new RoomDto(x.Id, x.RoomName, x.RoomPrice, x.Cancellation, x.Guests))
            //         .ToArrayAsync();

            if (rooms.Count == 0)
            {
                _logger.Warning(ErrorMessage.RoomsNotFound, rooms!.Count);
                return new CollectionResult<RoomDto>()
                {
                    ErrorMessage = ErrorMessage.RoomsNotFound,
                    ErrorCode = (int)ErrorCodes.RoomsNotFound
                };
            }
            if (rooms == null)
            {
                _logger.Warning(ErrorMessage.RoomsNotFound);
                return new CollectionResult<RoomDto>()
                {
                    ErrorMessage = ErrorMessage.RoomsNotFound,
                    ErrorCode = (int)ErrorCodes.RoomsNotFound
                };
            }

            if (email != null)
            {
                var currancy = await GetUserCurrencyAsync(email);

                if (currancy != null)
                {
                    rooms.ForEach(x =>
                    {
                        x.CancelationPrice = Math.Round(x.CancelationPrice / (decimal)currancy.ExchangeRate);
                        x.Price = Math.Round(x.Price / (decimal)currancy.ExchangeRate);
                        x.CurrencyChar = currancy.CurrencyChar;
                    });
                }
            }

            return new CollectionResult<RoomDto>()
            {
                Data = rooms,
                Count = rooms.Count
            };
        }

        public async Task<BaseResult<RoomResponseDto>> UpdateRoomAsync(UpdateRoomDto dto)
        {

            var room = await _roomRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (room == null)
            {
                return new BaseResult<RoomResponseDto>()
                {
                    ErrorMessage = ErrorMessage.RoomNotFound,
                    ErrorCode = (int)ErrorCodes.RoomNotFound,
                };
            }
             
            room.RoomName = dto.RoomName;
            room.RoomPrice = dto.RoomPrice;
            room.Cancellation = dto.Cancellation;


            var updatedRoom = _roomRepository.Update(room);
            await _roomRepository.SaveChangesAsync();

            return new BaseResult<RoomResponseDto>()
            {
                Data = _mapper.Map<RoomResponseDto>(updatedRoom)
            };
        }

        private async Task<UserCurrency?> GetUserCurrencyAsync(string? email)
        {
            if (email != null)
            {
                var currancy = await _userRepository.GetAll()
                   .Where(u => u.UserEmail == email)
                   .Include(x => x.UserProfile).ThenInclude(x => x.Currency)
                   .Select(x => new UserCurrency
                   {
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
