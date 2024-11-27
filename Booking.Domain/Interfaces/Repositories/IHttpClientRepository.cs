using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Repositories
{
    public interface IHttpClientRepository
    {
        Task<OperationResult<HttpMessage>> GetStreamFromUrlAsync(string url);
    }
}
