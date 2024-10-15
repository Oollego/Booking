using Booking.Domain.Dto.Topic;
using Booking.Domain.Dto.UserTopicDto;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface IUserTopicService
    {
        Task<CollectionResult<TopicDto>> GetAllUserTopicsAsync(string? email);
        Task<BaseResult<TopicDto>> GetUserTopicByIdAsync(long id, string? email);
        Task<BaseResult<UserTopicDto>> CreatUserTopicAsync(long topicId, string? email);
        Task<BaseResult<UserTopicDto>> DeleteUserTopicAsync(long topicId, string? email);
    }
}
