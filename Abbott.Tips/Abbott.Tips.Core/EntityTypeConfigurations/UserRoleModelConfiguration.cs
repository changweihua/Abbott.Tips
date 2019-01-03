using Abbott.Tips.EntityFrameworkCore.EntityTypeConfigurations;
using Abbott.Tips.Model.Entities;
using Microsoft.EntityFrameworkCore;
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

            builder.HasOne(ur => ur.User)
               .WithMany(ur => ur.UserRoles)
               .HasForeignKey(fk => fk.UserId);

            builder.HasOne(ur => ur.Role)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(fk => fk.RoleId);

            builder.HasIndex(p => p.RoleId)
                .HasName("RoleId")
                .IsUnique(false);

            builder.HasIndex(p => p.UserId)
                .HasName("UserId")
                .IsUnique(false);
        }
    }
}
