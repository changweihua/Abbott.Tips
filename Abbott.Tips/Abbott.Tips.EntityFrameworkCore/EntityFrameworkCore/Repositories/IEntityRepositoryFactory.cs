using Abbott.Tips.EntityFrameworkCore.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.EntityFrameworkCore.Repository
{

    /// <summary>
    /// Defines the interfaces for <see cref="IEntityRepository{TEntity}"/> interfaces.
    /// </summary>
    public interface IEntityRepositoryFactory
    {
        /// <summary>
        /// Gets the specified repository for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of type inherited from <see cref="IRepository{TEntity}"/> interface.</returns>
        IEntityRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
