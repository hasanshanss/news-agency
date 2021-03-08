using NewsAgency.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsCategoryAsyncAgency.Services.Abstraction
{
    public interface INewsCategoryService<TEntity> where TEntity : NewsCategory, new()
                                             


    {
        //Delete
        Task DeleteNewsCategoryAsync(int id, byte[] timeStamp);
        Task DeleteNewsCategoryAsync(IEnumerable<TEntity> entities);

        //Get
        Task<TEntity> GetOneNewsCategoryAsync(int id);

        IAsyncEnumerable<TEntity> GetNewsCategoryListAsync();
        IAsyncEnumerable<TEntity> GetDeletedNewsCategoryListAsync();

        Task<int> GetCountAsync();

        //Insert&Update
        Task AddNewsCategoryAsync(TEntity entity);
        Task AddNewsCategoryAsync(IEnumerable<TEntity> entityList);
        Task UpdateNewsCategoryAsync(TEntity entityToUpdate);
        Task UpdateNewsCategoryAsync(IEnumerable<TEntity> entityList);

    }
}
