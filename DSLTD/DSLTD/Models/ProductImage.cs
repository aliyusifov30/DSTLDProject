using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class ProductImage:BaseEntity
    {
        [StringLength(maximumLength:100)]
        public string Image { get; set; }
        public int ProductId { get; set; }
        public bool? PosterStatus { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public Product Product { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }
    }
}
