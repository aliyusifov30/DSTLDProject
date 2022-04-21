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
    public class ShopOnInstagramController : Controller
    {
        DataContext _context;
        IWebHostEnvironment _env;
        public ShopOnInstagramController(DataContext context , IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(string word = null)
        {
            var query = _context.ShopOnInstagrams.AsQueryable();

            if(word != null)
            {
                query = query.Where(x => x.Url.Contains(word) || x.Desc.Contains(word));
            }

            return View(query.ToList());
        }

        public IActionResult Edit(int id)
        {
            ShopOnInstagram post = _context.ShopOnInstagrams.FirstOrDefault(x => x.Id == id);

            if (post == null) return RedirectToAction("notfound", "pages");

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ShopOnInstagram post)
        {
            if (!ModelState.IsValid) return View();

            ShopOnInstagram exist = _context.ShopOnInstagrams.FirstOrDefault(x => x.Id == post.Id);

            if (exist == null) return RedirectToAction("notfound", "pages");

            if (post.ImageFile != null)
            {
                if (post.ImageFile.Length > 10485760)
                {
                    ModelState.AddModelError("", "Image must be lower then 10 mb");
                    return View(post);
                }

                if (post.ImageFile.ContentType != "image/jpeg" && post.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("", "Image must be png,jpeg");
                    return View();
                }


                FileManager.Delete(_env.WebRootPath, "uploads/shop-on-instagram", exist.Image);
            }
            
            exist.Url = post.Url;
            exist.Desc = post.Desc;
            exist.CreateAt = post.CreateAt;

            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
