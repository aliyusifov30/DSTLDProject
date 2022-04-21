using DSLTD.Models;
using DSLTD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Controllers
{
    public class PagesController : Controller
    {
        DataContext _context;
        public PagesController(DataContext context)
        {
            _context = context;
        }
        public IActionResult ContactUs()
        {
            return View(_context.Settings.FirstOrDefault(x => x.Key == "contact-us"));
        }

        public IActionResult FAQ()
        {
            return View(_context.Settings.FirstOrDefault(x=>x.Key=="faq-text"));
        }
        
        public IActionResult NotFound()
        {
            return View();
        }

        public IActionResult OurStory()
        {

            OurStoryViewModel ourStory = new OurStoryViewModel
            {
                SuperText = _context.Settings.FirstOrDefault(x => x.Key == "our-story").Value,
                Image = _context.Settings.FirstOrDefault(x => x.Key == "our-story-image").Value,
                SubText = _context.Settings.FirstOrDefault(x => x.Key == "our-story-sub-text").Value
            };

            return View(ourStory);
        }

        public IActionResult Returns()
        {
            return View(_context.Settings.FirstOrDefault(x => x.Key == "returns"));
        }

        public IActionResult SizeChart()
        {
            return View(_context.Settings.ToList());
        }

        public IActionResult Accessibility()
        {
            return View(_context.Settings.FirstOrDefault(x => x.Key == "accessibility"));
        }
        public IActionResult Terms()
        {
            return View(_context.Settings.FirstOrDefault(x => x.Key == "terms"));
        }
        public IActionResult Privacy()
        {
            return View(_context.Settings.FirstOrDefault(x => x.Key == "privacy"));
        }


    }
}
