using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgency.API.Contracts.V2
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v2";
        public static readonly string Base = $"{Root}/{Version}";

        public static class News
        {
            public static readonly string GetAll = $"{Base}/news";
        }
    }
}
