using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Converters
{
    public interface IImageToLinkConverter
    {
        string ConvertImageToLink(string ImageName, string folder);
        List<string> ConvertImagesToLink(IEnumerable<string> Images, string folder);
    }
}
