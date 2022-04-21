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
    public class ProductController : Controller
    {
        DataContext _context;
        UserManager<AppUser> _userManager;
        public ProductController(DataContext context , UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index( int? genderId , int? categoryId , int page = 1, string? size = null , string? color = null , string? sort = null , bool lovestyle = false , string title = null)
        {
            ViewBag.GenderId = genderId;
            ViewBag.PageIndex = page;
            ViewBag.SizeName = size;
            ViewBag.ColorName = color;
            ViewBag.Sort = sort;
            ViewBag.LoveStyle = lovestyle;
            //var queryProduct = _context.Products.Include(x => x.ProductImages).Include(x => x.WishListItems).Include(x => x.ProductColors).Include(x => x.ProductSizes).ThenInclude(x => x.Size).AsQueryable();

            var queryProductSizes = _context.ProductSizes.Include(x => x.Size).Include(x => x.Product).Where(x=>!x.IsDeleted);

            //List<ProductSize> productSizes = new List<ProductSize>();            
          
            List<Product> products = new List<Product>();

            var productQuery = _context.Products.Include(x => x.ProductColors).ThenInclude(x => x.Color)
                .Include(x => x.ProductSizes).ThenInclude(x => x.Size)
                .Include(x => x.ProductImages).Include(x => x.Gender).Include(x => x.WishListItems)
                .Where(x => x.ProductSizes.Any(x => x.SizeCount != 0) && x.ProductImages.Count!=0);;

            if (lovestyle)
            {
                productQuery = productQuery.OrderByDescending(x => x.LoveCount).Take(3);
            }
            if(title != null)
            {
                ViewBag.Title = title;
            }
            #region filter
            if (size != null)
            {
                productQuery = productQuery.Where(x => x.ProductSizes.FirstOrDefault(x => x.Size.Name == size).Size.Name == size && !x.IsDeleted);
            }
            if (sort == "isNew")
            {
                productQuery = productQuery.Where(x => x.IsNew && !x.IsDeleted);
            }
            if (sort == "best-selling")
            {
                productQuery = productQuery.OrderByDescending(x => x.Rating);
            }
            if (sort == "price-ascending")
            {
                productQuery = productQuery.OrderBy(x => x.SalePrice);
            }
            if (sort == "price-descending")
            {
                productQuery = productQuery.OrderByDescending(x => x.SalePrice);
            }
            if(sort == "sort")
            {
                productQuery = productQuery.Where(x => x.Discount > 0);
            }
            if (color != null)
            {
                //productQuery = productQuery.Where(x => x.ProductColors.FirstOrDefault(x => x.Color.Name == color).Color.Name == color && !x.IsDeleted);
                //productQuery = productQuery.Where(x => x.ProductImages.Any(x => x.PosterStatus == true && x.Color.Name == color));
                productQuery = productQuery.Where(x => x.ProductImages.Any(x => x.PosterStatus == true && x.Color.Name.Contains(color)));
            }
            if (categoryId != null)
            {
                productQuery = productQuery.Where(ps=>ps.CategoryId == categoryId);
            }
            if(genderId != null)
            {
                productQuery = productQuery.Where(ps => ps.GenderId == genderId);
            }

            #endregion

            ProductViewModel productVM = new ProductViewModel()
            {
                Products = new PagenatedList<Product>(productQuery.ToList(),productQuery.Count() , page , 10),
                ProductSizes = _context.ProductSizes.Include(x => x.Size).Include(x => x.Product).ToList(),
                ProductColors = _context.ProductColors.Include(x => x.Color).Include(x => x.Product).ToList(),
                Colors = _context.Colors.ToList(),
                Sizes = _context.Sizes.ToList()
            };

            if (!string.IsNullOrWhiteSpace(size) )
            {
                ViewBag.SizeActive = size;
            }
            if (!string.IsNullOrWhiteSpace(color) )
            {
                ViewBag.ColorActive = color;
            }

            return View(productVM);
        }
        public IActionResult Detail(int id , int colorId)
        {
            var query = _context.Products.Include(x=>x.Gender).
                Include(x => x.ProductImages).Include(x => x.ProductComments).ThenInclude(x=>x.AppUser).
                Include(x => x.ProductSizes).ThenInclude(x => x.Size).
                Include(x => x.ProductColors).
                ThenInclude(x => x.Color).AsQueryable();

            Product product = query.FirstOrDefault(x => x.Id == id && x.ProductSizes.Any(x => x.SizeCount != 0));

            if (product==null)
            {
                return RedirectToAction("notfound", "pages");
            }

            List<ProductImage> productImages = _context.ProductsImages.Where(x => x.ProductId == id && x.ColorId == colorId).ToList();

            if (colorId == 0)
            {
                return RedirectToAction("notfound", "pages");
            }

            ViewBag.ColorId = colorId;
            TempData["ProductColorId"] = colorId;
            TempData["ProductColorIdForComment"] = colorId;
            TempData["ProductId"] = id;

            // Countu 0 olan varsa o size gelmesin.
            // Hamisinin count-u 0-disa onda notfound-a falan yoneltsin.

            //int productId = id; 
            //if (colorId != null)
            //{
            //    ProductColor productColor = _context.ProductColors.FirstOrDefault(x => x.ColorId == colorId);
            //    productId = productColor.ProductId;
            //}

            ProducDetailViewModel detailVM = new ProducDetailViewModel()
            {
                Product = product,
                BestRatingProduct = _context.Products.Include(x => x.ProductImages).Include(x=>x.ProductColors).ThenInclude(x=>x.Color)
                .Include(x=>x.ProductSizes).ThenInclude(x=>x.Size).OrderByDescending(x => x.Rating).Where(x=>x.ProductSizes.Any(x=>x.SizeCount!=0) && x.CategoryId == product.CategoryId).Take(4).ToList(),

                ProductComment = new ProductComment(),
                Setting = _context.Settings.ToList(),
                ProductComments = _context.ProductComments.Where(x => x.Status == true && x.ProductId == id).ToList(),
                ProductImages = productImages
            };

            return View(detailVM);
        }

        public IActionResult AddBasket(int id , int sizeId, int colorId)
        {
            if (!_context.Products.Any(x => x.Id == id))
            {
                return RedirectToAction("notfound", "pages");
            }

            List<BasketItemViewModel> basketItems = addBasketItems(id,sizeId,colorId);

            return PartialView("_BasketPartial", basketItems);
        }
        [HttpPost]
        public async Task<IActionResult> Comment(ProductComment comment)
        {
            Product product = _context.Products.Include(x=>x.ProductImages).Include(x=>x.ProductColors).
                Include(x=>x.ProductSizes).ThenInclude(x=>x.Size).Include(x=>x.ProductComments).FirstOrDefault(x => x.Id == comment.ProductId && !x.IsDeleted);

            if (product == null) return RedirectToAction("notfound", "pages");

            int productId = (int)TempData["ProductId"];
            int productColorId = (int)TempData["ProductColorIdForComment"];

            ViewBag.ColorId = productColorId;
            ProducDetailViewModel detailVM = new ProducDetailViewModel()
            {
                Setting = _context.Settings.ToList(),
                BestRatingProduct = _context.Products.Include(x => x.ProductImages).Include(x => x.ProductColors).ThenInclude(x => x.Color)
                .Include(x => x.ProductSizes).ThenInclude(x => x.Size).OrderByDescending(x => x.Rating).Where(x => x.ProductSizes.Any(x => x.SizeCount != 0)).Take(4).ToList(),
                
                Product = _context.Products.Include(x=>x.ProductImages).Include(x=>x.Gender).Include(x => x.ProductColors).ThenInclude(x=>x.Color).
                    Include(x => x.ProductSizes).ThenInclude(x => x.Size).Include(x => x.ProductComments).ThenInclude(x => x.AppUser).FirstOrDefault(x => x.Id == comment.ProductId),
                ProductComment = comment,
                ProductImages = _context.ProductsImages.Where(x => x.ProductId == productId && x.ColorId == productColorId).ToList(),
                ProductComments = _context.ProductComments.Where(x=>x.ProductId == productId).ToList(),
            };

            if (!ModelState.IsValid)
            {
                TempData["error"] = "Comment not null";
                return View("Detail", detailVM);
            }

            if(_context.Products.Any(x=>x.Id == comment.Id))
            {
                TempData["error"] = "Selected Product not founded";
                return View("Detail", detailVM);
            }

            AppUser member = null;

            if (User.Identity.IsAuthenticated)
            {
                member = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            if(member == null)
            {
                TempData["Warning"] = "You must be login or register";
                return RedirectToAction("detail", new { id = comment.ProductId, colorId = productColorId });
            }
            else
            {
                comment.AppUserId = member.Id;
                comment.UserName = member.UserName;
                comment.Status = false;
                comment.CreatedAt = DateTime.UtcNow.AddHours(4);
            }

            _context.ProductComments.Add(comment);
            _context.SaveChanges();

            TempData["Success"] = "Thanks your comment";

            return RedirectToAction("detail", new { id = comment.ProductId , colorId = productColorId});
        }   
        public IActionResult DeleteBasketItem(int id , int sizeId , int colorId , string? check = null)
            {
            if (!_context.Products.Any(x => x.Id == id))
            {
                return RedirectToAction("notfound", "pages");
            }

            List<BasketItemViewModel> basketItems = removeOneBasketItems(id, sizeId,colorId,check);

            return PartialView("_BasketPartial", basketItems);
        }
        public IActionResult GetBasket()
        {
            string basketItemsStr;
            List<BasketItemViewModel> basketItems = null;

            basketItemsStr = HttpContext.Request.Cookies["BasketItems"];
            basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);

            return PartialView("_BasketPartial", basketItems);
        }
            
        public IActionResult AddCart(int id, int sizeId , int colorId)
        {
            if (!_context.Products.Any(x => x.Id == id))
            {
                return RedirectToAction("notfound", "pages");
            }

            List<BasketItemViewModel> basketItems = addBasketItems(id, sizeId,colorId);

            return PartialView("_CartPartial", basketItems);
        }

        public IActionResult RemoveCart(int id, int sizeId,int colorId,string? check = null)
        {
            if (!_context.Products.Any(x => x.Id == id))
            {
                return RedirectToAction("notfound", "pages");
            }

            List<BasketItemViewModel> basketItems = removeOneBasketItems(id, sizeId ,colorId, check);
            
            return PartialView("_CartPartial", basketItems);
        }

        private List<BasketItemViewModel> addBasketItems(int id , int sizeId , int colorId)
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
                ProductSize productSize = _context.ProductSizes.Include(x => x.Size).Include(x => x.Product).ThenInclude(x => x.ProductImages).
                    Include(x=>x.Product).ThenInclude(x=>x.ProductColors).FirstOrDefault(x => x.ProductId == id && x.SizeId == sizeId && x.Product.ProductImages.Any(x=>x.ColorId==colorId));

                string basketItemsStr;

                if (HttpContext.Request.Cookies["BasketItems"] != null)
                {
                    basketItemsStr = HttpContext.Request.Cookies["BasketItems"];
                    basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);
                    basketItemVM = basketItems.Where(x => x.Count > 0).FirstOrDefault(x => x.ProductId == id && x.SizeName == productSize.Size.Name && x.ColorId == colorId);
                }

                if (basketItemVM == null)
                {
                    basketItemVM = new BasketItemViewModel()
                    {
                        ProductId = id,
                        Name = productSize.Product.Name,
                        Count = 1,
                        Discount = productSize.Product.Discount,
                        SalePrice = productSize.Product.SalePrice,
                        PosterImage = productSize.Product.ProductImages.FirstOrDefault(x => x.PosterStatus == true && x.ColorId == colorId).Image,
                        SizeName = productSize.Product.ProductSizes.FirstOrDefault(x => x.SizeId == sizeId).Size.Name,
                        SizeId = productSize.SizeId,
                        ColorId = colorId,
                        ColorName = _context.Colors.FirstOrDefault(x=>x.Id == colorId).Name
                    };

                    if (productSize.Product.ProductSizes.FirstOrDefault(x => x.ProductId == id).SizeCount > 0)
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
                            ProductId = id,
                            Name = productSize.Product.Name,
                            Count = 1,
                            Discount = productSize.Product.Discount,
                            SalePrice = productSize.Product.SalePrice,
                            PosterImage = productSize.Product.ProductImages.FirstOrDefault(x => x.PosterStatus == true).Image,
                            SizeName = productSize.Product.ProductSizes.FirstOrDefault(x => x.SizeId == sizeId).Size.Name,
                            ColorId = colorId,
                            ColorName = _context.Colors.FirstOrDefault(x => x.Id == colorId).Name
                        };
                    }

                    if (productSize.Product.ProductSizes.FirstOrDefault(x => x.ProductId == id).SizeCount > 0 && check == true)
                    {
                        basketItems.Add(basketItemVM);
                    }
                }

                basketItemsStr = JsonConvert.SerializeObject(basketItems);
                HttpContext.Response.Cookies.Append("BasketItems", basketItemsStr);
            }
            else
            {
                BasketItem memberBasketItem = _context.BasketItems.Include(x => x.Product).
                    ThenInclude(x => x.ProductSizes).ThenInclude(x => x.Size).
                    Include(x=>x.Product).ThenInclude(x=>x.ProductColors).ThenInclude(x=>x.Color)
                    .FirstOrDefault(x => x.ProductId == id && x.SizeId == sizeId && x.ColorId == colorId && x.AppUserId == member.Id);

                //ProductSize memberProductSize = _context.ProductSizes.Include(x => x.Product).ThenInclude(x => x.BasketItems).FirstOrDefault(x => x.Size.Id == sizeId);


                if (memberBasketItem == null)
                {
                    memberBasketItem = new BasketItem
                    {
                        AppUserId = member.Id,
                        ProductId = id,
                        SizeId = sizeId,
                        Count = 1,
                        ColorId = colorId,
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
                    PosterImage = x.Product.ProductImages.FirstOrDefault(ps=>ps.PosterStatus==true && ps.ColorId == x.ColorId).Image,
                    ColorId = x.ColorId,
                    ColorName = x.Product.ProductColors.FirstOrDefault(ps=>ps.ColorId == x.ColorId).Color.Name
                }).ToList();
            }

            return basketItems;
        }

        private List<BasketItemViewModel> removeOneBasketItems(int id , int sizeId , int colorId, string? check =null)
        {

            BasketItemViewModel basketItemVM = null;

            AppUser member = null;

            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }

            List<BasketItemViewModel> basketItemsVM = new List<BasketItemViewModel>();

            if (member == null)
            {
                string basketItemStr = HttpContext.Request.Cookies["BasketItems"];
                basketItemsVM = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemStr);
                basketItemVM = basketItemsVM.FirstOrDefault(x => x.ProductId == id && x.SizeId == sizeId);

                if (!string.IsNullOrWhiteSpace(check))
                {
                    basketItemsVM.Remove(basketItemVM);
                }

                if (basketItemVM.Count == 1)
                {
                    basketItemsVM.Remove(basketItemVM);
                }
                else
                {
                    basketItemVM.Count--;
                }

                basketItemStr = JsonConvert.SerializeObject(basketItemsVM);
                HttpContext.Response.Cookies.Append("BasketItems", basketItemStr);
            }
            else
            {
                ProductSize productSize = _context.ProductSizes.Include(x => x.Size).Include(x => x.Product).ThenInclude(x => x.ProductImages).FirstOrDefault(x => x.ProductId == id && x.SizeId == sizeId);

                //Product product = _context.Products.Include(x => x.ProductSizes).ThenInclude(x => x.Size).Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);


                BasketItem memberBasketItem = _context.BasketItems.FirstOrDefault(x => x.ProductId == id && x.SizeId == sizeId && x.ColorId == colorId);

                if (check == "true")
                {
                    _context.BasketItems.Remove(memberBasketItem);
                }
                else if (memberBasketItem.Count == 1)
                {
                    _context.BasketItems.Remove(memberBasketItem);
                }
                else
                {
                    memberBasketItem.Count--;
                }

                _context.SaveChanges();

                basketItemsVM = _context.BasketItems.
                    Include(x => x.Product).ThenInclude(x => x.ProductImages).
                    Include(x => x.Product).ThenInclude(x => x.ProductSizes).ThenInclude(x => x.Size)
                    .Where(x => x.AppUserId == member.Id).Select(x => new BasketItemViewModel
                    {
                        ProductId = x.ProductId,
                        Name = x.Product.Name,
                        SizeName = x.Product.ProductSizes.FirstOrDefault(ps => ps.ProductId == x.ProductId && ps.SizeId == x.SizeId).Size.Name,
                        SizeId = x.Product.ProductSizes.FirstOrDefault(ps=>ps.ProductId == x.ProductId && ps.SizeId == x.SizeId).Size.Id,
                        Discount = x.Product.Discount,
                        SalePrice = x.Product.SalePrice,
                        Count = x.Count,
                        ColorId = x.ColorId,
                        ColorName = x.Color.Name,
                        Price = x.Product.SalePrice,
                        PosterImage = x.Product.ProductImages.FirstOrDefault(ps => ps.PosterStatus == true && ps.ColorId == x.ColorId).Image,
                    }).ToList();
            }
            return basketItemsVM;
        }
    }
}
