using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class Size:BaseEntity
    {

        [StringLength(maximumLength:25)]
        [Required]
        public string Name { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
    }
}
