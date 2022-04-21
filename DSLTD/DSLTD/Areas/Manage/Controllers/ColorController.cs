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
    public class ColorController : Controller
    {
        IWebHostEnvironment _env;
        DataContext _context;
        public ColorController(DataContext context , IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1 , string? select = null , string? word = null)
        {
            var query = _context.Colors.AsQueryable();

            if (select == "not-deleted")
            {
                query = query.Where(x => x.IsDeleted == false);
            }
            if (select == "is-deleted")
            {
                query = query.Where(x => x.IsDeleted == true);
            }
            if (word != null)
            {
                query = query.Where(x => x.Name.Contains(word));
            }

            ViewBag.PageSize = 8;
            return View(PagenatedList<Color>.Save(query, page, 8));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Color color)
        {
            if (!ModelState.IsValid) return View();

            if (_context.Colors.Any(x => x.Name.Equals(color.Name)))
            {
                ModelState.AddModelError("Name", "This name is taken");
                return View();
            }
            
            if (color.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image not null");
                return View();
            }

            if (color.ImageFile.Length > 10485760)
            {
                ModelState.AddModelError("ImageFile", "Image must be lower 10mb");
                return View();
            }

            if (color.ImageFile.ContentType != "image/jpeg" && color.ImageFile.ContentType != "image/png" && color.ImageFile.ContentType != "image/gif")
            {
                ModelState.AddModelError("ImageFile", "Image must be jpeg,png,gif");
                return View();
            }

            color.Image = FileManager.Save(_env.WebRootPath, "uploads/colors", color.ImageFile);

            _context.Colors.Add(color);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Color color = _context.Colors.FirstOrDefault(x => x.Id == id);

            if (color == null) return RedirectToAction("notfound", "pages");

            return View(color);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Color color)
        {

            if (!ModelState.IsValid) return View();

            Color exist = _context.Colors.FirstOrDefault(x => x.Id == color.Id);

            if (exist == null) return RedirectToAction("notfound", "pages");

            if(color.ImageFile != null)
            {
                if (color.ImageFile.Length > 10485760)
                {
                    ModelState.AddModelError("ImageFile", "Image must be lower 10mb");
                    return View();
                }

                if (color.ImageFile.ContentType != "image/jpeg" && color.ImageFile.ContentType != "image/png" && color.ImageFile.ContentType != "image/gif")
                {
                    ModelState.AddModelError("ImageFile", "Image must be jpeg,png,gif");
                    return View();
                }
                
                FileManager.Delete(_env.WebRootPath, "uplods/colors", exist.Image);

                exist.Image = FileManager.Save(_env.WebRootPath, "uploads/colors", color.ImageFile);
            }

            _context.SaveChanges();

            return RedirectToAction("index");
        }


        public IActionResult Delete(int id)
        {
            Color color = _context.Colors.FirstOrDefault(x => x.Id == id);

            color.IsDeleted = true;

            _context.SaveChanges();

            return RedirectToAction("index");
        }


    }
}
