using AutoMapper;
using Booking.Domain.Dto.Topic;
using Booking.Domain.Entity;

namespace Booking.Application.Mapping
{
    internal class TopicMapping: Profile
    {
        public TopicMapping()
        {
            CreateMap<Topic, TopicDto>().ReverseMap();
        }
    }
}
