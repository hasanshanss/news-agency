using NewsAgency.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgency.DAL.Repositories.Abstraction
{
    public interface INewsRepository : IRepository<News>
    {
        Task<NewsCategory?> GetNewsCategoryAsync(int newsId);
        News GetMostViewed();
        Task<int> GetViewCountAsync(int newsId);

        IEnumerable<News> GetNewsByCategoryId(int? categoryId = null);
        
    }
}
