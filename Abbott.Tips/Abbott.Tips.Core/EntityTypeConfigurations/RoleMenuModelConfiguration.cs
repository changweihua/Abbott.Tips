using Abbott.Tips.EntityFrameworkCore.EntityTypeConfigurations;
using Abbott.Tips.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.EntityTypeConfigurations
{
    public class RoleMenuModelConfiguration : EntityTypeConfiguration<RoleMenuModel, int>
    {
        public override void Configure(EntityTypeBuilder<RoleMenuModel> builder)
        {
            base.Configure(builder);

            builder.HasOne(ur => ur.Menu)
               .WithMany(ur => ur.RoleMenus)
               .HasForeignKey(fk => fk.MenuId);

            builder.HasOne(ur => ur.Role)
                .WithMany(ur => ur.RoleMenus)
                .HasForeignKey(fk => fk.RoleId);

        }
    }
}
