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
    public class SearchController : Controller
    {
        DataContext _context;
        public SearchController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index(string? word,int? genderId, int page = 1, string? size = null, string? color = null, string? sort = null)
        {

            TempData["Word"] = word;
            ViewBag.GenderId = genderId;
            ViewBag.PageIndex = page;
            ViewBag.SizeName = size;
            ViewBag.ColorName = color;
            ViewBag.Sort = sort;
            
            if (word != null)
            {
                ViewBag.Word = word;
            }

            var queryProductSizes = _context.ProductSizes.Include(x => x.Size).Include(x => x.Product).Where(x => !x.IsDeleted);

            List<Product> products = new List<Product>();

            var productQuery = _context.Products.Include(x => x.ProductColors).ThenInclude(x => x.Color)
                .Include(x => x.ProductSizes).ThenInclude(x => x.Size)
                .Include(x => x.ProductImages).Include(x => x.Gender).Include(x => x.WishListItems)
                .Where(x=>x.Name.Contains(word) && x.ProductImages.Count!=0);

            if (genderId != null)
            {
                productQuery = productQuery.Where(x => x.GenderId == genderId && !x.IsDeleted);
            }
            if (size != null)
            {
                productQuery = productQuery.Where(x => x.ProductSizes.FirstOrDefault(x => x.Size.Name == size).Size.Name == size && !x.IsDeleted);
            }
            if (color != null)
            {
                productQuery = productQuery.Where(x => x.ProductColors.FirstOrDefault(x => x.Color.Name == color).Color.Name == color && !x.IsDeleted);
            }
            if (sort == "isNew")
            {
                productQuery = productQuery.Where(x => x.IsNew && !x.IsDeleted);
            }
            if (sort == "best-selling")
            {
                productQuery = productQuery.OrderByDescending(x => x.Rating);
            }
            if (sort == "price-ascending")
            {
                productQuery = productQuery.OrderBy(x => x.SalePrice);
            }
            if (sort == "price-descending")
            {
                productQuery = productQuery.OrderByDescending(x => x.SalePrice);
            }

            PagenatedList<Product> newProducts = PagenatedList<Product>.Save(productQuery, page, 10);

            SearchViewModel searchVM = new SearchViewModel()
            {
                Products = newProducts,
                Categories = _context.Categories.Where(x => x.Name.Contains(word)).ToList(),
                ProductSizes = _context.ProductSizes.Include(x => x.Size).Include(x => x.Product).ToList(),
                ProductColors = _context.ProductColors.Include(x => x.Color).Include(x => x.Product).ToList(),
                Colors = _context.Colors.ToList(),
                Sizes = _context.Sizes.ToList(),
                SimpleListProducts = productQuery.Where(x => x.Name.Contains(word) || x.SalePrice.ToString().Contains(word) && x.ProductImages.Count!=0).ToList()
            };

            if (!string.IsNullOrWhiteSpace(size))
            {
                ViewBag.SizeActive = size;
            }
            if (!string.IsNullOrWhiteSpace(color))
            {
                ViewBag.ColorActive = color;
            }

            return View(searchVM);
        }

        public IActionResult Searching(string word)
        {
            var productQuery = _context.Products.Include(x => x.ProductImages).Where(x => x.Name.Contains(word) || x.SalePrice.ToString().Contains(word));
            
            TempData["ProductCount"] = productQuery.Count();
            TempData["Word"] = word;

            SearchViewModel searchVM = new SearchViewModel()
            {
                SimpleListProducts = productQuery.Take(6).ToList(),
                Categories = _context.Categories.Where(x => x.Name.Contains(word)).ToList()
            };

            return PartialView("_SearchPartial", searchVM);
        }

    }
}
