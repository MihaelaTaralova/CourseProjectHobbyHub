using HobbyBubSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;

namespace HobbyHubSystem.Data.EntityConfiguration
{
    internal class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder
               .Property(a => a.RepliedOn)
               .HasDefaultValueSql("GETDATE()");

            builder
                .HasOne(a => a.Question)
                .WithMany(c => c.Answers)
                .HasForeignKey(q => q.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(a => a.DiscussionTopic)
                .WithMany(a => a.Answers)
                .HasForeignKey(a => a.DiscussionTopicId)
                .OnDelete(DeleteBehavior.NoAction);
               
                        
        }

       
    }
}
