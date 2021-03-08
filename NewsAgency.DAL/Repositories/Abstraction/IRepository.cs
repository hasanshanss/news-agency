using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgency.DAL.Repositories.Abstraction
{
    public interface IRepository<TEntity> : IDisposable
    {
        //Delete
        void  Delete(TEntity entityToDelete);
        void Delete(int id, byte[] timeStamp);
        void DeleteRange(IEnumerable<TEntity> entities);

        //Get
        IEnumerable<TEntity> GetBy(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            bool ignoreQueryFilter = false);
        Task<TEntity> FindAsync(int id);
        Task<TEntity> FindAsNoTrackingAsync(int id);
        Task<TEntity> FindIgnoreQueryFilterAsync(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllIgnoreQueryFilter();

        Task<int> GetCountAsync();

        //Insert&Update
        Task InsertAsync(TEntity entity);
        Task InsertRangeAsync(IEnumerable<TEntity> entity);
        void Update(TEntity entityToUpdate);
        void UpdateRange(IEnumerable<TEntity> entityList);

        IEnumerable<TEntity> GetWithRawSql(string query,
            params object[] parameters);
    }
}
