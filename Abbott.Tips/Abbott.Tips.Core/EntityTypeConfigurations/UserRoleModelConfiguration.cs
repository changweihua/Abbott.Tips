using Abbott.Tips.EntityFrameworkCore.EntityTypeConfigurations;
using Abbott.Tips.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.EntityTypeConfigurations
{
    public class UserRoleModelConfiguration : EntityTypeConfiguration<UserRoleModel, int>
    {
        public override void Configure(EntityTypeBuilder<UserRoleModel> builder)
        {
            base.Configure(builder);

            builder.HasOne(u => u.CreatedUser).WithMany().HasForeignKey(fk => fk.CreatedBy);
            builder.HasOne(u => u.UpdatedUser).WithMany().HasForeignKey(fk => fk.UpdatedBy);

            builder.HasOne(ur => ur.User)
               .WithMany(ur => ur.UserRoles)
               .HasForeignKey(fk => fk.UserID);

            builder.HasOne(ur => ur.Role)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(fk => fk.RoleID);

        }
    }
}
