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
    public class SosialMediaController : Controller
    {
        DataContext _context;
        IWebHostEnvironment _env;
        public SosialMediaController(DataContext context , IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string word = null) 
        {
            var query = _context.SosialMedias.AsQueryable();

            if (word != null)
            {
                query = query.Where(x => x.Url.Contains(word) || x.Icon.Contains(word));
            }

            ViewBag.PageSize = 8;
            
            return View(PagenatedList<SosialMedia>.Save(query,page,8));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SosialMedia media)
        {

            if (!ModelState.IsValid) return View();

            if (_context.SosialMedias.Any(x => x.Icon.Equals(media.Icon)))
            {
                ModelState.AddModelError("Icon", "This name is taken");
                return View();
            }

            _context.SosialMedias.Add(media);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            SosialMedia media = _context.SosialMedias.FirstOrDefault(x => x.Id == id);

            if (media == null) return View();

            return View(media);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SosialMedia media)
        {
            if (media == null) return RedirectToAction("notfound", "pages");

            if (!ModelState.IsValid) return View();

            if (_context.SosialMedias.Any(x => x.Icon.Equals(media.Icon)))
            {
                ModelState.AddModelError("Icon", "This name is taken");
                return View();
            }

            SosialMedia exist = _context.SosialMedias.FirstOrDefault(x => x.Id == media.Id);

            exist.Url = media.Url;
            exist.Icon = media.Icon;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            SosialMedia media = _context.SosialMedias.FirstOrDefault(x => x.Id == id);

            _context.SosialMedias.Remove(media);

            _context.SaveChanges();

            return RedirectToAction("index");
        }

    }
}
