using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAgency.DAL.Entities
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public string? DisplayName { get; set; }

        public int Views { get; set; }

        public int NewsCategoryId { get; set; }
        public NewsCategory? NewsCategoryNavigation { get; set; }

    }
}
