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
        Task<S3OperationResult<System.Net.HttpStatusCode>> UploadFileAsync(string bucketName, string key, Stream fileStream, string contentType);
        Task<S3OperationResult<Stream>> GetFileAsync(string bucketName, string key);
        Task<S3OperationResult<bool>> DeleteFileAsync(string bucketName, string key);
        Task<bool> DoesBucketExistAsync(string bucketName);
    }
}
