using Microsoft.EntityFrameworkCore;
using NewsAgency.DAL.Entities;
using NewsAgency.DAL.UnitOfWork;
using NewsAgency.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgency.Services
{
    public class NewsService : INewsService<News, NewsCategory>
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddNewsAsync(News entity)
        {
            await _unitOfWork.News.InsertAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddNewsAsync(IEnumerable<News> entityList)
        {
            await _unitOfWork.News.InsertRangeAsync(entityList);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteNewsAsync(int id, byte[] timeStamp)
        {
            _unitOfWork.News.Delete(id, timeStamp);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteNewsAsync(IEnumerable<News> entities)
        {
            _unitOfWork.News.DeleteRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task<int> GetCountAsync() => await _unitOfWork.News.GetCountAsync();

        public News GetMostViewed() => _unitOfWork.News.GetMostViewed();

        public IEnumerable<News> GetNewsBy(int? NewsCategoryId = null, Expression<Func<News, bool>> filter = null, Func<IQueryable<News>, IOrderedQueryable<News>> orderBy = null, bool isDeleted = false)
        {
            throw new NotImplementedException();
        }

        public async Task<NewsCategory> GetNewsCategoryAsync(int newsId) => await _unitOfWork.News.GetNewsCategoryAsync(newsId);

        public IEnumerable<News> GetNewsList(int? NewsCategoryId = null) => _unitOfWork.News.GetNewsByCategoryId(NewsCategoryId);

        public async Task<News> GetOneNewsAsync(int id) => await _unitOfWork.News.FindAsNoTrackingAsync(id);

        public async Task UpdateNewsAsync(News entityToUpdate)
        {
            _unitOfWork.News.Update(entityToUpdate);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateNewsAsync(IEnumerable<News> entityList)
        {
            _unitOfWork.News.UpdateRange(entityList);
            await _unitOfWork.CommitAsync();
        }
    }
}
