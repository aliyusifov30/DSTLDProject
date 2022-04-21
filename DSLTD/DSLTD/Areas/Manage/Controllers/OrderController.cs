using DSLTD.Enums;
using DSLTD.Models;
using DSLTD.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text;
using ClosedXML.Excel;

namespace DSLTD.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class OrderController : Controller
    {

        DataContext _context;
        IEmailService _emailService;
        IWebHostEnvironment _env;
        IHttpContextAccessor _accessor;

        public OrderController(DataContext context , IEmailService emailService , IWebHostEnvironment env , IHttpContextAccessor accessor)
        {
            _context = context;
            _emailService = emailService;
            _env = env;
            _accessor = accessor;
        }
        public IActionResult Index(int page = 1 ,string word = null, OrderStatus orderStatus = OrderStatus.Pending)
        {
            var query = _context.Orders.AsQueryable();

            if (orderStatus == OrderStatus.Accepted)
            {
                query = query.Where(x => x.Status == OrderStatus.Accepted);
            }
            if (orderStatus == OrderStatus.Canceled)
            {
                query = query.Where(x => x.Status == OrderStatus.Canceled);
            }
            if (orderStatus==OrderStatus.Pending)
            {
                query = query.Where(x => x.Status == OrderStatus.Pending);
            }
            if (orderStatus == OrderStatus.Rejected)
            {
                query = query.Where(x => x.Status == OrderStatus.Rejected);
            }

            ViewBag.PageSize = 8;
            ViewBag.Word = word;
            TempData["Word"] = word;
            TempData["PageSize"] = 8;
            TempData["OrderStatus"] = orderStatus;


            return View(PagenatedList<Order>.Save(query,page,8));
        }

        public IActionResult Edit(int id)
        {
            Order order = _context.Orders.Include(x=>x.AppUser).Include(x=>x.OrderItems).ThenInclude(x=>x.Product).ThenInclude(x=>x.ProductImages).
                Include(x => x.AppUser).Include(x => x.OrderItems).ThenInclude(x => x.Product).ThenInclude(x => x.ProductSizes).ThenInclude(x=>x.Size).
                FirstOrDefault(x => x.Id == id);

            if (order == null) return RedirectToAction("notfound", "pages");

            return View(order); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Accepted(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null) return RedirectToAction("notfound", "pages");

            order.Status = (OrderStatus)2;
            _context.SaveChanges();

            TempData["Success"] = "Order is Accessed";
            
            var dsltdUrl = Url.Action("Index", "Home", Request.Scheme);


            var pathToFile = _env.WebRootPath
                            + Path.DirectorySeparatorChar.ToString()
                            + "Templates"
                            + Path.DirectorySeparatorChar.ToString()
                            + "EmailTemplate"
                            + Path.DirectorySeparatorChar.ToString()
                            + "SendOrderMessage.html";

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            string messageBody = string.Format(builder.HtmlBody, dsltdUrl, "Your order is accepted");

            _emailService.Send(order.Email, "Order Message", messageBody);

            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Canceled(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null) return RedirectToAction("notfound", "pages");

            order.Status = (OrderStatus)4;
            _context.SaveChanges();

            TempData["Success"] = "Order is Canceled";

            var dsltdUrl = Url.Action("Index", "Home", Request.Scheme);

            var pathToFile = _env.WebRootPath
                            + Path.DirectorySeparatorChar.ToString()
                            + "Templates"
                            + Path.DirectorySeparatorChar.ToString()
                            + "EmailTemplate"
                            + Path.DirectorySeparatorChar.ToString()
                            + "SendOrderMessage.html";

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            string messageBody = string.Format(builder.HtmlBody, dsltdUrl, "Your order is canceled");

            _emailService.Send(order.Email, "Order Message", messageBody);
            
            return RedirectToAction("index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Pending(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null) return RedirectToAction("notfound", "pages");

            order.Status = (OrderStatus)1;
            _context.SaveChanges();

            TempData["Success"] = "Order is Pending";

            var dsltdUrl = Url.Action("Index", "Home", Request.Scheme);


            var pathToFile = _env.WebRootPath
                            + Path.DirectorySeparatorChar.ToString()
                            + "Templates"
                            + Path.DirectorySeparatorChar.ToString()
                            + "EmailTemplate"
                            + Path.DirectorySeparatorChar.ToString()
                            + "SendOrderMessage.html";

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            string messageBody = string.Format(builder.HtmlBody, dsltdUrl , "Your order is pending");

            _emailService.Send(order.Email, "Order Message", messageBody);
           
            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Rejected(int id  , string rejectText)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null) return RedirectToAction("notfound", "pages");

            if (string.IsNullOrWhiteSpace(rejectText))
            {
                TempData["Error"] = "Your must fill the editor";
                return RedirectToAction("Edit", new { id = id });
            }

            order.Status = (OrderStatus)3;
            order.RejectText = rejectText;
            _context.SaveChanges();
                
            TempData["Success"] = "Order is Reject";

            var dsltdUrl = Url.Action("Index", "Home", Request.Scheme);

            var pathToFile = _env.WebRootPath
                            + Path.DirectorySeparatorChar.ToString()
                            + "Templates"
                            + Path.DirectorySeparatorChar.ToString()
                            + "EmailTemplate"
                            + Path.DirectorySeparatorChar.ToString()
                            + "SendOrderMessage.html";

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            string messageBody = string.Format(builder.HtmlBody, dsltdUrl , rejectText);

            _emailService.Send(order.Email, "Order Message", messageBody);

            return RedirectToAction("index");
        }
        public IActionResult ExportToExcel()
        {

            var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Orders");

            int row = 2;

            var orders = _context.Orders.ToList();

            worksheet.Cell(1, 1).Value = "Email";
            worksheet.Cell(1, 2).Value = "FullName";
            worksheet.Cell(1, 3).Value = "Address";
            worksheet.Cell(1, 4).Value = "Aparment";
            worksheet.Cell(1, 5).Value = "City";
            worksheet.Cell(1, 6).Value = "Phone";
            worksheet.Cell(1, 7).Value = "Status";
            worksheet.Cell(1, 8).Value = "TotalAmount";
            worksheet.Cell(1, 9).Value = "OrderCode";

            foreach (var item in orders)
            {
                worksheet.Cell(row, 1).Value = item.Email;
                worksheet.Cell(row, 2).Value = item.FullName;
                worksheet.Cell(row, 3).Value = item.Addresses;
                worksheet.Cell(row, 4).Value = item.Aparment;
                worksheet.Cell(row, 5).Value = item.City;
                worksheet.Cell(row, 6).Value = item.Phone;
                worksheet.Cell(row, 7).Value = item.Status;
                worksheet.Cell(row, 8).Value = item.TotalAmount;
                worksheet.Cell(row, 9).Value = item.OrderCode;
                row++;
            }

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            stream.Close();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "orders.xlsx");
        }

    }
}
