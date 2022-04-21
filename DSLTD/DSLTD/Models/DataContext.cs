using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class DataContext:IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<ShopOnInstagram> ShopOnInstagrams { get; set; }
        public DbSet<SosialMedia> SosialMedias { get; set; }
        public DbSet<CategoryGender> CategoryGenders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Color> Colors { get; set; }
        //public DbSet<Detail> Details { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        //public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProductImage> ProductsImages { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<WishListItem> WishListItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<HeaderTop> HeaderTops { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Policy> Policies { get; set; }

        public DbSet<BestSaleCategory> BestSaleCategories { get; set; }
    }
}
