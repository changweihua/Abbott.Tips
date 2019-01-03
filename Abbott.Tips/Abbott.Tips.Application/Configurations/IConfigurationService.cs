using Abbott.Tips.EntityFrameworkCore.UnitOfWork.PagedList;
using Abbott.Tips.Framework.Dependency;
using Abbott.Tips.Model.Entities;
using System;
using System.Collections.Generic;

namespace Abbott.Tips.Application.Configurations
{
    public interface IConfigurationService : IDependency
    {
        IPagedList<ConfigurationModel> GetConfigurationList(string a);

        IList<ConfigurationModel> GetTypedConfigurationList(int configType);

        ConfigurationModel GetConfiguration(int cfgId);
    }
}
