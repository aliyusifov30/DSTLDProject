using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class ProductSize:BaseEntity
    {

        public int SizeId { get; set; }
        public int ProductId { get; set; }


        public Product Product { get; set; }
        public Size Size { get; set; }

        public int SizeCount { get; set; }

        [NotMapped]
        public List<int> SizeCountIds { get; set; }
        [NotMapped]
        public List<int> SizeCountValues { get; set; }

    }
}
