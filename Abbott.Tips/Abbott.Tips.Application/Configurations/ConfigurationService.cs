using Abbott.Tips.Application.BCL;
using Abbott.Tips.EntityFrameworkCore.UnitOfWork;
using Abbott.Tips.EntityFrameworkCore.UnitOfWork.PagedList;
using Abbott.Tips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abbott.Tips.Application.Configurations
{
    public class ConfigurationService : AsyncCrudApplicationService<ConfigurationModel, int>, IConfigurationService
    {
        public async Task<IPagedList<ConfigurationModel>> GetConfigurationList(string a)
        {
            Func<IQueryable<ConfigurationModel>, IOrderedQueryable<ConfigurationModel>> orderBy = (b) => b.OrderBy(_ => _.ConfigType).ThenBy(_ => _.ConfigName);
            return await GetPagerAsync(orderBy: orderBy);
        }

        public IList<ConfigurationModel> GetTypedConfigurationList(int configType)
        {
            Func<IQueryable<ConfigurationModel>, IOrderedQueryable<ConfigurationModel>> orderBy = (b) => b.OrderBy(_ => _.ConfigName);
            return unitOfWork.GetRepository<ConfigurationModel>().Get().ToList();
        }

        public ConfigurationModel GetConfiguration(int cfgId)
        {
            return unitOfWork.GetRepository<ConfigurationModel>().GetFirstOrDefault(predicate: cfg => !cfg.IsDeleted && cfg.Id == cfgId);
        }
    }
}
