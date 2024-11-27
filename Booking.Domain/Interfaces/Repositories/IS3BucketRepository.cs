using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Repositories
{
    public interface IS3BucketRepository
    {
        Task<OperationResult<System.Net.HttpStatusCode>> UploadFileAsync(string bucketName, string key, Stream fileStream, string contentType);
        Task<OperationResult<Stream>> GetFileAsync(string bucketName, string key);
        Task<OperationResult<bool>> DeleteFileAsync(string bucketName, string key);
        Task<bool> DoesBucketExistAsync(string bucketName);
    }
}
