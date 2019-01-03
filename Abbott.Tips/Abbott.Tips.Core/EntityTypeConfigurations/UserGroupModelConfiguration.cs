using Abbott.Tips.EntityFrameworkCore.EntityTypeConfigurations;
using Abbott.Tips.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.EntityTypeConfigurations
{
    public class UserGroupModelConfiguration : EntityTypeConfiguration<UserGroupModel, int>
    {
        public override void Configure(EntityTypeBuilder<UserGroupModel> builder)
        {
            base.Configure(builder);

            builder.HasOne(u => u.CreatedUser).WithMany().HasForeignKey(fk => fk.CreatedBy);
            builder.HasOne(u => u.UpdatedUser).WithMany().HasForeignKey(fk => fk.UpdatedBy);

            builder.HasOne(ur => ur.User)
               .WithMany(ur => ur.UserGroups)
               .HasForeignKey(fk => fk.UserID);

            builder.HasOne(ur => ur.Group)
                .WithMany(ur => ur.UserGroups)
                .HasForeignKey(fk => fk.GroupID);

        }
    }
}
