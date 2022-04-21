using DSLTD.Models;
using DSLTD.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Controllers
{
    public class CartController : Controller
    {
        DataContext _context;
        UserManager<AppUser> _userManager;
        public CartController(DataContext context , UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        
        public async Task<IActionResult> Index(int productId = 0, int sizeId = 0 , int colorId = 0)
        {
            BasketItemViewModel basketItemVM = null;

            AppUser member = null;

            if (User.Identity.IsAuthenticated)
            {
                member = _context.AppUsers.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }

            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();

            if (member == null)
            {
                ProductSize productSize = _context.ProductSizes.Include(x => x.Size).Include(x => x.Product).ThenInclude(x => x.ProductImages).FirstOrDefault(x => x.ProductId == productId && x.SizeId == sizeId);

                string basketItemsStr;

                if (HttpContext.Request.Cookies["BasketItems"] != null)
                {
                    basketItemsStr = HttpContext.Request.Cookies["BasketItems"];
                    basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);
                    basketItemVM = basketItems.Where(x => x.Count > 0).FirstOrDefault(x => x.ProductId == productId && x.SizeName == productSize.Size.Name);
                }

                if (!(productId==0||sizeId==0||colorId==0))
                {
                    if (basketItemVM == null)
                    {
                        basketItemVM = new BasketItemViewModel()
                        {
                            ProductId = productId,
                            Name = productSize.Product.Name,
                            Count = 1,
                            Discount = productSize.Product.Discount,
                            SalePrice = productSize.Product.SalePrice - (productSize.Product.SalePrice / 100) * productSize.Product.Discount,
                            PosterImage = productSize.Product.ProductImages.FirstOrDefault(x => x.PosterStatus == true).Image,
                            SizeName = productSize.Product.ProductSizes.FirstOrDefault(x => x.SizeId == sizeId).Size.Name,
                            SizeId = productSize.SizeId,
                            ColorId = colorId
                        };

                        if (productSize.Product.ProductSizes.FirstOrDefault(x => x.ProductId == productId).SizeCount > 0)
                        {
                            basketItems.Add(basketItemVM);
                        }
                    }
                    else
                    {
                        var hasItem = false;
                        var check = true;
                        foreach (var item in basketItems)
                        {
                            if (item.SizeName == productSize.Size.Name)
                            {
                                hasItem = true;
                            }
                        }

                        if (hasItem)
                        {
                            basketItemVM.Count++;
                            check = false;
                        }
                        else
                        {
                            basketItemVM = new BasketItemViewModel()
                            {
                                ProductId = productId,
                                Name = productSize.Product.Name,
                                Count = 1,
                                Discount = productSize.Product.Discount,
                                SalePrice = productSize.Product.SalePrice - (productSize.Product.SalePrice / 100) * productSize.Product.Discount,
                                PosterImage = productSize.Product.ProductImages.FirstOrDefault(x => x.PosterStatus == true).Image,
                                SizeName = productSize.Product.ProductSizes.FirstOrDefault(x => x.SizeId == sizeId).Size.Name,
                                ColorId = colorId
                            };
                        }

                        if (productSize.Product.ProductSizes.FirstOrDefault(x => x.ProductId == productId).SizeCount > 0 && check == true)
                        {
                            basketItems.Add(basketItemVM);
                        }
                    }
                }
                basketItemsStr = JsonConvert.SerializeObject(basketItems);
                HttpContext.Response.Cookies.Append("BasketItems", basketItemsStr);
            }
            else
            {
                BasketItem memberBasketItem = _context.BasketItems.Include(x => x.Product).ThenInclude(x => x.ProductSizes).ThenInclude(x => x.Size).FirstOrDefault(x => x.ProductId == productId && x.SizeId == sizeId);

                //ProductSize memberProductSize = _context.ProductSizes.Include(x => x.Product).ThenInclude(x => x.BasketItems).FirstOrDefault(x => x.Size.Id == sizeId);

                if (!(productId == 0 || sizeId == 0 || colorId == 0))
                {
                    if (memberBasketItem == null)
                    {
                        memberBasketItem = new BasketItem
                        {
                            AppUserId = member.Id,
                            ProductId = productId,
                            SizeId = sizeId,
                            Count = 1,
                            ColorId = colorId
                        };
                        _context.BasketItems.Add(memberBasketItem);
                    }
                    else
                    {
                        memberBasketItem.Count++;
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
                        ColorId = colorId,
                        ColorName = x.Color.Name,
                        PosterImage = x.Product.ProductImages.FirstOrDefault(ps => ps.PosterStatus == true && ps.ColorId == x.ColorId).Image
                    }).ToList();
                }
               
            }

            return View(basketItems);
        }

    }
}
