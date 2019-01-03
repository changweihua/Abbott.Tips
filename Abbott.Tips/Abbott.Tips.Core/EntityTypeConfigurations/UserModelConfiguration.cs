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
        }
    }
}
