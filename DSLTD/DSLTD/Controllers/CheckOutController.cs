using DSLTD.Models;
using DSLTD.Services;
using DSLTD.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Controllers
{
    public class CheckOutController : Controller
    {
        DataContext _context;
        UserManager<AppUser> _userManager;
        IEmailService _emailService;
        IWebHostEnvironment _env;
        public CheckOutController(DataContext context , UserManager<AppUser> userManager , IEmailService emailService , IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
            _env = env;
        }

        public IActionResult Index(string? giftCode)
        {
            CheckoutViewModel checkoutItemVM = new CheckoutViewModel();
            checkoutItemVM.Policies = _context.Policies.ToList();
            
            AppUser member = null;
            Address address = null;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
                address = _context.Addresses.FirstOrDefault(x => x.AppUserId == member.Id);
            }

            if (member == null)
            {
                string basketItemStr = HttpContext.Request.Cookies["BasketItems"];

                if (!string.IsNullOrWhiteSpace(basketItemStr))
                {
                    checkoutItemVM.basketItemsVM = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemStr);
                    foreach (var item in checkoutItemVM.basketItemsVM)
                    {
                        Product product = _context.Products.Include(x=>x.ProductSizes).ThenInclude(x=>x.Size)
                            .Include(x=>x.ProductImages).Include(x=>x.Gift).FirstOrDefault(x => x.Id == item.ProductId);
                        
                        if (product != null)
                        {
                            item.Name = product.Name + " in " + item.ColorName;
                            item.Discount = product.Discount;   
                            item.PosterImage = product.ProductImages.FirstOrDefault(x => x.PosterStatus == true && x.ColorId == item.ColorId)?.Image;


                            //item.SizeName = product.ProductSizes.FirstOrDefault(x => x.Size.Name ).Size.Name;
                            if(giftCode == product.Gift.Code)
                            {
                                Gift gift = product.Gift;
                                TempData["GiftCode"] = gift.GiftDiscount;
                                item.Price = product.SalePrice;
                                item.SalePrice = (product.SalePrice - (product.SalePrice / 100)  * (gift.GiftDiscount + product.Discount)) * item.Count;
                            }
                            else
                            {
                                item.SalePrice = ((product.SalePrice - (product.SalePrice / 100) * item.Discount));
                                item.Price = product.SalePrice;
                            }
                        }
                        checkoutItemVM.TotalAmount += item.SalePrice;
                    }
                }
            }
            else
            {
                checkoutItemVM.Email = member.Email;
                checkoutItemVM.FullName = member.FullName;

                checkoutItemVM.Addresses = address.Addresses;
                checkoutItemVM.Aparment= address.Aparment;
                checkoutItemVM.City= address.City;
                checkoutItemVM.Country= address.Country;
                checkoutItemVM.ZipCode= address.ZipCode;
                checkoutItemVM.Phone= address.Phone;
                checkoutItemVM.State= address.State;
                
                Gift gift = _context.Gifts.FirstOrDefault(x => x.Code == giftCode);


                if (gift!=null)
                {
                    
                    TempData["GiftCode"] = gift.GiftDiscount;

                    checkoutItemVM.basketItemsVM = _context.BasketItems.Include(x => x.Product).Include(x=>x.Color).Where(x => x.AppUserId == member.Id).Select(x => new BasketItemViewModel()
                    {
                        ProductId = x.ProductId,
                        Name = x.Product.Name,
                        Count = x.Count,
                        Discount = x.Product.Discount,
                        PosterImage = x.Product.ProductImages.FirstOrDefault(ps=>ps.PosterStatus==true && ps.ColorId == x.ColorId).Image,
                        ColorId = x.ColorId,
                        ColorName = x.Color.Name,
                        SalePrice = (x.Product.SalePrice  - (x.Product.SalePrice / 100) * (gift.GiftDiscount + x.Product.Discount)),
                        Price = x.Product.SalePrice,
                        SizeName = x.Product.ProductSizes.FirstOrDefault(ps => ps.ProductId == x.Product.Id).Size.Name
                    }).ToList();
                }
                else
                {
                    checkoutItemVM.basketItemsVM = _context.BasketItems.Include(x => x.Product).Include(x => x.Color).Where(x => x.AppUserId == member.Id).Select(x => new BasketItemViewModel()
                    {
                        ProductId = x.ProductId,
                        Name = x.Product.Name,
                        Count = x.Count,
                        Discount = x.Product.Discount,
                        ColorId = x.ColorId,
                        ColorName = x.Color.Name,
                        PosterImage = x.Product.ProductImages.FirstOrDefault(ps=>ps.PosterStatus==true && ps.ColorId==x.ColorId).Image,
                        SalePrice = (x.Product.SalePrice - ((x.Product.SalePrice / 100)*x.Product.Discount)) * x.Count,
                        SizeName = x.Product.ProductSizes.FirstOrDefault(ps => ps.SizeId == x.SizeId).Size.Name,
                        Price = x.Product.SalePrice,
                        SizeId = x.Product.ProductSizes.FirstOrDefault(ps => ps.SizeId == x.SizeId).Size.Id,
                    }).ToList();
                }

                ViewBag.GiftCode = giftCode;

                foreach (var item in checkoutItemVM.basketItemsVM)
                {
                    checkoutItemVM.TotalAmount += item.SalePrice;
                }
            }

            if (!string.IsNullOrWhiteSpace(giftCode))
            {
                Gift exist = _context.Gifts.FirstOrDefault(x => x.Code == giftCode);

                if (exist == null)
                {
                    ModelState.AddModelError("", "Enter a valid discount code or gift card");
                    return View("index", checkoutItemVM);
                }
            }

            return View(checkoutItemVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CheckoutViewModel checkoutVM)
        {

            checkoutVM.basketItemsVM = _getBasketItem();

            int giftCode = (int)TempData["GiftCode"];

            if (!ModelState.IsValid) return View(checkoutVM);

            AppUser member = null;

            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }

            Order order = new Order()
            {
                Addresses = checkoutVM.Addresses,
                Aparment = checkoutVM.Aparment,
                City = checkoutVM.City,
                Country = checkoutVM.Country,
                Phone = checkoutVM.Phone,
                State = checkoutVM.State,
                ZipCode = checkoutVM.ZipCode,
                CreatedAt = DateTime.UtcNow.AddHours(4),
                OrderItems = new List<OrderItem>(),
                FullName = checkoutVM.FullName,
                Email = checkoutVM.Email,
                Status = Enums.OrderStatus.Pending,
            };

            string basketItemsStr;

            List<BasketItemViewModel> basketItemsVM = new List<BasketItemViewModel>();

            if (member != null)
            {
                order.AppUserId = member.Id;
                order.FullName = member.FullName;
                basketItemsVM = _context.BasketItems.Where(x => x.AppUserId == member.Id).Select(x => new BasketItemViewModel
                {
                    ProductId = x.Product.Id,
                    Count = x.Count,
                    ColorId = x.ColorId,
                    ColorName = x.Color.Name,
                    Discount = x.Product.Discount,
                    Name = x.Product.Name,
                    //ProdName = x.Product.Name,
                    //SizeName = pro.uct.ProductSizes.FirstOrDefault(x => x.Size.Name == x.Size.Name).Size.Name,
                    //ColorId = item.ColorId,
                    //ColorName = item.ColorName,
                    //Count = item.Count,
                    //CostPrice = item.SalePrice,
                    //Discount = item.Discount,
                }).ToList();
            }
            else
            {
                basketItemsStr = HttpContext.Request.Cookies["BasketItems"];

                if (basketItemsStr != null)
                {
                    basketItemsVM = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);
                }
            }
                
            foreach (var item in basketItemsVM)
            {
                Product product = _context.Products.Include(x => x.ProductImages).Include(x=>x.ProductSizes).ThenInclude(x=>x.Size).FirstOrDefault(x => x.Id == item.ProductId);

                if (product == null)
                {
                    ModelState.AddModelError("", "Not found");
                    return View(checkoutVM);
                }

                _addOrderItem(ref order, product, item , giftCode);
            }

            if (order.OrderItems.Count == 0)
            {
                ModelState.AddModelError("", "NotFound");
                return View(checkoutVM);
            }

            var lastOrder = _context.Orders.OrderByDescending(x => x.Id).FirstOrDefault();
            order.OrderCode = lastOrder == null ? 1001 : lastOrder.OrderCode + 1;

            _context.Orders.Add(order);
            _context.SaveChanges();

            if (checkoutVM.IsSubscribe)
            {
                Subscribe subscribe = new Subscribe();
                subscribe.Email = checkoutVM.Email;
                subscribe.SignIpTime = DateTime.UtcNow.AddHours(4);
                _context.Subscribes.Add(subscribe);
            }

            if(member != null)
            {
                _context.BasketItems.RemoveRange(_context.BasketItems.Where(x => x.AppUserId == member.Id));
                _context.SaveChanges();
            }
            else
            {
                //Response.Cookies.Delete("BasketItem");
            }

            basketItemsVM.Clear();

            basketItemsStr = HttpContext.Request.Cookies["BasketItems"];
            
            basketItemsStr = JsonConvert.SerializeObject(basketItemsVM);

            HttpContext.Response.Cookies.Append("BasketItems", basketItemsStr);

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

            string messageBody = string.Format(builder.HtmlBody, dsltdUrl, "Your order is pending");

            _emailService.Send(order.Email, "Order Message", messageBody);

            TempData["Success"] = "Thanks for your order";

            return RedirectToAction("index", "home");
        }
        
        public IActionResult Shipping()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }

        private void _addOrderItem(ref Order order , Product product , BasketItemViewModel item , int giftCode)
        {
            ++product.Rating;

            if (giftCode != 0)
            {
                product.Discount += giftCode;
            }

            OrderItem orderItem = new OrderItem()
            {
                ProductId = product.Id,
                ProdName = product.Name,
                SalePrice = (product.SalePrice - (product.SalePrice / 100) * product.Discount),
                SizeName = product.ProductSizes.FirstOrDefault(x => x.Size.Name == x.Size.Name).Size.Name,
                ColorId = item.ColorId,
                ColorName = item.ColorName,
                Count = item.Count,
                CostPrice = product.CostPrice,
                Discount = item.Discount,
            };
            order.OrderItems.Add(orderItem);
            order.TotalAmount += (orderItem.SalePrice);
        }
        private List<BasketItemViewModel> _getBasketItem()
        {
            List<BasketItemViewModel> basketItemsVM = new List<BasketItemViewModel>();

            AppUser member = null;

            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }

            if(member == null)
            {
                string basketItemsStr = HttpContext.Request.Cookies["BasketItems"];

                if (!string.IsNullOrWhiteSpace(basketItemsStr))
                {

                    basketItemsVM = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);

                    foreach (var item in basketItemsVM)
                    {

                        Product product = _context.Products.Include(x=>x.ProductImages).
                            Include(x=>x.ProductSizes).ThenInclude(x=>x.Size).FirstOrDefault(x => x.Id == item.ProductId);


                        if (product!=null)
                        {
                            item.Name = product.Name;
                            item.SizeName = product.ProductSizes.FirstOrDefault(x => x.Size.Name == item.SizeName).Size.Name;
                            item.SalePrice = product.SalePrice - (product.SalePrice / 100) * product.Discount;
                            item.PosterImage = product.ProductImages.FirstOrDefault(x => x.PosterStatus == true)?.Image;
                        }

                    }
                }
            }
            else
            {
                basketItemsVM = _context.BasketItems.Include(x => x.Product).ThenInclude(x => x.ProductImages).
                    Include(x => x.Product).ThenInclude(x => x.ProductSizes).ThenInclude(x => x.Size).Select(x => new BasketItemViewModel()
                    {
                        Count = x.Count,
                        Discount = x.Product.Discount,
                        Name = x.Product.Name,
                        PosterImage = x.Product.ProductImages.FirstOrDefault(x=>x.PosterStatus==true).Image,
                        ProductId = x.Product.Id,
                        SalePrice = x.Product.SalePrice - (x.Product.SalePrice / 100) * x.Product.Discount,
                        SizeName = x.Product.ProductSizes.FirstOrDefault(x=>x.Size.Name == x.Size.Name).Size.Name
                    }).ToList();
            }

            return basketItemsVM;
        }


        public IActionResult getPolicyData(int id)
        {
            Policy policy = _context.Policies.FirstOrDefault(x => x.Id == id);
            return PartialView("_PolicyPartial", policy);
        }

    }
}
