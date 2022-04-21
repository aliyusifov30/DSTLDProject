using DSLTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.ViewModels
{
    public class ProductViewModel
    {
        public PagenatedList<Product> Products { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<ProductColor> ProductColors { get; set; }

        public List<Color> Colors { get; set;}
        public List<Size> Sizes { get; set; }
    }
}
