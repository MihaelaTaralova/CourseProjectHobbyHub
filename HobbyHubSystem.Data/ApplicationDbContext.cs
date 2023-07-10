using HobbyBubSystem.Data.Models;
using HobbyBubSystem.Data.Models.Account;
using HobbyBubSystem.Data.Models.Discussion;
using HobbyHubSystem.Data.EntityConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace HobbyHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<HobbyUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        DbSet<Article> Articles { get; set; } = null!;

        DbSet<Category> Categories { get; set; } = null!;

        DbSet<Event> Events { get; set; } = null!;

        DbSet<Hobby> Hobbies { get; set; } = null!;

        DbSet<Hub> Hubs { get; set; } = null!;

        DbSet<Answer> Answers { get; set; } = null!;

        DbSet<Question> Questions { get; set; } = null!;

        DbSet<DiscussionTopic> DiscussionTopics { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration<Answer>(new AnswerConfiguration());
            builder.ApplyConfiguration<Article>(new ArticleConfiguration());
            builder.ApplyConfiguration<Event>(new EventConfiguration());
            builder.ApplyConfiguration<Hobby>(new HobbyConfiguration());
            builder.ApplyConfiguration<Question>(new QuestionConfiguration());
        }

    }
}