using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAgency.DAL.Entities;

namespace NewsAgency.DAL.Configurations
{
    class NewsConfiguration : BaseEntityConfiguration<News>
    {
        public override void Configure(EntityTypeBuilder<News> builder)
        {
            base.Configure(builder);

            builder
                .Property(m=>m.CreatedAt)
                .IsRequired()
                .HasColumnType("Date")
                .HasDefaultValueSql("getdate()");

            builder
                .Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(m => m.Content)
                .IsRequired()
                .HasColumnType("Text");

            builder
                .Property(m => m.DisplayName)
                .HasComputedColumnSql("[Title] + ' - ' + convert(nvarchar, CreatedAt, 101)");

            builder
                .Property("Timestamp")
                .IsRowVersion();


            builder
                .HasOne(m => m.NewsCategoryNavigation)
                .WithMany(m=>m.News)
                .HasForeignKey(m => m.NewsCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.ToTable("News");
        }
    }
}
