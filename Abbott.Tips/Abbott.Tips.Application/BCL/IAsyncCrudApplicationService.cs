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
        /// <summary>
        /// 获取分页结果，基于EntityModel
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        Task<IPagedList<TEntity>> GetPagerAsync(Expression<Func<TEntity, bool>> predicate = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                         int pageIndex = 0,
                                         int pageSize = 20,
                                         bool disableTracking = true);

        /// <summary>
        /// 获取分页结果，基于EntityDtoModel
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        Task<IPagedList<TResult>> GetPagerAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                        Expression<Func<TEntity, bool>> predicate = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                        int pageIndex = 0,
                                        int pageSize = 20,
                                        bool disableTracking = true) where TResult : class;
    }
}
