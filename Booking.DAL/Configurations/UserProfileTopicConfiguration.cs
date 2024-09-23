using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Configurations
{
    internal class UserProfileTopicConfiguration : IEntityTypeConfiguration<UserProfileTopic>
    {
        public void Configure(EntityTypeBuilder<UserProfileTopic> builder)
        {
            builder.ToTable("user_topics");
            builder.Property(x => x.UserProfileId).IsRequired();
            builder.Property(x => x.TopicId).IsRequired();
        }
    }
}
