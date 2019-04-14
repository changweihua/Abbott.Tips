using Abbott.Tips.EntityFrameworkCore.UnitOfWork.PagedList;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abbott.Tips.Application.BCL
{
    public interface IAsyncCrudApplicationService<TEntity, TKey> : IApplicationService where TEntity : class
    {
        Task<IPagedList<TEntity>> GetPagerAsync(Expression<Func<TEntity, bool>> predicate = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                         int pageIndex = 0,
                                         int pageSize = 20,
                                         bool disableTracking = true);
    }
}
