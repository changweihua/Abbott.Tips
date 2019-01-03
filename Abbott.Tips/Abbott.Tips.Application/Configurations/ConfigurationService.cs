using Abbott.Tips.EntityFrameworkCore.UnitOfWork;
using Abbott.Tips.EntityFrameworkCore.UnitOfWork.PagedList;
using Abbott.Tips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abbott.Tips.Application.Configurations
{
    public class ConfigurationService : IConfigurationService
    {
        public IUnitOfWork unitOfWork { get; set; }

        public IPagedList<ConfigurationModel> GetConfigurationList(string a)
        {
            Func<IQueryable<ConfigurationModel>, IOrderedQueryable<ConfigurationModel>> orderBy = (b) => b.OrderBy(_ => _.ConfigType).ThenBy(_ => _.ConfigName);
            return unitOfWork.GetRepository<ConfigurationModel>().GetPagedList(orderBy: orderBy);
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
