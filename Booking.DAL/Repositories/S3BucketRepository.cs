using Amazon.S3;
using Amazon.S3.Model;
using Azure.Core;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Booking.DAL.Repositories
{
    internal class S3BucketRepository : IS3BucketRepository
    {
        private readonly IAmazonS3 _s3Client;

        public S3BucketRepository(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }
        public async Task<S3OperationResult<System.Net.HttpStatusCode>> UploadFileAsync(string bucketName, string key, Stream fileStream, string contentType)
        {
            try
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    InputStream = fileStream,
                    ContentType = contentType
                };

                var response = await _s3Client.PutObjectAsync(putRequest);

                return new S3OperationResult<System.Net.HttpStatusCode>
                {
                    Data = response.HttpStatusCode
                };
            }
            catch (AmazonS3Exception ex)
            {
                return new S3OperationResult<System.Net.HttpStatusCode>
                {
                    ErrorMessage = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new S3OperationResult<System.Net.HttpStatusCode>
                {
                    ErrorMessage = $"An error occurred in AWS S3 operation: {ex.Message}"
                };
            }
        }


        public async Task<bool> DoesBucketExistAsync(string bucketName)
        {
            return await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
        }

        public async Task<S3OperationResult<Stream>> GetFileAsync(string bucketName, string key)
        {
            try
            {
                var getRequest = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = key
                };

                using (var response = await _s3Client.GetObjectAsync(getRequest))
                {
                    var memoryStream = new MemoryStream();
                    await response.ResponseStream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    if (memoryStream.Length > 0) 
                    {
                        return new S3OperationResult<Stream>
                        {
                            Data = memoryStream
                        };
                    }
                    else
                    {
                        return new S3OperationResult<Stream>
                        {
                            ErrorMessage = $"An error occurred in AWS S3 operation"
                        };
                    }
                   
                }
            }
            catch (AmazonS3Exception ex)
            {
                return new S3OperationResult<Stream>
                {
                    ErrorMessage = $"An error occurred in AWS S3 operation: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new S3OperationResult<Stream>
                {
                   ErrorMessage = $"An error occurred in AWS S3 operation: {ex.Message}"
                };
            }
        }
        //public async Task<File> GetFileAsync(string bucketName, string key)
        //{
        //    var bucketExists = await DoesBucketExistAsync(bucketName);

        //    if (!bucketExists) return NotFound($"booking-img does not exist.");
        //    var s3Object = await _s3Client.GetObjectAsync(bucketName, key);
        //    return File(s3Object.ResponseStream, s3Object.Headers.ContentType);
        //}

        public async Task<S3OperationResult<bool>> DeleteFileAsync(string bucketName, string key)
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };

            var response = await _s3Client.DeleteObjectAsync(deleteRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK ||
                response.HttpStatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return new S3OperationResult<bool>
                {
                    Data = true
                };
            }

            return new S3OperationResult<bool>
            {
                ErrorMessage = "File deletion failed with unexpected status code."
            };
 
        }
        //public async Task<bool> DoesFileExistAsync(string bucketName, string key)
        //{
        //    var metadataRequest = new GetObjectMetadataRequest
        //    {
        //        BucketName = bucketName,
        //        Key = key
        //    };

        //    var response = await _s3Client.GetObjectMetadataAsync(metadataRequest);

        //    if (response.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
        //    {
        //        return false;
        //    }

        //    return true;
        //}
    }
}
