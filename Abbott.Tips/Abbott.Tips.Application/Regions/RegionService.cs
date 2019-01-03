using Abbott.Tips.EntityFrameworkCore.UnitOfWork;
using Abbott.Tips.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Abbott.Tips.Application.Regions
{
    public class RegionService : IRegionService
    {
        public IUnitOfWork unitOfWork { get; set; }

        public IList<RegionModel> GetRegions()
        {
            Func<IQueryable<RegionModel>, IOrderedQueryable<RegionModel>> orderBy = (b) => b.OrderBy(_ => _.Id);
            Expression<Func<RegionModel, bool>> predicate = region => !region.IsDeleted;


            return unitOfWork.GetRepository<RegionModel>().Get(predicate: predicate).ToList();
        }

        public int CreateRegion(RegionModel model)
        {
            unitOfWork.GetRepository<RegionModel>().Insert(model);

            return unitOfWork.SaveChanges();
        }

        public int DeleteRegion(RegionModel model)
        {
            var estModel = unitOfWork.GetRepository<RegionModel>().GetFirstOrDefault(predicate: region => !region.IsDeleted && region.Id == model.Id);

            estModel.UpdatedBy = model.UpdatedBy;
            estModel.UpdatedTime = model.UpdatedTime;
            estModel.IsDeleted = true;

            unitOfWork.GetRepository<RegionModel>().Update(estModel);

            return unitOfWork.SaveChanges();
        }

    }
}
