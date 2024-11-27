using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Repositories
{
    public class HttpClientRepository : IHttpClientRepository
    {
        public async Task<OperationResult<HttpMessage>> GetStreamFromUrlAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var urlResponce = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                    urlResponce.EnsureSuccessStatusCode();

                    var contentType = urlResponce.Content.Headers.ContentType?.MediaType;
                    var fileName = urlResponce.Content.Headers.ContentDisposition?.FileName?.Trim('"') ??
                        Path.GetFileName(new Uri(url).LocalPath) ?? "default.jpg";

                    byte[] file = await urlResponce.Content.ReadAsByteArrayAsync();
                    MemoryStream memoryStream = new MemoryStream(file);

                    var httpMessage = new HttpMessage
                    {
                        ContentType = contentType,
                        FileName = fileName,
                        StreamData = memoryStream
                    };


                    return new OperationResult<HttpMessage>
                    {
                        Data = httpMessage,
                    };
                }
                catch (HttpRequestException)
                {
                    return new OperationResult<HttpMessage>
                    {
                        ErrorMessage = "Can not get data from url"
                    };
                }
            }
        }
    }
}
