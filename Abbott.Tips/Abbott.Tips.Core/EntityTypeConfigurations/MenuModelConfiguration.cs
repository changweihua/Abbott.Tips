using Abbott.Tips.EntityFrameworkCore.EntityTypeConfigurations;
using Abbott.Tips.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.EntityTypeConfigurations
{
    public class MenuModelConfiguration : EntityTypeConfiguration<MenuModel, int>
    {
        public override void Configure(EntityTypeBuilder<MenuModel> builder)
        {
            base.Configure(builder);

            builder.HasMany(m => m.SubMenus).WithOne(x => x.ParentMenu).HasForeignKey(fk => fk.ParentID);

            builder.HasOne(u => u.CreatedUser).WithMany().HasForeignKey(fk => fk.CreatedBy);
            builder.HasOne(u => u.UpdatedUser).WithMany().HasForeignKey(fk => fk.UpdatedBy);

        }
    }
}
