using Abbott.Tips.Model.Entities;
using Abbott.Tips.Model.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abbott.Tips.EntityFrameworkCore
{
    /// <summary>
    /// HGDX DbContext
    /// </summary>
    public class TipsContext : DbContext
    {
        public TipsContext(DbContextOptions<TipsContext> options)
            : base(options)
        {
            //设置数据库Command永不超时
            //this.Database.SetCommandTimeout(0);

            //解决团队开发中，多人迁移数据库造成的修改覆盖问题。
            //this.Database.se<TipsContext>(null);
            ////关闭EF6.x 默认自动生成null判断语句
            //base.Configuration.UseDatabaseNullSemantics = true;    

            //DbContext.ChangeTracker.StateChanged事件，会在DbContext中被Track的实体其EntityState状态值发生变化时被触发
            this.ChangeTracker.StateChanged += (sender, entityStateChangedEventArgs) =>
            {
                //如果实体状态变为了EntityState.Modified，那么就尝试设置其UpdateTime属性为当前系统时间DateTime.Now，如果实体没有UpdateTime属性，会抛出InvalidOperationException异常，所以下面要用try catch来捕获异常避免系统报错
                if (entityStateChangedEventArgs.NewState == EntityState.Modified)
                {
                    try
                    {
                        //如果是Person表的实体那么下面的Entry.Property("UpdateTime")就不会抛出异常
                        entityStateChangedEventArgs.Entry.Property("UpdateTime").CurrentValue = DateTime.Now;
                    }
                    catch (InvalidOperationException)
                    {
                        //如果上面try中抛出InvalidOperationException，就是实体没有属性UpdateTime，应该是表Book的实体
                    }
                }

                //如果要自动更新多列，比如还要自动更新实体的UpdateUser属性值到数据库，可以像下面这样再加一个try catch来更新UpdateUser属性
                //if (entityStateChangedEventArgs.NewState == EntityState.Modified)
                //{
                //    try
                //    {
                //        entityStateChangedEventArgs.Entry.Property("UpdateUser").CurrentValue = currentUser;
                //    }
                //    catch (InvalidOperationException)
                //    {
                //    }
                //}
            };
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=localhost;User Id=sa;Password=1qaz!QAZ;Database=EFDemo");
            }
        }

        public DbSet<ConfigurationModel> Configurations { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<MenuModel> Menus { get; set; }

        public DbSet<RoleModel> Roles { get; set; }

        public DbSet<GroupModel> Groups { get; set; }

        public DbSet<UserGroupModel> UserGroups { get; set; }

        public DbSet<RoleMenuModel> RoleMenus { get; set; }

        public DbSet<UserRoleModel> UserRoles { get; set; }

        public DbSet<OperationLogModel> OperationLogs { get; set; }

        public DbSet<RegionModel> Regions { get; set; }

        public DbSet<TEventModel> Events { get; set; }

        public DbSet<OrganizationModel> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.EnableAutoHistory(null);

            modelBuilder.Query<ConfigurationListModel>();
            modelBuilder.Query<UserMenuPermissionModel>();
            modelBuilder.Query<UserPermissionModel>();
            modelBuilder.Query<RoleMenuListModel>();
            modelBuilder.Query<ShareCategoryListModel>();

            var mappingInterface = typeof(IEntityTypeConfiguration<>);
            // Types that do entity mapping
            var mappingTypes = GetType().GetTypeInfo().Assembly.GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == mappingInterface) && x.Name.EndsWith("ModelConfiguration"));

            // Get the generic Entity method of the ModelBuilder type
            var entityMethod = typeof(ModelBuilder).GetMethods()
                .Single(x => x.Name == "Entity" &&
                        x.IsGenericMethod &&
                        x.ReturnType.Name == "EntityTypeBuilder`1");

            foreach (var mappingType in mappingTypes)
            {
                // Get the type of entity to be mapped
                var genericTypeArg = mappingType.GetInterfaces().Single().GenericTypeArguments.FirstOrDefault();

                // Get the method builder.Entity<TEntity>
                var genericEntityMethod = entityMethod.MakeGenericMethod(genericTypeArg);

                // Invoke builder.Entity<TEntity> to get a builder for the entity to be mapped
                var entityBuilder = genericEntityMethod.Invoke(modelBuilder, null);

                // Create the mapping type and do the mapping
                var mapper = Activator.CreateInstance(mappingType);
                mapper.GetType().GetMethod("Configure").Invoke(mapper, new[] { entityBuilder });
            }
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //var mappingInterface = typeof(IEntityTypeConfiguration<>);
            //// Types that do entity mapping
            //var mappingTypes = GetType().GetTypeInfo().Assembly.GetTypes()
            //    .Where(x => x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == mappingInterface));

            //// Get the generic Entity method of the ModelBuilder type
            //var entityMethod = typeof(ModelBuilder).GetMethods()
            //    .Single(x => x.Name == "Entity" &&
            //            x.IsGenericMethod &&
            //            x.ReturnType.Name == "EntityTypeBuilder`1");

            //foreach (var mappingType in mappingTypes)
            //{

            //    // Get the type of entity to be mapped
            //    var genericTypeArg = mappingType.GetInterfaces().Single().GenericTypeArguments.FirstOrDefault();

            //    // Get the method builder.Entity<TEntity>
            //    var genericEntityMethod = entityMethod.MakeGenericMethod(genericTypeArg);

            //    // Invoke builder.Entity<TEntity> to get a builder for the entity to be mapped
            //    var entityBuilder = genericEntityMethod.Invoke(modelBuilder, null);

            //    // Create the mapping type and do the mapping
            //    var mapper = Activator.CreateInstance(mappingType);
            //    mapper.GetType().GetMethod("Configure").Invoke(mapper, new[] { entityBuilder });
            //}
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

        }
    }
}
