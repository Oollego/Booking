using Booking.Application.Resources;
using Booking.Domain.Interfaces.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Converters
{
    public class ImageToLinkConverter: IImageToLinkConverter
    {
        public string _serverDomain = null!;

        public ImageToLinkConverter(string serverDomain)
        {
            _serverDomain = serverDomain;
        }

        public string ConvertImageToLink(string ImageName, string folder)
        {
            if (ImageName == null || ImageName == "")
            {
                return "";
            }
            return _serverDomain + "/api/Images?key=" + folder + "/" + ImageName;
        }

        public List<string> ConvertImagesToLink(IEnumerable<string> Images, string folder)
        {
             return Images.Select(img => _serverDomain + "/api/Images?key=" + folder + "/" + img ).ToList();
        }
    }
}
