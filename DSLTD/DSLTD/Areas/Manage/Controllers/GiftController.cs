using DSLTD.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class GiftController : Controller
    {
        DataContext _context;
        public GiftController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, string? select = null, string? word = null)
        {

            var query = _context.Gifts.AsQueryable();

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
                query = query.Where(x => x.Code.Contains(word));
            }

            ViewBag.PageSize = 8;

            TempData["Word"] = word;

            return View(PagenatedList<Gift>.Save(query, page, 8));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Gift gift)
        {
            if (!ModelState.IsValid) return View();

            if (_context.Gifts.Any(x => x.Code.Equals(gift.Code)))
            {
                ModelState.AddModelError("Name", "This name is taken");
                return View();
            }

            if (gift.GiftDiscount < 0)
            {
                ModelState.AddModelError("GiftDiscount", "Discount upper zero");
                return View(gift);
            }

            _context.Gifts.Add(gift);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Gift gift = _context.Gifts.FirstOrDefault(x => x.Id == id);

            if (gift == null) return RedirectToAction("notfound", "pages");

            return View(gift);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Gift gift)
        {

            if (!ModelState.IsValid) return View();

            Gift exist = _context.Gifts.FirstOrDefault(x => x.Id == gift.Id);

            if (gift.GiftDiscount < 0)
            {
                ModelState.AddModelError("GiftDiscount", "Discount upper zero");
                return View(gift);
            }

            exist.Code= gift.Code;
            exist.GiftDiscount = gift.GiftDiscount;
            exist.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");

        }

        public IActionResult Delete(int id)
        {
            Gift gift = _context.Gifts.FirstOrDefault(x => x.Id == id);

            if (gift == null) return RedirectToAction("notfound", "pages");

            gift.IsDeleted = true;

            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
