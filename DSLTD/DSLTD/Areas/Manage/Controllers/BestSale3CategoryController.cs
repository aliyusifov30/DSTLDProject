using DSLTD.Helper;
using DSLTD.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BestSale3CategoryController : Controller
    {
        IWebHostEnvironment _env;
        DataContext _context;
        public BestSale3CategoryController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Gender = _context.Genders.ToList();
            return View(_context.BestSaleCategories.ToList());
        }

        //public IActionResult Create()
        //{
        //    ViewBag.Category = _context.Categories.ToList();
        //    ViewBag.Gender = _context.Genders.ToList();

        //    if (_context.Categories.Count() == 3)
        //    {
        //        return RedirectToAction("index");
        //    }

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(BestSaleCategory bestSaleCategory)
        //{

        //    ViewBag.Category = _context.Categories.ToList();
        //    ViewBag.Gender = _context.Genders.ToList();

        //    if (!ModelState.IsValid) return View();

        //    if (bestSaleCategory.ImageFile == null)
        //    {
        //        ModelState.AddModelError("ImageFile", "Image not null");
        //        return View();
        //    }

        //    if (bestSaleCategory.ImageFile.Length > 10485760)
        //    {
        //        ModelState.AddModelError("ImageFile", "Image must be lower 10mb");
        //        return View();
        //    }

        //    if (bestSaleCategory.ImageFile.ContentType != "image/jpeg" && bestSaleCategory.ImageFile.ContentType != "image/png" && bestSaleCategory.ImageFile.ContentType != "image/gif")
        //    {
        //        ModelState.AddModelError("ImageFile", "Image must be jpeg,png,gif");
        //        return View();
        //    }

        //    bestSaleCategory.Image = FileManager.Save(_env.WebRootPath, "uploads/products", bestSaleCategory.ImageFile);

        //    _context.BestSaleCategories.Add(bestSaleCategory);
        //    _context.SaveChanges();

        //    return RedirectToAction("index");
        //}

        public IActionResult Edit(int id)
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Gender = _context.Genders.ToList();
             
            BestSaleCategory bestSaleCategory = _context.BestSaleCategories.FirstOrDefault(x => x.Id == id);

            if (bestSaleCategory == null) return RedirectToAction("notfound", "pages");

            return View(bestSaleCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BestSaleCategory bestSaleCategory)
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Gender = _context.Genders.ToList();

            if (!ModelState.IsValid) return View();

            BestSaleCategory exist = _context.BestSaleCategories.FirstOrDefault(x => x.Id == bestSaleCategory.Id);

            if (exist == null) return RedirectToAction("notfound", "pages");

            if (bestSaleCategory.ImageFile != null)
            {
                if (bestSaleCategory.ImageFile.Length > 10485760)
                {
                    ModelState.AddModelError("ImageFile", "Image must be lower 10mb");
                    return View();
                }

                if (bestSaleCategory.ImageFile.ContentType != "image/jpeg" && bestSaleCategory.ImageFile.ContentType != "image/png" && bestSaleCategory.ImageFile.ContentType != "image/gif")
                {
                    ModelState.AddModelError("ImageFile", "Image must be jpeg,png,gif");
                    return View();
                }

                FileManager.Delete(_env.WebRootPath, "uplods/colors", exist.Image);

                exist.Image = FileManager.Save(_env.WebRootPath, "uploads/products", bestSaleCategory.ImageFile);
            }

            exist.Title = bestSaleCategory.Title;
            exist.CategoryId = bestSaleCategory.CategoryId;
            exist.GenderId = bestSaleCategory.GenderId;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

    }
}
