using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NewsAgency.DAL.Entities;

namespace NewsAgency.Services.Abstraction
{
    public interface INewsService<TEntity, KEntity> where TEntity : News, new()
                                                    where KEntity : NewsCategory, new()

    {
        //Delete
        Task DeleteNewsAsync(int id, byte[] timeStamp);
        Task DeleteNewsAsync(IEnumerable<TEntity> entities);

        //Get
        Task<TEntity> GetOneNewsAsync(int id);
        IEnumerable<TEntity> GetNewsBy(int? NewsCategoryId = null,
                                        Expression<Func<TEntity, bool>> filter = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                        bool isDeleted = false);

        IEnumerable<TEntity> GetNewsList(int? NewsCategoryId = null);

        Task<KEntity> GetNewsCategoryAsync(int newsId);

        Task<int> GetCountAsync();
        TEntity GetMostViewed();

        //Insert&Update
        Task AddNewsAsync(TEntity entity);
        Task AddNewsAsync(IEnumerable<TEntity> entityList);
        Task UpdateNewsAsync(TEntity entityToUpdate);
        Task UpdateNewsAsync(IEnumerable<TEntity> entityList);

    }
}
