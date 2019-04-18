using Abbott.Tips.Application.BCL;
using Abbott.Tips.Application.Configurations.Dtos;
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
        protected virtual async Task ValidateConfigurationAsync(ConfigurationModel configuration)
        {
            await Task.Run(() =>
            {
                if (Repository.Get().Any(c => c.ConfigName == configuration.ConfigName && c.ConfigType == configuration.ConfigType && c.Id != configuration.Id))
                {
                    throw new Exception(configuration.ConfigName);
                }
            });
        }

        public override async Task<TResult> Add<TResult>(ConfigurationModel entity)
        {
            await ValidateConfigurationAsync(entity);

            return await base.Add<TResult>(entity);
        }

        public async Task<IPagedList<ConfigurationListModel>> GetConfigurationList(string a)
        {
            Func<IQueryable<ConfigurationModel>, IOrderedQueryable<ConfigurationModel>> orderBy = (b) => b.OrderBy(_ => _.ConfigType).ThenBy(_ => _.ConfigName);
            return await GetPagerAsync(e => ObjectMapper.Map<ConfigurationListModel>(e), orderBy: orderBy);
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
