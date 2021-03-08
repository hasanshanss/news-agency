using NewsAgency.DAL.Entities;
using NewsAgency.DAL.Repositories;
using NewsAgency.DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgency.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        INewsRepository News { get; }
        INewsCategoryRepository NewsCategories { get; }
        Task CommitAsync();
    }
}
