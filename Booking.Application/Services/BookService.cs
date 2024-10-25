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

        public BookService(IBaseRepository<User> userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<BaseResult<long>> AddUserBooking(CreateBookDto dto, string? email)
        {
            var user = await _userRepository.GetAll().AsNoTracking()
                .Where(u => u.UserEmail == email).FirstOrDefaultAsync();

            if (user == null)
            {
                return new BaseResult<long>
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }

           // var book = await _bookRepository.GetAll().AsNoTracking()


            var room = await _roomRepository.GetAll().AsNoTracking()
                .Where(r => r.Id == dto.RoomId).FirstOrDefaultAsync();

            if (room == null)
            {
                return new BaseResult<long>
                {
                    ErrorMessage = ErrorMessage.RoomNotFound,
                    ErrorCode = (int)ErrorCodes.RoomNotFound
                };
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
                RoomPrice = dto.RoomPrice,
                RoomId = dto.RoomId,
                UserId = user.Id
            };

            //public long Id { get; set; }
            //public string? BookComment { get; set; } = null!;
            //public DateTime CheckIn { get; set; }
            //public DateTime CheckOut { get; set; }
            //public int Adult { get; set; }
            //public int? Children { get; set; }
            //public int RoomQuantity { get; set; }
            //public bool IsPhoneCall { get; set; } = false;
            //public bool IsEmail { get; set; } = false;
            //public decimal RoomPrice { get; set; }
            //public DateTime? DateUntilChange { get; set; }
            //public DateTime BookDate { get; set; } = DateTime.Now;
            //public long RoomId { get; set; }
            //public Room Room { get; set; } = null!;
            //public long UserId { get; set; }
            //public User User { get; set; } = null!;

            return new BaseResult<long>
            {
                Data = 0
            };
        }

        public Task<CollectionResult<BookDto>> GetAllUserBooks(string? email)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<BookDto>> GetBookById(long id, string? email)
        {
            throw new NotImplementedException();
        }
    }
}
