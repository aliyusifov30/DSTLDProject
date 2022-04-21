using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class Color:BaseEntity
    {

        [StringLength(maximumLength:100)]
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public List<ProductColor> ProductColors { get; set; }

    }
}
