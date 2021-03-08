using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAgency.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAgency.DAL.Configurations
{
    class NewsCategoryConfiguration : BaseEntityConfiguration<NewsCategory>
    {
        public override void Configure(EntityTypeBuilder<NewsCategory> builder)
        {
            base.Configure(builder);

            builder
                .Property(m => m.Category)
                .IsRequired()
                .HasMaxLength(50);


            builder.ToTable("NewsCategories");
        }
    }
}
