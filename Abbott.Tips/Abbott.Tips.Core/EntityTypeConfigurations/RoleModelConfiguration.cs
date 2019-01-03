using Abbott.Tips.EntityFrameworkCore.EntityTypeConfigurations;
using Abbott.Tips.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.EntityTypeConfigurations
{
    public class RoleModelConfiguration : EntityTypeConfiguration<RoleModel, int>
    {
        public override void Configure(EntityTypeBuilder<RoleModel> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.ParentRole).WithMany(m => m.SubRoles).HasForeignKey(fk => fk.ParentID);

            //builder.HasMany(m => m.SubRoles).WithOne(x => x.ParentRole).HasForeignKey(fk => fk.ParentID);

            builder.HasOne(u => u.CreatedUser).WithMany().HasForeignKey(fk => fk.CreatedBy);
            builder.HasOne(u => u.UpdatedUser).WithMany().HasForeignKey(fk => fk.UpdatedBy);
        }
    }
}
