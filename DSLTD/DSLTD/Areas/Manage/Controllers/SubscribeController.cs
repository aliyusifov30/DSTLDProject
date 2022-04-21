using DSLTD.Models;
using DSLTD.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SubscribeController : Controller
    {
        DataContext _context;
        IEmailService _emailService;
        public SubscribeController(DataContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(string text)
        {

            List<Subscribe> subscribes = _context.Subscribes.ToList();

            if (text == null)
            {
                ModelState.AddModelError("", "Text not null");
                return View();
            }

            foreach (var item in subscribes)
            {
                _emailService.Send(item.Email, "Subscribe", "Note");
            }

            TempData["Success"] = "SEND";

            return View();
        }
    }
}