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
    public class CommentController : Controller
    {
        DataContext _context;
        public CommentController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, bool? select = null)
        {
            var query = _context.ProductComments.Include(x=>x.Product).ThenInclude(x=>x.ProductImages).AsQueryable();

            if (select == true)
            {
                query = query.Where(x => x.Status == true);
            }
            if (select == false)
            {
                query = query.Where(x => x.Status == false);
            }

            ViewBag.PageSize = 8;
            return View(PagenatedList<ProductComment>.Save(query , page , 8));
        }

        public IActionResult Active(int id)
        {
            ProductComment comment = _context.ProductComments.FirstOrDefault(x => x.Id == id);

            if (comment == null) return RedirectToAction("notfound", "pages");

            comment.Status = true;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Reject(int id)
        {
            ProductComment comment = _context.ProductComments.FirstOrDefault(x => x.Id == id);

            if (comment == null) return RedirectToAction("notfound", "pages");

            comment.Status = false;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

    }
}
