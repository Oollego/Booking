using Booking.Domain.Dto.Book;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface IBookService
    {
        Task<BaseResult<long>> AddUserBooking(CreateBookDto dto, string? email);
        Task<CollectionResult<BookDto>> GetAllUserBooks(string? email);
        Task<BaseResult<BookDto>> GetBookById(long id, string? email);
    }
}
