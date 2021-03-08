using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAgency.ConsoleClient.Resources
{
    public class News
    {
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public string? DisplayName { get; set; }

        public int Views { get; set; }

        public int NewsCategoryId { get; set; }

        public byte[]? Timestamp { get; set; }
        public NewsCategory? NewsCategoryNavigation { get; set; }


    }
}
