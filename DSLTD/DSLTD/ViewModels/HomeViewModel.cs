using DSLTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.ViewModels
{
    public class HomeViewModel
    {
        public List<Setting> Settings { get; set; }
        public List<ShopOnInstagram> ShopOnInstagrams { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<Gender> Genders { get; set; }

        public List<Product> MostLovedProduct { get; set; }
        public Subscribe Subscribe { get; set; }
        public List<BestSaleCategory> BestSaleCategories { get; set; }

        public List<Product> NewAndTopProduct { get; set; }

    }
}
