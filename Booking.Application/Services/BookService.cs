using Booking.Application.Resources;
using Booking.Domain.Dto.Book;
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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Room> _roomRepository;
        private readonly IBaseRepository<Book> _bookRepository;
        private readonly ILogger _logger;

        public BookService(IBaseRepository<User> userRepository, ILogger logger, IBaseRepository<Room> roomRepository, IBaseRepository<Book> bookRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _roomRepository = roomRepository;
            _bookRepository = bookRepository;
        }

        public async Task<BaseResult<long>> AddUserBooking(CreateBookDto dto, string? email)
        {
            var user = await _userRepository.GetAll().AsNoTracking()
                .Where(u => u.UserEmail == email).FirstOrDefaultAsync();

            if (user == null)
            {
                _logger.Warning(ErrorMessage.UserNotFound);
                return new BaseResult<long>
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }

            var room = await _roomRepository.GetAll().AsNoTracking()
                .Where(r => r.Id == dto.RoomId).FirstOrDefaultAsync();

            if (room == null)
            {
                _logger.Warning(ErrorMessage.RoomNotFound);
                return new BaseResult<long>
                {
                    ErrorMessage = ErrorMessage.RoomNotFound,
                    ErrorCode = (int)ErrorCodes.RoomNotFound
                };
            }

            var boockedRooms = await _bookRepository.GetAll().AsNoTracking()
               .Where(b => b.RoomId == dto.RoomId)
               .Where(b => dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn)
               .GroupBy(b => b.RoomQuantity)
               .CountAsync();

            if((room.RoomQuantity - boockedRooms) < 0)
            {
                if (room == null)
                {
                    _logger.Warning(ErrorMessage.NoAvailableRooms);
                    return new BaseResult<long>
                    {
                        ErrorMessage = ErrorMessage.NoAvailableRooms,
                        ErrorCode = (int)ErrorCodes.NoAvailableRooms
                    };
                }
            }

            var newBook = new Book()
            {
                BookComment = dto.BookComment,
                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut,
                Adult = dto.Adult,
                Children = dto.Children,
                RoomQuantity = dto.RoomQuantity,
                IsPhoneCall = dto.IsPhoneCall,
                IsEmail = dto.IsEmail,
                DateUntilChange = dto.CheckIn.AddDays( -room.FixedDays ),
                RoomPrice = room.RoomPrice,
                RoomId = dto.RoomId,
                UserId = user.Id,
                BookDate = DateTime.Now,
            };

            newBook = await _bookRepository.CreateAsync( newBook );
            await _bookRepository.SaveChangesAsync();

            return new BaseResult<long>
            {
                Data = newBook.Id
            };
        }

        public async Task<CollectionResult<BookDto>> GetAllUserBooks(string? email)
        {
          
            var books = await _bookRepository.GetAll()
                .AsNoTracking()
                .Include(b => b.User)
                .Where(b => b.User.UserEmail == email)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    BookComment = b.BookComment,
                    CheckIn = b.CheckIn,
                    CheckOut = b.CheckOut,
                    Adult = b.Adult,
                    Children = b.Children,
                    RoomQuantity = b.RoomQuantity,
                    IsPhoneCall = b.IsPhoneCall,
                    IsEmail = b.IsEmail,
                    RoomPrice = b.RoomPrice,
                    DateUntilChange = b.DateUntilChange,
                    RoomId = b.RoomId,
                    UserId = b.UserId

                }).ToListAsync();

            if(books == null || books.Count == 0)
            {
                _logger.Warning(ErrorMessage.BookingNotFound);
                return new CollectionResult<BookDto>
                {
                    ErrorMessage = ErrorMessage.BookingNotFound,
                    ErrorCode = (int)ErrorCodes.BookingNotFound
                };
            }

            return new CollectionResult<BookDto>
            {
                Data = books,
                Count = books.Count
            };
 
        }

        public async Task<BaseResult<BookDto>> GetBookById(long id, string? email)
        {
            var booking = await _bookRepository.GetAll()
              .AsNoTracking()
              .Include(b => b.User)
              .Where(b => b.User.UserEmail == email)
              .Where(b => b.Id == id)
              .Select(b => new BookDto
              {
                  Id = b.Id,
                  BookComment = b.BookComment,
                  CheckIn = b.CheckIn,
                  CheckOut = b.CheckOut,
                  Adult = b.Adult,
                  Children = b.Children,
                  RoomQuantity = b.RoomQuantity,
                  IsPhoneCall = b.IsPhoneCall,
                  IsEmail = b.IsEmail,
                  RoomPrice = b.RoomPrice,
                  DateUntilChange = b.DateUntilChange,
                  RoomId = b.RoomId,
                  UserId = b.UserId

              }).FirstOrDefaultAsync();

            if (booking == null)
            {
                _logger.Warning(ErrorMessage.BookingNotFound);
                return new BaseResult<BookDto>
                {
                    ErrorMessage = ErrorMessage.BookingNotFound,
                    ErrorCode = (int)ErrorCodes.BookingNotFound
                };
            }

            return new BaseResult<BookDto>
            {
                Data = booking,
               
            };
        }
    }
}
