using DSLTD.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SizeController : Controller
    {

        DataContext _context;
        public SizeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1, string? select = null , string? word = null)
        {
            var query = _context.Sizes.AsQueryable();

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

            TempData["Word"] = word;

            ViewBag.PageSize = 8;
            return View(PagenatedList<Size>.Save(query,page,8));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Size size)
        {
            if (!ModelState.IsValid) return View();

            if (string.IsNullOrEmpty(size.Name))
            {
                ModelState.AddModelError("Name", "Not null");
                return View();
            }

            if (_context.Sizes.Any(x => x.Name.Equals(size.Name)))
            {
                ModelState.AddModelError("Name", "This name is taken");
                return View();
            }

            _context.Sizes.Add(size);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Size size = _context.Sizes.FirstOrDefault(x => x.Id == id);

            if (size == null) return RedirectToAction("notfound", "pages");

            return View(size);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Size size)
        {

            if (!ModelState.IsValid) return View();

            Size exist = _context.Sizes.FirstOrDefault(x => x.Id == size.Id);

            exist.Name = size.Name;
            exist.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Size size = _context.Sizes.FirstOrDefault(x => x.Id == id);

            size.IsDeleted = true;

            _context.SaveChanges();

            return RedirectToAction("index");
        }


    }
}
