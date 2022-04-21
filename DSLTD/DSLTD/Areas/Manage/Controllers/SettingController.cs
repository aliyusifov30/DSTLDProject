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
    public class SettingController : Controller
    {
        DataContext _context;
        IWebHostEnvironment _env;
        public SettingController(DataContext context , IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1 , string word = null)
        {

            var query = _context.Settings.AsQueryable();

            if (word != null)
            {
                query = query.Where(x => x.Key.Contains(word) || x.Value.Contains(word));
                TempData["Word"] = word;
            }
            ViewBag.PageSize = 8;
            return View(PagenatedList<Setting>.Save(query,page,8));
        }

        public IActionResult Edit(int id)
        {
            Setting setting = _context.Settings.FirstOrDefault(x => x.Id == id);

            if (setting == null) return RedirectToAction("notfound", "pages");

            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Setting setting)  
        {
            if (!ModelState.IsValid) return View();

            Setting exist = _context.Settings.FirstOrDefault(x => x.Id == setting.Id);

            if (exist == null) return RedirectToAction("notfound", "pages");

            if (setting.ImageFile != null)
            {
                if(setting.ImageFile.ContentType != "image/jpeg" && setting.ImageFile.ContentType != "image/png" && setting.ImageFile.ContentType != "image/gif")
                {
                    ModelState.AddModelError("", "Image must be jpeg,png,gif");
                    return View();
                }

                if(setting.ImageFile.Length > 10485760)
                {
                    ModelState.AddModelError("", "Image must be lower then 10 mb");
                    return View();
                }

                FileManager.Delete(_env.WebRootPath, "uploads/settings", exist.Value);
                exist.Value = FileManager.Save(_env.WebRootPath, "uploads/settings", setting.ImageFile);
            }
            else
            {
                exist.Value = setting.Value;
            }

            _context.SaveChanges();

            return RedirectToAction("index","setting");
        }

    }
}
