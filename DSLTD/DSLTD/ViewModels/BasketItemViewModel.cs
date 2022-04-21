using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.ViewModels
{
    public class BasketViewModel
    {
        public decimal TotalAmount { get; set; }
        public List<BasketItemViewModel> basketItems { get; set; }
    }
    public class BasketItemViewModel
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Discount { get; set; }
        public string Name { get; set; }
        public string PosterImage { get; set; }
        public string SizeName { get; set; }
        public int SizeId { get; set; }
    
        public int ColorId { get; set; }
        public string ColorName { get; set; }

        public decimal Price { get; set; }
    }
}
