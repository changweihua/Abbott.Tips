using Abbott.Tips.EntityFrameworkCore.EntityTypeConfigurations;
using Abbott.Tips.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.EntityTypeConfigurations
{
    public class OrganizationModelConfiguration : EntityTypeConfiguration<OrganizationModel, int>
    {
        public override void Configure(EntityTypeBuilder<OrganizationModel> builder)
        {
            base.Configure(builder);

            builder.HasMany(m => m.SubOrganizations).WithOne(x => x.ParentOrganization).HasForeignKey(fk => fk.ParentID);

            builder.HasOne(u => u.CreatedUser).WithMany().HasForeignKey(fk => fk.CreatedBy);
            builder.HasOne(u => u.UpdatedUser).WithMany().HasForeignKey(fk => fk.UpdatedBy);

        }
    }
}
