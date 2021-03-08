using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgency.API.Options
{
    public class RedisOptions
    {
        public bool Enabled { get; set; }
        public string ConnectionString { get; set; }
    }
}
