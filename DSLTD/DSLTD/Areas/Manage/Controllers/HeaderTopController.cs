using DSLTD.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class HeaderTopController : Controller
    {
        DataContext _context;
        public HeaderTopController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1 , string? select = null, string? word = null)
        {
            var query = _context.HeaderTops.AsQueryable();

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
                query = query.Where(x => x.Text.Contains(word));
            }

            ViewBag.PageSize = 8;
            TempData["Word"] = word;
            return View(PagenatedList<HeaderTop>.Save(query, page, 8));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HeaderTop headerTop)
        {
            if (!ModelState.IsValid) return View();

            _context.HeaderTops.Add(headerTop);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            HeaderTop header = _context.HeaderTops.FirstOrDefault(x => x.Id == id);

            if (header == null) return RedirectToAction("notfound", "pages");

            return View(header);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HeaderTop header)
        {
            if (!ModelState.IsValid) return View();

            HeaderTop exist = _context.HeaderTops.FirstOrDefault(x => x.Id == header.Id);

            exist.Text = header.Text;
            exist.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            HeaderTop header = _context.HeaderTops.FirstOrDefault(x => x.Id == id);

            if (header == null) return RedirectToAction("notfound", "pages");

            header.IsDeleted = true;

            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
