using DSLTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.ViewModels
{
    public class ProducDetailViewModel
    {
        public Product Product { get; set; }
        public List<Product> BestRatingProduct { get; set; }
        public List<Setting> Setting { get; set; }
        public ProductComment ProductComment { get; set; }
        public List<ProductComment> ProductComments { get; set; }

        public List<ProductImage> ProductImages { get; set; }
    }
}
