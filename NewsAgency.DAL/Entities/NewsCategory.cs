using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAgency.DAL.Entities
{
    public class NewsCategory : BaseEntity
    {
        public string Category { get; set; }
        public ICollection<News> News { get; set; }
    }
}
