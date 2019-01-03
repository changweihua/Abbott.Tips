using Abbott.Tips.Framework.Dependency;
using Abbott.Tips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Application.Regions
{
    public interface IRegionService : IDependency
    {
        IList<RegionModel> GetRegions();

        int CreateRegion(RegionModel model);

        int DeleteRegion(RegionModel model);
    }
}
