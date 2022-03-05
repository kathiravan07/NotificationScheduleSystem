using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Notification.DataAccess.GenericRepository.Interface
{
    public interface IGenericRepository<TEntity>
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        ICollection<TType> GetSelect<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class;
        Task<ICollection<TType>> GetSelectAsync<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class;
        Task<TEntity> GetByIDAsync(object id);
        Task<TEntity> InsertAsync(TEntity entity);
        List<TEntity> InsertMultiple(List<TEntity> entity);
        TEntity Insert(TEntity entity);
        TEntity GetByID(object id);
        TEntity Update(TEntity entityToUpdate);
        void Delete(object id);
        void Save();
        Task SaveAsync();

    }
}
