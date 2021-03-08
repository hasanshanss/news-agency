using NewsAgency.DAL.Entities;
using System;
using System.Collections.Generic;
using NewsAgency.DAL.Repositories.Abstraction;
using System.Text;

namespace NewsAgency.DAL.Repositories
{
    class NewsCategoryRepository : BaseRepository<NewsCategory>, 
                                   INewsCategoryRepository
    {
        public NewsCategoryRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
