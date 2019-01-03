using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abbott.Tips.EntityFrameworkCore.Configurations
{
    public class NoForeignKeySqlServerMigrationsSqlGenerator : SqlServerMigrationsSqlGenerator
    {
        public NoForeignKeySqlServerMigrationsSqlGenerator(MigrationsSqlGeneratorDependencies dependencies, IMigrationsAnnotationProvider migrationsAnnotations) : base(dependencies, migrationsAnnotations)
        {
        }
        //重写这个方法
        protected override void Generate(CreateTableOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            //删除级联关系
            RemoveForeignKeysHelper.ExecuForeignKeys(operation);
            base.Generate(operation, model, builder);
        }

    }

    public class RemoveForeignKeysHelper
    {
        //定义个全局变量，用来存储需要移除的级联属性
        internal static ConcurrentDictionary<string, List<string>> RemoveForeignKeys = new ConcurrentDictionary<string, List<string>>();

        public static void ExecuForeignKeys(CreateTableOperation operation)
        {
            if (RemoveForeignKeys.TryGetValue(operation.Name, out List<string> columns))
            {
                operation.ForeignKeys
                    .Where(item => item.Columns.Intersect(columns).Count() > 0)
                    .ToList()
                    .ForEach(item => operation.ForeignKeys.Remove(item));
            }
        }
    }

    public static class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder<T> RemoveForeignKey<T>(this EntityTypeBuilder<T> builder, string name) where T : class
        {
            var tableName = builder.Metadata.FindAnnotation("Relational:TableName").Value.ToString();
            RemoveForeignKeysHelper.RemoveForeignKeys.AddOrUpdate(tableName, new List<string> { name }, (value, values) => {
                values.Add(name);
                return values.Distinct().ToList();
            });
            return builder;
        }
    }

    public static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder UseRemoveForeignKeyService(this DbContextOptionsBuilder options)
        {
            options.ReplaceService<IMigrationsSqlGenerator, NoForeignKeySqlServerMigrationsSqlGenerator>();
            return options;
        }
    }
}
