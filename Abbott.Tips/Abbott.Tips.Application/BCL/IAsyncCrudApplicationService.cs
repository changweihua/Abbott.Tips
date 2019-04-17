using Abbott.Tips.EntityFrameworkCore.UnitOfWork.PagedList;
using Abbott.Tips.Model.Audition;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abbott.Tips.Application.BCL
{
    public interface IAsyncCrudApplicationService<TEntity, TKey> : IApplicationService where TEntity : IdentityKeyEntity<TKey>
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

        /// <summary>
        /// 查询单个实体 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

        /// <summary>
        /// 查询单个实体,自定义返回类型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        Task<TResult> Get<TResult>(Expression<Func<TEntity, TResult>> selector = null,
                                                  Expression<Func<TEntity, bool>> predicate = null,
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                  bool disableTracking = true) where TResult : class;

        Task<TEntity> Add(TEntity entity);

        Task<TResult> Add<TResult>(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task<TResult> Update<TResult>(TEntity entity);

        Task<TEntity> Delete(TEntity entity);

        Task<TEntity> Delete(TKey key);

        Task<TResult> Delete<TResult>(TEntity entity);

        Task<TResult> Delete<TResult>(TKey key);
    }
}
