using NewsAgency.DAL.Entities;
using System;
using System.Collections.Generic;
using NewsAgency.DAL.Repositories.Abstraction;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgency.DAL.Repositories
{
    class NewsRepository : BaseRepository<News>, 
                           INewsRepository
    {
      
        public NewsRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public News GetMostViewed()
        {
            return dbSet.Aggregate((news1, news2) => news1.Views > news2.Views ? news1 : news2);
        }

        public IEnumerable<News> GetNewsByCategoryId(int? categoryId = null)
        {
            IQueryable<News> query = dbSet.AsNoTracking();

            if (categoryId.HasValue)
                query = query.Where(m => m.NewsCategoryId == categoryId);

            return  query;
        }

        public async Task<NewsCategory?> GetNewsCategoryAsync(int newsId)
        {
            return await dbSet
                            .Where(m => m.Id == newsId)
                            .Select(m=>m.NewsCategoryNavigation)
                            .FirstOrDefaultAsync();
        }

        public async Task<int> GetViewCountAsync(int newsId)
        {
            return await dbSet
                            .Where(m => m.Id == newsId)
                            .Select(m => m.Views)
                            .FirstOrDefaultAsync();
        }
    }
}
