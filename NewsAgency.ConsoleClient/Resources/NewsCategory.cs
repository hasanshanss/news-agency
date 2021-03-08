using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAgency.ConsoleClient.Resources
{
    public class NewsCategory
    {
        public string Category { get; set; }
        public ICollection<News> News { get; set; }
    }
}
