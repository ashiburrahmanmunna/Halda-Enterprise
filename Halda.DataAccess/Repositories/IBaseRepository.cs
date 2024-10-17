using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories
{
    public interface IBaseRepository<TEntity, TKey>
    {

       // void Add(TEntity entity);
        Task AddAsync(TEntity entity);
       // void Edit(TEntity entityToUpdate);
        Task EditAsync(TEntity entityToUpdate);
        IList<TEntity> GetAll();
        Task<IList<TEntity>> GetAllAsync(CancellationToken token);
        TEntity GetById(TKey id);
        Task<TEntity> GetByIdAsync(TKey id, CancellationToken token);
        Task<IQueryable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);
        TEntity FindWhere(Expression<Func<TEntity, bool>> predicate);
        int GetCount(Expression<Func<TEntity, bool>> filter = null);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);
        void Remove(Expression<Func<TEntity, bool>> filter);
        void Remove(TEntity entityToDelete);
        void Remove(TKey id);
        Task RemoveAsync(Expression<Func<TEntity, bool>> filter);
        Task RemoveAsync(TEntity entityToDelete);
        Task RemoveAsync(TKey id);
        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);
        int Save();
        Task<int> SaveAsync();
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Single(Expression<Func<TEntity, bool>> predicate);

    }
}
