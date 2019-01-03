using Abbott.Tips.EntityFrameworkCore.EntityTypeConfigurations;
using Abbott.Tips.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.EntityTypeConfigurations
{
    public class GroupModelConfiguration : EntityTypeConfiguration<GroupModel, int>
    {
        public override void Configure(EntityTypeBuilder<GroupModel> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.ParentGroup).WithMany(m => m.SubGroups).HasForeignKey(fk => fk.ParentId);
        }
    }
}
