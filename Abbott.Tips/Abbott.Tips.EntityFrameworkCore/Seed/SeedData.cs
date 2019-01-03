using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abbott.Tips.EntityFrameworkCore.Seed
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider provider)
        {
            using (var context = new TipsContext(provider.GetRequiredService<DbContextOptions<TipsContext>>()))
            {
                if (context.Users.Any())
                {
                    return;   // 已经初始化过数据
                }

                context.Users.Add(new Model.Entities.UserModel
                {
                    CreatedBy = 1,
                    LoginName = "Lance.Chang",
                    LoginPwd = "LL",
                    UserName = "Lance Chang"
                });

                context.SaveChanges();
            }
        }
    }
}
