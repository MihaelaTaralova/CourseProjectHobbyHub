﻿using HobbyBubSystem.Data.Models;
using HobbyBubSystem.Data.Models.Account;
using HobbyBubSystem.Data.Models.Discussion;
using HobbyHubSystem.Data.EntityConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HobbyHub.Data
{
    public class HobbyHubDbContext : IdentityDbContext<HobbyUser, IdentityRole<Guid>, Guid>
    {
       
        public HobbyHubDbContext(DbContextOptions<HobbyHubDbContext> options)
            : base(options)
        {
           
        }

        public DbSet<Article> Articles { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Event> Events { get; set; } = null!;

        public DbSet<Hobby> Hobbies { get; set; } = null!;

        public DbSet<Hub> Hubs { get; set; } = null!;

        public DbSet<Answer> Answers { get; set; } = null!;

        public DbSet<Question> Questions { get; set; } = null!;

        public DbSet<DiscussionTopic> DiscussionTopics { get; set; } = null!;

        public DbSet<HobbyUserHub> HobbyUserHubs { get; set; } = null!; 

        public DbSet<HobbyUserEvent> HobbyUserEvents { get; set; } = null!; 

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration<HobbyUser>(new HobbyUserConfiguration());
            builder.ApplyConfiguration<Category>(new CategoryConfiguration());
            builder.ApplyConfiguration<Hobby>(new HobbyConfiguration());
            builder.ApplyConfiguration<Hobby>(new SeedHobbyConfiguration());
            builder.ApplyConfiguration<Hub>(new HubConfiguration());
            builder.ApplyConfiguration<HobbyUserHub>(new HobbyUserHubConfiguration());
            builder.ApplyConfiguration<HobbyUserEvent>(new HobbyUserEventConfiguration());
            builder.ApplyConfiguration<Article>(new ArticleConfiguration());
            builder.ApplyConfiguration<Event>(new EventConfiguration());
            builder.ApplyConfiguration<Question>(new QuestionConfiguration());
            builder.ApplyConfiguration<Answer>(new AnswerConfiguration());
            
            base.OnModelCreating(builder);
        }
        
    }
}