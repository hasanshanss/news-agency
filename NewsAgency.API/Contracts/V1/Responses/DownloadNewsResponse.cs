using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgency.API.Contracts.V1.Responses
{
    public class DownloadNewsResponse
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        public override string ToString()
        {
            return $"{Title}:\n\n{Content}\n\n\n";
        }
    }
}
