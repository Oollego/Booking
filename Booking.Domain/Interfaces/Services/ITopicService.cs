using Booking.Domain.Dto.Topic;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface ITopicService
    {
        Task<BaseResult<TopicDto>> GetTopicByIdAsync(long id);
        Task<CollectionResult<TopicDto>> GetAllTopicsAsync();
        Task<BaseResult<TopicDto>> CreatTopicAsync(CreateTopicDto dto);
        Task<BaseResult<TopicDto>> DeleteTopicAsync(long id);
        Task<BaseResult<TopicDto>> UpdateTopicAsync(UpdateTopicDto dto);
    }
}
