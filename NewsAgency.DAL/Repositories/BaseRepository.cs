using Microsoft.EntityFrameworkCore;
using NewsAgency.DAL.Entities;
using NewsAgency.DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NewsAgency.DAL.Repositories
{
    abstract class BaseRepository<TEntity> : IRepository<TEntity> 
                                            where TEntity : BaseEntity, new()
    {
        internal ApplicationDbContext context;
        internal DbSet<TEntity> dbSet;

        protected BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            //var entry = context.Entry(entityToDelete);

            //if (entry.State == EntityState.Detached)
            //    dbSet.Attach(entityToDelete);

            //var IsDeletedProperty = entry.Property("IsDeleted");
            //IsDeletedProperty.CurrentValue = true;
            //IsDeletedProperty.IsModified = true;

            dbSet.Remove(entityToDelete);

        }

        public virtual void Delete(int id, byte[] timeStamp)
        {
            var entityToDelete = new TEntity { Id = id };
            context.Entry(entityToDelete).Property("Timestamp").CurrentValue = timeStamp;
            dbSet.Remove(entityToDelete);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            //foreach (var entity in entities)
            //    await DeleteAsync(entity);
            dbSet.RemoveRange(entities);
        }

        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }

        public async virtual Task<TEntity> FindAsync(int id) => await dbSet.FindAsync(id);

        public async virtual Task<TEntity> FindAsNoTrackingAsync(int id) 
        {
            return await dbSet
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async virtual Task<TEntity> FindIgnoreQueryFilterAsync(int id)
        {
            return await dbSet
                            .IgnoreQueryFilters()
                            .FirstOrDefaultAsync(m => m.Id == id);
        }


        public virtual IEnumerable<TEntity> GetAll() => dbSet;
        public virtual IEnumerable<TEntity> GetAllIgnoreQueryFilter() => dbSet.IgnoreQueryFilters();
       
        public virtual IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> filter = null, 
                                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
                                                                  string includeProperties = "", 
                                                                  bool ignoreQueryFilter = false)
                        {
                            IQueryable<TEntity> query = dbSet;

                            if (ignoreQueryFilter)
                            {
                                query = query.IgnoreQueryFilters();
                            }

                            if (filter != null)
                            {
                                query = query.Where(filter);
                            }

                            if (includeProperties != null)
                            {
                                foreach (var includeProperty in includeProperties.Split
                                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    query = query.Include(includeProperty);
                                }
                            }

                            return orderBy != null ? orderBy(query) : query;
                        }

        public async Task<int> GetCountAsync() => await dbSet.CountAsync();
        public  virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters) => dbSet.FromSqlRaw(query, parameters);
        public async virtual Task InsertAsync(TEntity entity) => await dbSet.AddAsync(entity);
        public async virtual Task InsertRangeAsync(IEnumerable<TEntity> entities) => await dbSet.AddRangeAsync(entities);
        public virtual void Update(TEntity entityToUpdate) => dbSet.Update(entityToUpdate);
        public virtual void UpdateRange(IEnumerable<TEntity> entityList) => dbSet.UpdateRange(entityList);

    }
}
