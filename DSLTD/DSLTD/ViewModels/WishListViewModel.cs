using DSLTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.ViewModels
{
    public class WishListViewModel
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int Count { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Discount { get; set; }
        public string Name { get; set; }
        public string PosterImage { get; set; }
        public string ColorName { get; set; }

        public Color Color { get; set; }
        public Product Product { get; set; }
        //public string SizeName { get; set; }
    }
}
