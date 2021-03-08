using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAgency.DAL.Entities;

namespace NewsAgency.DAL.Configurations
{
    class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        { 
            //builder.Property<byte[]?>("Timestamp");
            builder.Property<bool>("IsDeleted").HasDefaultValue(false);
            builder.HasQueryFilter(m => !EF.Property<bool>(m, "IsDeleted"));
        }
    }
}
