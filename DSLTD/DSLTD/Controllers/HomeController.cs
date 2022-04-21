using DSLTD.Models;
using DSLTD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Controllers
{
    public class HomeController : Controller
    {
        DataContext _context;
        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var query = _context.Products.Include(x=>x.ProductColors).ThenInclude(x=>x.Color)
                .Include(x=>x.ProductSizes).ThenInclude(x=>x.Size)
                .Include(x=>x.ProductImages).Include(x=>x.Gender).Include(x=>x.BasketItems).Include(x=>x.WishListItems).Where(x=>x.ProductSizes.Any(ps=>ps.SizeCount!=0))
                .AsQueryable();

            HomeViewModel homeVM = new HomeViewModel()
            {
                Settings = _context.Settings.ToList(),
                ShopOnInstagrams = _context.ShopOnInstagrams.OrderByDescending(x => x.CreateAt).ToList(),
                Genders = _context.Genders.ToList(),
                Categories = _context.Categories.ToList(),
                Products = query.Where(x=>x.ProductImages.Count!=0).ToList(),
                MostLovedProduct = query.OrderBy(x=>x.LoveCount).ToList(),
                BestSaleCategories = _context.BestSaleCategories.ToList(),
                NewAndTopProduct = _context.Products.Where(x=>x.IsNew == true && x.ProductSizes.Any(x=>x.SizeCount!=0)).OrderBy(x=>x.Rating).Take(8).ToList()
            };

            return View(homeVM);
        }

        public IActionResult getInstagramData(int id)
        {
            var post = _context.ShopOnInstagrams.FirstOrDefault(x => x.Id == id);

            return PartialView("_ShopOnInstagramModalPartial", post);
        }
    }
}
