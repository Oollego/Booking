using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.SearchFilter
{
    public class SearchFilterResponseDto
    {
        public List<RatingFilterDto> Ratings { get; set; } = null!;
        public List<LabelFilterDto> Labels { get; set; } = null!;
        public List<StarFilterDto> Stars { get; set; } = null!;
        public List<NearPlaceFilterDto> NearPlaces { get; set;} = null!;
        public List<FacilityFilterDto> Facilities { get; set;} = null!;
        public List<HotelTypeFilterDto> HotelTypes { get; set; } = null!;
        public List<HotelChainFilterDto> HotelChains { get; set; } = null!;
    }
}
