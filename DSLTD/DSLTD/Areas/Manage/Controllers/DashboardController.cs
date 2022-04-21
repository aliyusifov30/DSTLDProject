using DSLTD.Areas.Manage.ViewModels;
using DSLTD.Enums;
using DSLTD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin , Admin")]
    public class DashboardController : Controller
    {
        DataContext _context;
        public DashboardController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            DashboardViewModel dashboardVM = new DashboardViewModel()
            {
                Products = _context.Products.ToList(),
                NewOrders = _context.Orders.Where(x=>x.OrderCode.Equals(OrderStatus.Pending) && x.CreatedAt.Day > DateTime.UtcNow.Day - 5 ).ToList(),
                Orders = _context.Orders.ToList(),
                ProductComments = _context.ProductComments.Where(x=>x.CreatedAt.Month.Equals(DateTime.UtcNow.AddHours(4).Month)).ToList()
            };

            return View(dashboardVM);
        }
    }
}
