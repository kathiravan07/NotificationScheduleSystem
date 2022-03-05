using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Notification.DataAccess.GenericRepository.Interface;
using NotificationSystem.infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Notification.DataAccess.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal SchedulerContext context;
        internal DbSet<TEntity> dbSet;
        public GenericRepository(SchedulerContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (string includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public ICollection<TType> GetSelect<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class
        {
            return dbSet.Where(where).Select(select).ToList();
        }
        public async Task<ICollection<TType>> GetSelectAsync<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class
        {
            return await dbSet.Where(where).Select(select).ToListAsync();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (string includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }
        public virtual async Task<TEntity> GetByIDAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }
        public virtual TEntity Insert(TEntity entity)
        {
            EntityEntry<TEntity> insert = dbSet.Add(entity);
            return insert.Entity;
        }
        public virtual List<TEntity> InsertMultiple(List<TEntity> entity)
        {
            dbSet.AddRange(entity);
            return entity;
        }
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            EntityEntry<TEntity> insert = await dbSet.AddAsync(entity);
            return insert.Entity;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void DeleteMultiple(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public virtual TEntity Update(TEntity entityToUpdate)
        {
            context.Update(entityToUpdate);
            return entityToUpdate;
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

    }
}
