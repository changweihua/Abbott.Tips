using Abbott.Tips.EntityFrameworkCore.EntityTypeConfigurations;
using Abbott.Tips.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.EntityTypeConfigurations
{
    public class TEventModelConfiguration : EntityTypeConfiguration<TEventModel, int>
    {
        public override void Configure(EntityTypeBuilder<TEventModel> builder)
        {
            base.Configure(builder);
        }
    }
}
