using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgency.API.Contracts.V2.Requests
{
    public class CreateNewsRequest
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Content { get; set; }
        [Required]
        public int NewsCategoryId { get; set; }
    }
}
