using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using NewsAgency.DAL.Entities;
using NewsAgency.DAL.Repositories;
using NewsAgency.DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgency.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _dbContext;
        private INewsRepository _news;
        private INewsCategoryRepository _newsCategories;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public INewsRepository News
        {
            get
            {
                return _news ?? (_news = new NewsRepository(_dbContext));
            }
        }

        public INewsCategoryRepository NewsCategories
        {
            get
            {
                return _newsCategories ?? (_newsCategories = new NewsCategoryRepository(_dbContext));
            }
        }

     

        public async Task CommitAsync()
        {
            try
            {
                HandleSoftDelete();

                await _dbContext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private void HandleSoftDelete()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {

                switch (entry.State)
                {
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }
        }
    }
}
