﻿using HobbyBubSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HobbyHubSystem.Data.EntityConfiguration
{
    internal class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {

        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder
                   .Property(a => a.PublishDate)
                   .HasDefaultValueSql("GETDATE()");

            builder
                .Property(a => a.IsApproved)
                .HasDefaultValue(false);

        }
    }
}
