using HobbyBubSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyHubSystem.Data.EntityConfiguration
{
    internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder
                   .Property(a => a.CreatedOn)
                   .HasDefaultValueSql("GETDATE()");

            builder
               .HasOne(e => e.DiscussionTopic)
               .WithMany(c => c.Questions)
               .HasForeignKey(d => d.DiscussionTopicId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
