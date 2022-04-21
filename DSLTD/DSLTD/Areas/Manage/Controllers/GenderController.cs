using DSLTD.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class GenderController : Controller
    {

        DataContext _context;

        public GenderController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1, bool? select = false , string word = null)
        {
            var query = _context.Genders.AsQueryable();

            if (select == false)
            {
                query = query.Where(x => x.IsDeleted == false);
            }
            if (select == true)
            {
                query = query.Where(x => x.IsDeleted == true);
            }
            if (select == null)
            {
                query = _context.Genders.AsQueryable();
            }

            if(word != null)
            {
                query = query.Where(x => x.Name.Contains(word));
            }

            ViewBag.PageSize = 8;

            return View(PagenatedList<Gender>.Save(query,page,8));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Gender gender)
        {
            if (!ModelState.IsValid) return View();

            if (_context.Genders.Any(x => x.Name.Equals(gender.Name)))
            {
                ModelState.AddModelError("Name", "This name is taken");
                return View();
            }

            _context.Genders.Add(gender);
            _context.SaveChanges();
            
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Gender gender = _context.Genders.FirstOrDefault(x => x.Id == id);
            
            if (gender == null) return RedirectToAction("notfound", "pages");

            return View(gender);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Gender gender)    
        {



            if (!ModelState.IsValid) return View();

            Gender exist = _context.Genders.FirstOrDefault(x => x.Id == gender.Id);

            exist.Name = gender.Name;
            exist.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");

        }
        
        public IActionResult Delete(int id)
        {
            Gender gender = _context.Genders.FirstOrDefault(x => x.Id == id);

            gender.IsDeleted = true;
            
            _context.SaveChanges();

            return RedirectToAction("index");
        }

    }
}
