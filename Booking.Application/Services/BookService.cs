using Booking.Application.Converters;
using Booking.Application.Resources;
using Booking.Application.Services.ServiceDto;
using Booking.Domain.Dto.Book;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Converters;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Booking.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Room> _roomRepository;
        private readonly IBaseRepository<Book> _bookRepository;
        private readonly IUniqueCodeGenerator _uniqueCodeGenerator;
        private readonly IImageToLinkConverter _imageToLinkConverter;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        public BookService(IBaseRepository<User> userRepository, ILogger logger, IBaseRepository<Room> roomRepository,
            IBaseRepository<Book> bookRepository, IUniqueCodeGenerator uniqueCodeGenerator, IEmailService emailService, 
            IImageToLinkConverter imageToLinkConverter)
        {
            _userRepository = userRepository;
            _logger = logger;
            _roomRepository = roomRepository;
            _bookRepository = bookRepository;
            _uniqueCodeGenerator = uniqueCodeGenerator;
            _emailService = emailService;
            _imageToLinkConverter = imageToLinkConverter;
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
                .Include(r => r.Hotel)
                    .ThenInclude(h => h.City)
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

            string bookingCode = GetBookingCode(10);

            var newBook = new Book()
            {
                BookingEmail = dto.BookingEmail,
                BookComment = dto.BookComment,
                BookingCode = bookingCode,
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

            if (dto.IsEmail)
            {
                string confirmationEmail = dto.BookingEmail ?? user.UserEmail;
                ConfirmationEmailData data = new ConfirmationEmailData
                {
                    Address = room.Hotel.HotelAddress,
                    City = room.Hotel.City.CityName,
                    Code = newBook.BookingCode,
                    Email = newBook.BookingEmail ?? user.UserEmail,
                    CheckIn = newBook.CheckIn,
                    CheckOut = newBook.CheckOut,
                    ChangeDate = newBook.DateUntilChange ?? newBook.CheckIn,
                    RoomQuantity = newBook.RoomQuantity,
                    RoomPrice = newBook.RoomPrice,
                    Adults = newBook.Adult,
                    Children = newBook.Children ?? 0,
                    Image = _imageToLinkConverter.ConvertImageToLink(room.Hotel.HotelImage, S3Folders.HotelsImg)
                };
                await _emailService.SendConfirmationBookingEmailAsync(confirmationEmail, bookingCode, data);
            }

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
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                        .ThenInclude(h => h.City)
                .Include(b => b.User)
                .Where(b => b.User.UserEmail == email)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    BookingCode = b.BookingCode,
                    BookComment = b.BookComment,
                    CheckIn = b.CheckIn,
                    CheckOut = b.CheckOut,
                    Adult = b.Adult,
                    Children = b.Children,
                    RoomQuantity = b.RoomQuantity,
                    BookingEmail = b.BookingEmail,
                    IsPhoneCall = b.IsPhoneCall,
                    IsEmail = b.IsEmail,
                    RoomPrice = b.RoomPrice,
                    DateUntilChange = b.DateUntilChange,
                    CityId = b.Room.Hotel.CityId,
                    CityName = b.Room.Hotel.City.CityName,
                    HotelId = b.Room.Hotel.Id,
                    HotelName = b.Room.Hotel.HotelName,
                    RoomId = b.RoomId,
                    UserId = b.UserId
                }).ToListAsync();

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
              .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                        .ThenInclude(h => h.City)
              .Include(b => b.User)
              .Where(b => b.User.UserEmail == email)
              .Where(b => b.Id == id)
              .Select(b => new BookDto
              {
                  Id = b.Id,
                  BookingCode = b.BookingCode,
                  BookComment = b.BookComment,
                  CheckIn = b.CheckIn,
                  CheckOut = b.CheckOut,
                  Adult = b.Adult,
                  Children = b.Children,
                  RoomQuantity = b.RoomQuantity,
                  BookingEmail = b.BookingEmail,
                  IsPhoneCall = b.IsPhoneCall,
                  IsEmail = b.IsEmail,
                  RoomPrice = b.RoomPrice,
                  DateUntilChange = b.DateUntilChange,
                  CityId = b.Room.Hotel.CityId,
                  CityName = b.Room.Hotel.City.CityName,
                  HotelId = b.Room.Hotel.Id,
                  HotelName = b.Room.Hotel.HotelName,
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

        public string GetBookingCode(int times)
        {
            string code = _uniqueCodeGenerator.GenerateUniqueBookingCode();

            var booking = _bookRepository.GetAll().AsNoTracking()
                .Where(b => b.BookingCode == code).FirstOrDefault();

            if (booking  != null)
            {
                times++;
                if (times < 0)
                {
                    return "";
                }
                code = GetBookingCode(times);
            }

            return code;
        }
    }
}
