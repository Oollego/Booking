using Booking.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class FileService : IFileService
    {
        public string GetRandomFileName(string fileName)
        {
            var indexOfDot = fileName.LastIndexOf('.');
            if (indexOfDot == -1) return "";
            string ext = fileName.Substring(indexOfDot, fileName.Length - indexOfDot);

            var guidName = Guid.NewGuid().ToString();
            return guidName + ext;
        }
    }
}
