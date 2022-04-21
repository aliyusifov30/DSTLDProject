using DSLTD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        DataContext _context;
        public CategoryController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1 , bool? select = null , string word = null)
        
        {
            var query = _context.Categories.AsQueryable();

            if(select == false)
            {
                query = query.Where(x => x.IsDeleted == false);
            }
            if (select == true)
            {
                query = query.Where(x => x.IsDeleted == true);
            }
            if(word != null)
            {
                query = query.Where(x => x.Name.Contains(word));
            }
            
            ViewBag.PageSize = 8;
            return View(PagenatedList<Category>.Save(query,page,8));
        }
        
        public IActionResult Create()
        {
            ViewBag.Genders = _context.Genders.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid) return View();

            ViewBag.Genders = _context.Genders.ToList();

            if (_context.Categories.Any(x => x.Name.Equals(category.Name)))
            {
                ModelState.AddModelError("Name", "This name is taken");
                return View();
            }

            _context.Categories.Add(category);

            _context.SaveChanges();

            for (int i = 0; i < category.GenderIds.Count; i++)
            {
                CategoryGender categoryGender = new CategoryGender()
                {
                    GenderId = category.GenderIds[i],
                    CategoryId = category.Id
                };
                _context.CategoryGenders.Add(categoryGender);
            }

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Genders = _context.Genders.ToList();
            Category category = _context.Categories.Include(x=>x.CategoryGenders).FirstOrDefault(x => x.Id == id);

            var item = category.CategoryGenders.Select(x => x.GenderId).AsQueryable();
            
            category.GenderIds = category.CategoryGenders.Select(x => x.GenderId).ToList();

            if (category == null) return RedirectToAction("notfound", "pages");

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {

            if (!ModelState.IsValid) return View();

            Category exist = _context.Categories.Include(x=>x.CategoryGenders).FirstOrDefault(x => x.Id == category.Id);

            if (exist == null) return RedirectToAction("notfound", "pages");

            exist.Name = category.Name;
            exist.ModifiedAt = category.ModifiedAt;

            exist.CategoryGenders.RemoveAll(x => x.CategoryId == category.Id);

            for (int i = 0; i < category.GenderIds.Count; i++)
            {
                CategoryGender categoryGender = new CategoryGender()
                {
                    GenderId = category.GenderIds[i],
                    CategoryId = category.Id
                };

                _context.CategoryGenders.Add(categoryGender);
            }

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.Include(x=>x.CategoryGenders).FirstOrDefault(x => x.Id == id);

            if (category == null) return RedirectToAction("notfound", "pages");

            category.IsDeleted = true;

            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
