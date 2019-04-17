using Abbott.Tips.Core.Mapper;
using Abbott.Tips.EntityFrameworkCore.UnitOfWork;
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
    public class AsyncCrudApplicationService<TEntity, TKey> : IAsyncCrudApplicationService<TEntity, TKey> where TEntity : IdentityKeyEntity<TKey>
    {
        public IObjectMapper ObjectMapper { get; set; }

        public IUnitOfWork unitOfWork { get; set; }

        protected IEntityRepository<TEntity> Repository
        {
            get
            {
                return unitOfWork.GetRepository<TEntity>();
            }
        }

        public async Task<IPagedList<TEntity>> GetPagerAsync(Expression<Func<TEntity, bool>> predicate = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                         int pageIndex = 0,
                                         int pageSize = 20,
                                         bool disableTracking = true)
        {
            return await Repository.GetPagedListAsync(predicate, orderBy, include, pageIndex, pageSize, disableTracking);
        }

        public async Task<IPagedList<TResult>> GetPagerAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                        Expression<Func<TEntity, bool>> predicate = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                        int pageIndex = 0,
                                        int pageSize = 20,
                                        bool disableTracking = true) where TResult : class
        {
            return await Repository.GetPagedListAsync(selector, predicate, orderBy, include, pageIndex, pageSize, disableTracking);
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            return await Repository.GetFirstOrDefaultAsync(predicate, orderBy, include, disableTracking);
        }

        public async Task<TResult> Get<TResult>(Expression<Func<TEntity, TResult>> selector = null,
                                                  Expression<Func<TEntity, bool>> predicate = null,
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                  bool disableTracking = true) where TResult : class
        {
            if (selector == null)
            {
                selector = (entity) => ObjectMapper.Map<TResult>(entity);
            }
            return await Repository.GetFirstOrDefaultAsync(selector, predicate, orderBy, include, disableTracking);
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await Repository.InsertAsync(entity);
            await unitOfWork.SaveChangesAsync();
            return await Task.FromResult(entity);
        }

        public async Task<TResult> Add<TResult>(TEntity entity)
        {
            await Repository.InsertAsync(entity);
            await unitOfWork.SaveChangesAsync();
            return await Task.FromResult(ObjectMapper.Map<TResult>(entity));
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            Repository.Update(entity);
            await unitOfWork.SaveChangesAsync();
            return await Task.FromResult(entity);
        }

        public async Task<TResult> Update<TResult>(TEntity entity)
        {
            //var estEntity = Repository.GetFirstOrDefault(predicate: e => e.Id.Equals(entity.Id));
            //var modifiedEntity = ObjectMapper.Map(entity, estEntity);
            Repository.Update(entity);
            unitOfWork.SaveChanges();
            return await Task.FromResult(ObjectMapper.Map<TResult>(entity));
        }

        public async Task<TEntity> Delete(TEntity entity)
        {
            Repository.Delete(entity);
            unitOfWork.SaveChanges();
            return await Task.FromResult(entity);
        }

        public async Task<TEntity> Delete(TKey key)
        {
            Repository.Delete(key);
            unitOfWork.SaveChanges();
            return await Get(predicate: e => e.Id.Equals(key));
        }

        public async Task<TResult> Delete<TResult>(TEntity entity)
        {
            if(entity is ISoftDelete)
            {
                (entity as ISoftDelete).IsDeleted = true;
                Repository.Update(entity);
            }
            else
            {
                Repository.Delete(entity);
            }
            unitOfWork.SaveChanges();
            return await Task.FromResult(ObjectMapper.Map<TResult>(entity));
        }

        public async Task<TResult> Delete<TResult>(TKey key)
        {
            Repository.Delete(key);
            unitOfWork.SaveChanges();
            var entity = await Get(predicate: e => e.Id.Equals(key));
            return await Task.FromResult(ObjectMapper.Map<TResult>(entity));
        }

    }
}
