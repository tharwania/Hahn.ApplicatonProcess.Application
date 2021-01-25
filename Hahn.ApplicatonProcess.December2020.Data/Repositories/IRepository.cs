using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> InsertAsync(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> Queryable();
    }
}
