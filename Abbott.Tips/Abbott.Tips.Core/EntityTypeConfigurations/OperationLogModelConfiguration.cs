using Abbott.Tips.EntityFrameworkCore.EntityTypeConfigurations;
using Abbott.Tips.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.EntityTypeConfigurations
{
    public class OperationLogModelConfiguration : EntityTypeConfiguration<OperationLogModel, int>
    {
        public override void Configure(EntityTypeBuilder<OperationLogModel> builder)
        {
            base.Configure(builder);
        }
    }
}
