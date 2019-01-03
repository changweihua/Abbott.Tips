using Abbott.Tips.EntityFrameworkCore.EntityTypeConfigurations;
using Abbott.Tips.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.EntityTypeConfigurations
{
    public class UserModelConfiguration : EntityTypeConfiguration<UserModel, int>
    {
        public override void Configure(EntityTypeBuilder<UserModel> builder)
        {
            base.Configure(builder);

            builder.HasOne(u => u.CreatedUser).WithMany().HasForeignKey(fk => fk.CreatedBy);
            builder.HasOne(u => u.UpdatedUser).WithMany().HasForeignKey(fk => fk.UpdatedBy);

            //builder.HasOne(u => u.CreatedUser).WithOne().HasForeignKey<UserModel>(fk => fk.CreatedBy);
            //builder.HasOne(u => u.UpdatedUser).WithOne().HasForeignKey<UserModel>(fk => fk.UpdatedBy);

            //builder.HasMany(u => u.UserRoles).WithOne(o => o.User);
        }
    }
}
