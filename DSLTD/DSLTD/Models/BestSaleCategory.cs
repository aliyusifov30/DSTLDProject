using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class BestSaleCategory:BaseEntity
    {

        public int CategoryId { get; set; }
        public string Title { get; set; }
        public int GenderId { get; set; }


        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [StringLength(maximumLength:100)]
        public string Image { get; set; }
        public Category Category { get; set; }
        public Gender Gender { get; set; }
    }
}
