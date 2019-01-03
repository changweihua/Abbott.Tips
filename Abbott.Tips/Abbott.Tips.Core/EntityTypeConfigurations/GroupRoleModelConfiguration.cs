using Abbott.Tips.EntityFrameworkCore.EntityTypeConfigurations;
using Abbott.Tips.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.EntityTypeConfigurations
{
    public class GroupRoleModelConfiguration : EntityTypeConfiguration<GroupRoleModel, int>
    {
        public override void Configure(EntityTypeBuilder<GroupRoleModel> builder)
        {
            base.Configure(builder);

            builder.HasOne(u => u.CreatedUser).WithMany().HasForeignKey(fk => fk.CreatedBy);
            builder.HasOne(u => u.UpdatedUser).WithMany().HasForeignKey(fk => fk.UpdatedBy);

            builder.HasOne(ur => ur.Role)
               .WithMany(ur => ur.RoleGroups)
               .HasForeignKey(fk => fk.RoleID);

            builder.HasOne(ur => ur.Group)
                .WithMany(ur => ur.GroupRoles)
                .HasForeignKey(fk => fk.GroupID);

        }
    }
}
