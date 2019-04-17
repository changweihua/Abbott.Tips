using Abbott.Tips.Application.BCL;
using Abbott.Tips.Application.Configurations.Dtos;
using Abbott.Tips.EntityFrameworkCore.UnitOfWork.PagedList;
using Abbott.Tips.Framework.Dependency;
using Abbott.Tips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abbott.Tips.Application.Configurations
{
    public interface IConfigurationService : IAsyncCrudApplicationService<ConfigurationModel, int>
    {
        Task<IPagedList<ConfigurationListModel>> GetConfigurationList(string a);

        IList<ConfigurationModel> GetTypedConfigurationList(int configType);

        ConfigurationModel GetConfiguration(int cfgId);
    }
}
