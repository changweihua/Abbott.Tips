using Abbott.Tips.Model;
using Abbott.Tips.Model.Audition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.EntityFrameworkCore.EntityTypeConfigurations
{
    public class EntityTypeConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity> where TEntity : TipsEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            if (typeof(TEntity) is ISoftDelete)
            {
                builder.HasQueryFilter(p => !p.IsDeleted);
                builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            }

            if (typeof(TEntity) is CreationAuditedEntity<TKey>)
            {
                builder.Property(p => p.CreatedTime).HasDefaultValue();
            }
        }
    }
}
