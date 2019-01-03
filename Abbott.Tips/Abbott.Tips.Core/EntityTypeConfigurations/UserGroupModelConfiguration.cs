using Abbott.Tips.EntityFrameworkCore.Configurations;
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

            builder.HasOne(ur => ur.User)
               .WithMany(ur => ur.UserGroups)
               .HasForeignKey(fk => fk.UserId);

            builder.HasOne(ur => ur.Group)
                .WithMany(ur => ur.UserGroups)
                .HasForeignKey(fk => fk.GroupId);

            //builder.RemoveForeignKey("CreatedUser");
            //builder.RemoveForeignKey("UpdatedUser");

            builder.HasIndex(p => p.GroupId)
                .HasName("GroupId")
                .IsUnique(false);

            builder.HasIndex(p => p.UserId)
                .HasName("UserId")
                .IsUnique(false);

        }
    }
}
