using DSLTD.Models;
using DSLTD.Services;
using DSLTD.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Controllers
{
    public class AccountController : Controller
    {

        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        IEmailService _emailService;
        RoleManager<IdentityRole> _roleManager;
        IWebHostEnvironment _env;
        DataContext _context;
        public AccountController(UserManager<AppUser> userManager , DataContext context , RoleManager<IdentityRole> roleManager , SignInManager<AppUser> signInManager , IEmailService emailService , IWebHostEnvironment env)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _env = env;
            _context = context;
            
        }

        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Profile()
        {
            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            MemberProfileViewModel profileVM = new MemberProfileViewModel()
            {
                Orders = _context.Orders.Include(x=>x.OrderItems).ThenInclude(x=>x.Product).Where(x => x.AppUserId == member.Id).ToList(),
                MainAddress = _context.Addresses.FirstOrDefault(x => x.AppUserId == member.Id && x.IsMain)
            };

            return View(profileVM);
        }

        public IActionResult Login()
        {
            if (User.IsInRole("Member"))
            {
                return RedirectToAction("index", "home");
            }
            return View();
        }

        [Authorize(Roles = "Member")]
        public IActionResult WishList()
        {
            List<WishListViewModel> wishListsVM = new List<WishListViewModel>();

            wishListsVM = _context.WishListItems.Include(x=>x.Color).Select(x => new WishListViewModel()
            {
                Count = x.Count,
                Discount = x.Product.Discount,
                Name = x.Product.Name,
                SalePrice = x.Product.SalePrice,
                ProductId = x.ProductId,
                PosterImage = x.Product.ProductImages.FirstOrDefault(ps=>ps.PosterStatus==true && ps.ColorId == x.ColorId).Image,
                ColorId = x.ColorId,
                ColorName = x.Color.Name,
                Product = _context.Products.FirstOrDefault(y=>y.Id == x.ProductId)
            }).ToList();

            return View(wishListsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(MemberLoginViewModel loginVM , string returnurl , string queryString)
        {
            if (!ModelState.IsValid) return View();

            AppUser member = await _userManager.FindByEmailAsync(loginVM.Email);

            if (member == null)
            {
                ModelState.AddModelError("", "Email or Password was not correct");
                return View();
            }

            var url = returnurl + queryString;


            if(member.IsAdmin == true)
            {
                ModelState.AddModelError("", "Email or Password was not correct");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(member, loginVM.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password was not correct");
                return View();
            }

            BasketItemViewModel basketItemVM = null;
            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();

            string basketItemsStr;

            if (HttpContext.Request.Cookies["BasketItems"] != null)
            {
                basketItemsStr = HttpContext.Request.Cookies["BasketItems"];
                basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);
            }


            foreach (var item in basketItems)
            {
                BasketItem memberBasketItem = new BasketItem
                {
                    AppUserId = member.Id,
                    ProductId = item.ProductId,
                    SizeId = item.SizeId,
                    ColorId = item.ColorId,
                    Count = 1
                };
                _context.BasketItems.Add(memberBasketItem);
            }
            _context.SaveChanges();

            basketItems = _context.BasketItems.Include(x => x.Product).ThenInclude(x => x.ProductImages).Include(x => x.Product).Include(x => x.Size).Select(x => new BasketItemViewModel
            {
                Count = x.Count,
                Discount = x.Product.Discount,
                Name = x.Product.Name,
                SalePrice = x.Product.SalePrice,
                SizeName = x.Product.ProductSizes.FirstOrDefault(ps => ps.SizeId == x.SizeId).Size.Name,
                SizeId = x.Product.ProductSizes.FirstOrDefault(ps => ps.SizeId == x.SizeId).Size.Id,
                ProductId = x.ProductId,
                ColorId = x.ColorId,
                ColorName = x.Color.Name,
                Price = x.Product.SalePrice,
                PosterImage = x.Product.ProductImages.FirstOrDefault(x => x.PosterStatus == true).Image
            }).ToList();

            if (!string.IsNullOrEmpty(url))
            {
                return LocalRedirect(url);
            }

            return RedirectToAction("profile");
        }

        public IActionResult Register()
        {
            if (User.IsInRole("Member"))
            {
                return RedirectToAction("index", "home");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(MemberRegisterViewModel memberVM)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser member = await _userManager.FindByNameAsync(memberVM.UserName);
            if (member != null)
            {
                ModelState.AddModelError("UserName", "UserName is already used");
                return View();
            }

            member = await _userManager.FindByEmailAsync(memberVM.Email);
            if (member != null)
            {
                ModelState.AddModelError("Email", "Email is already used");
                return View();
            }

            member = new AppUser
            {
                UserName = memberVM.UserName,
                Email = memberVM.Email,
                FullName = memberVM.FullName,
                IsAdmin = false
            };

            await _userManager.CreateAsync(member, memberVM.Password);
            await _userManager.AddToRoleAsync(member, "Member");

            await _signInManager.PasswordSignInAsync(member, memberVM.Password, false, false);

            return RedirectToAction("profile");
        }

        public IActionResult Addresses()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("login", "account");
            }
            if (User.IsInRole("SuperAdmin"))
            {
                return RedirectToAction("login", "account");
            }

            var member = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            List<Address> addresses = _context.Addresses.Include(x=>x.AppUser).Where(x=>!x.IsDeleted && x.AppUserId == member.Id ).ToList();
            return View(addresses);
        }

        public IActionResult CreateAddress()
        {
            ViewBag.Title = "Create an Address";
            ViewBag.Button = "Create an Address";
            ViewBag.Action = "CreateAddress";
            return PartialView("_AddressesModalPartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> CreateAddress(Address address)
        {

            //if (!ModelState.IsValid) return RedirectToAction("Addresses");

            AppUser member = await _userManager.FindByNameAsync(User.Identity.Name);
            address.AppUser = member;

            if (address.AppUser.AddressLimit > 3)
            {
                TempData["Error"] = "You have only 3 address";
                return RedirectToAction("Addresses", address);
            }

            address.AppUserId = member.Id;
            address.AppUser.AddressLimit++;

            if (address.IsMain)
            {
                List<Address> addresses = _context.Addresses.ToList();

                foreach (var item in addresses)
                {
                    item.IsMain = false;
                    _context.SaveChanges();
                }
                address.IsMain = true;

            }

            _context.Addresses.Add(address);

            _context.SaveChanges();

            return RedirectToAction("addresses");
        }

        public IActionResult EditAddress(int id)
        {
            Address address = _context.Addresses.FirstOrDefault(x => x.Id == id);
            
            ViewBag.Title = "Edit an Address";
            ViewBag.Button = "Edit an Address";
            ViewBag.Action = "EditAddress";
            return PartialView("_AddressesModalPartial" , address);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAddress(Address address)
        {
            Address exist = _context.Addresses.FirstOrDefault(x => x.Id == address.Id);

            if (exist == null) return RedirectToAction("notfound", "pages");

            exist.ModifiedAt = System.DateTime.UtcNow.AddHours(4);
            exist.Phone = address.Phone;
            exist.State = address.State;
            exist.ZipCode = address.ZipCode;
            exist.Country = address.Country;
            exist.City = address.City;
            exist.Aparment = address.Aparment;
            exist.Addresses = address.Addresses;

            if (address.IsMain)
            {
                List<Address> addresses = _context.Addresses.ToList();

                foreach (var item in addresses)
                {
                    item.IsMain = false;
                    _context.SaveChanges();
                }
                exist.IsMain = true;
            }

            _context.SaveChanges();

            return RedirectToAction("Addresses");
        }

        public IActionResult DeleteAddress(int id)
        {
            Address address = _context.Addresses.Include(x=>x.AppUser).FirstOrDefault(x => x.Id == id );

            address.IsDeleted = true;
            address.AppUser.AddressLimit--;

            _context.SaveChanges();

            return RedirectToAction("addresses");
        }

        public async Task<IActionResult> Forgot(MemberForgotViewModel forgotVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser member = await _userManager.FindByEmailAsync(forgotVM.Email);

            if (member == null)
            {
                TempData["error"] = "There is no such email";
                return RedirectToAction("login");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(member);
            var url = Url.Action("ResetPassword", "Account", new { email = forgotVM.Email, token = token } , Request.Scheme);

            var dsltdUrl = Url.Action("index", "home");

            TempData["success"] = "Link Send Your Email";

            #region Email

            var pathToFile = _env.WebRootPath
                            + Path.DirectorySeparatorChar.ToString()
                            + "Templates"
                            + Path.DirectorySeparatorChar.ToString()
                            + "EmailTemplate"
                            + Path.DirectorySeparatorChar.ToString()
                            + "SendToken.html";
            
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            string messageBody = string.Format(builder.HtmlBody , url , dsltdUrl);

            _emailService.Send(forgotVM.Email, "Reset Password", messageBody);

            #endregion

            return RedirectToAction("index", "home");

            //return RedirectToAction("login");
        }

        public async Task<IActionResult> ResetPassword(MemberResetPasswordViewModel resetVM)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("login", "account");
            //}

            if(resetVM.Email == null)
            {
                return RedirectToAction("notfound", "pages");
            }

            AppUser member = await _userManager.FindByEmailAsync(resetVM.Email);

            if (member == null || !(await _userManager.VerifyUserTokenAsync(member, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetVM.Token)))
            {
                return RedirectToAction("notfound", "pages");
            }
            
            return View(resetVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(MemberResetPasswordViewModel resetVM)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("login", "account");
            }

            if (resetVM.Email == null)
            {
                return RedirectToAction("notfound", "pages");
            }

            if (string.IsNullOrWhiteSpace(resetVM.NewPassword) || resetVM.NewPassword.Length > 25 || resetVM.NewPassword.Length < 5)
                ModelState.AddModelError("NewPassword", "Password is required and must be between 5 and 26 characters");

            if (!ModelState.IsValid) return View("ResetPassword", resetVM);

            AppUser memmber = await _userManager.FindByEmailAsync(resetVM.Email);

            if (memmber == null) return RedirectToAction("notfound", "pages");

            var result = await _userManager.ResetPasswordAsync(memmber,resetVM.Token , resetVM.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return RedirectToAction("resetPassword",resetVM);
            }

            return RedirectToAction("index","home");
        }

        public IActionResult AddWishList(int id , int colorId, string returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("login", "account");
            }

            Product product = _context.Products.Include(x => x.ProductImages).Include(x=>x.WishListItems).FirstOrDefault(x => x.Id == id);

            AppUser member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);

            List<WishListViewModel> wishListsVM = new List<WishListViewModel>();

            if (member != null)
            {
                WishListItem memberWistListItem = _context.WishListItems.FirstOrDefault(x => x.AppUserId == member.Id && x.ProductId == id);

                if (memberWistListItem == null)
                {
                    memberWistListItem = new WishListItem()
                    {
                        AppUserId = member.Id,
                        ProductId = product.Id,
                        Count = 1,
                        ColorId = colorId,
                    };
                    product.LoveCount++;
                    _context.WishListItems.Add(memberWistListItem);
                }

                _context.SaveChanges();

                wishListsVM = _context.WishListItems.Include(x=>x.Color).Select(x => new WishListViewModel()
                {
                    Count = x.Count,
                    Discount = x.Product.Discount,
                    Name = x.Product.Name,
                    SalePrice = x.Product.SalePrice,
                    ProductId = x.ProductId,
                    PosterImage = x.Product.ProductImages.FirstOrDefault(ps=>ps.PosterStatus==true && ps.ColorId == x.ColorId).Image,
                    ColorId = x.Color.Id,
                    ColorName = x.Color.Name
                }).ToList();


                //return RedirectToAction("wishlist", "account", wishListsVM);
            }
            else
            {
                TempData["Error"] = "Please create account or Login";
            }

            return PartialView("_FavPartial", product);
        }

        public IActionResult RemoveWishList(int id , int colorId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("login", "account");
            }
            
            Product product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);

            if (product == null) return RedirectToAction("notfound", "pages");

            AppUser member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);

            List<WishListViewModel> wishListsItem = new List<WishListViewModel>();

            if (member != null)
            {

                WishListItem wishListItem = _context.WishListItems.
                    Include(x => x.Product).ThenInclude(x => x.ProductImages).
                    FirstOrDefault(x => x.ProductId == id);

                if (wishListItem != null) 
                {
                    product.LoveCount--;

                    _context.WishListItems.Remove(wishListItem);

                    _context.SaveChanges();

                    wishListsItem = _context.WishListItems.Include(x => x.Product).ThenInclude(x => x.ProductSizes).ThenInclude(x => x.Size)
                        .Include(x => x.Product).ThenInclude(x => x.ProductImages).Include(x => x.Color).Where(x => x.AppUserId == member.Id).Select(x => new WishListViewModel()
                        {
                            ProductId = x.ProductId,
                            Name = x.Product.Name,
                            Discount = x.Product.Discount,
                            SalePrice = x.Product.SalePrice,
                            Count = x.Count,
                            PosterImage = x.Product.ProductImages.FirstOrDefault(ps => ps.PosterStatus == true && ps.ColorId == x.ColorId).Image,
                            ColorId = x.ColorId,
                            ColorName = x.Color.Name
                        }).ToList();
                }
                
            }

            //Return Change Action
            return PartialView("_FavPartial", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(Subscribe subscribe)
        {
            var member = _context.Subscribes.FirstOrDefault(x => x.Email == subscribe.Email);

            var query = _context.Products.Include(x => x.ProductColors).ThenInclude(x => x.Color)
               .Include(x => x.ProductSizes).ThenInclude(x => x.Size)
               .Include(x => x.ProductImages).Include(x => x.Gender).Include(x => x.BasketItems).Include(x => x.WishListItems)
               .AsQueryable();

            HomeViewModel homeVM = new HomeViewModel()
            {
                Settings = _context.Settings.ToList(),
                ShopOnInstagrams = _context.ShopOnInstagrams.OrderByDescending(x => x.CreateAt).ToList(),
                Genders = _context.Genders.ToList(),
                Categories = _context.Categories.ToList(),
                Products = _context.Products.Include(x => x.ProductImages).Include(x => x.WishListItems).Where(x => x.IsNew).OrderByDescending(x => x.Rating).ToList(),
                MostLovedProduct = query.OrderByDescending(x => x.LoveCount).Take(100).ToList()
            };

            if (member != null)
            {
                TempData["error"] = "This email has been sing up";
                return RedirectToAction("index", "home", homeVM);
            }

            Subscribe newSub = new Subscribe()
            {
                Email = subscribe.Email,
                SignIpTime = System.DateTime.UtcNow.AddHours(4)
            };

            _context.Subscribes.Add(newSub);
            _context.SaveChanges();

            TempData["Success"] = "You have successfully sing up";
            return RedirectToAction("index", "home", homeVM);
        }



        #region Create
        private async Task<IActionResult> CreateRole()
            {
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Member"));

                return Ok();
            }

        private async Task<IActionResult> CreateSuperAdmin()
            {
                AppUser admin = new AppUser
                {
                    UserName = "SuperAdmin",
                    Email = "aliyusifov220@gmail.com",
                    IsAdmin = true,
                    FullName = "SuperAdmin"
                };

                await _userManager.CreateAsync(admin, "SPadmin");
                await _userManager.AddToRoleAsync(admin, "SuperAdmin");

                return Ok();
            }

        #endregion

    }
}
