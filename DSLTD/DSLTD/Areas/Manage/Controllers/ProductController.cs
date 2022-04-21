using DSLTD.Helper;
using DSLTD.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        DataContext _context;
        IWebHostEnvironment _env;
        public ProductController(DataContext context , IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string? select = null , string? word = null)
        {
            var query = _context.Products.Include(x => x.ProductImages).AsQueryable();

            if (select == "not-deleted")
            {
                query = query.Where(x => x.IsDeleted == false);
            }
            if (select == "is-deleted")
            {
                query = query.Where(x => x.IsDeleted == true);
            }
            if (select == "without-images")
            {
                query = query.Where(x => x.ProductImages.Count == 0);
            }
            if(word != null)
            {
                query = query.Where(x => x.Name.Contains(word));
            }

            TempData["Word"] = word;
            TempData["Page"] = page;
            ViewBag.Select = select;

            ViewBag.PageSize = 5;

            return View(PagenatedList<Product>.Save(query,page,5));
        }

        public IActionResult Create(int id)
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Size = _context.Sizes.ToList();
            ViewBag.Color = _context.Colors.ToList();
            ViewBag.Gender = _context.Genders.ToList();
            ViewBag.Gift = _context.Gifts.ToList();
            //ViewBag.Detail = _context.Details.ToList();

            Product product = _context.Products.Include(x=>x.ProductImages).FirstOrDefault(x => x.Id == id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Size = _context.Sizes.ToList();
            ViewBag.Color = _context.Colors.ToList();
            ViewBag.Gender = _context.Genders.ToList();
            ViewBag.Gift = _context.Gifts.ToList();

            if (!ModelState.IsValid) return View();

            //ViewBag.Detail = _context.Details.ToList();

            if (product.ProductSizesIds != null)
            {
                foreach (var item in product.ProductSizesIds)
                {

                    ProductSize productSize = new ProductSize()
                    {
                        SizeId = item,
                        Product = product
                    };

                    _context.ProductSizes.Add(productSize);
                    //product.ProductSizes.Add(productSize);
                }
            }
            else
            {
                ModelState.AddModelError("ProductSizesIds", "Product Sizes is required");
                return View(product);
            }

            if (product.ProductColorsIds != null)
            {

                foreach (var item in product.ProductColorsIds)
                {

                    ProductColor productColor = new ProductColor()
                    {
                        ColorId = item,
                        Product = product
                    };

                    _context.ProductColors.Add(productColor);
                    //product.ProductSizes.Add(productSize);
                }
            }
            else
            {
                ModelState.AddModelError("ProductColorsIds", "Product Colors  is required");
                return View(product);
            }

            if(product.Detail == null)
            {
                ModelState.AddModelError("Detail", "Detail is required");
                return View(product);
            }


            //if (product.ProductDetailsIds != null)
            //{
            //    foreach (var item in product.ProductDetailsIds)
            //    {
            //        ProductDetail productDetail = new ProductDetail()
            //        {
            //            DetailId = item,
            //            Product = product
            //        };
            //        _context.ProductDetails.Add(productDetail);
            //        //product.ProductSizes.Add(productSize);
            //    }
            //}

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _context.Products.Include(x => x.ProductImages).Include(x => x.ProductColors).Include(x => x.ProductSizes).FirstOrDefaultAsync(x => x.Id == id);

            if (product == null) return RedirectToAction("notfound", "pages");

            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Size = _context.Sizes.ToList();
            ViewBag.Color = _context.Colors.ToList();
            ViewBag.Gender = _context.Genders.ToList();
            ViewBag.Gift = _context.Gifts.ToList();
            //ViewBag.Detail = _context.Details.ToList();

            product.ProductSizesIds = product.ProductSizes.Select(x => x.SizeId).ToList();
            product.ProductColorsIds = product.ProductColors.Select(x => x.ColorId).ToList();
            //product.ProductDetailsIds = product.ProductDetails.Select(x => x.DetailId).ToList();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            Product exist = _context.Products.Include(x => x.ProductImages).Include(x => x.ProductSizes)
                .Include(x => x.ProductColors).FirstOrDefault(x => x.Id == product.Id);

            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Size = _context.Sizes.ToList();
            ViewBag.Color = _context.Colors.ToList();
            ViewBag.Gender = _context.Genders.ToList();
            ViewBag.Gift = _context.Gifts.ToList();
            //ViewBag.Detail = _context.Details.ToList();

            //product.ProductSizesIds = exist.ProductSizes.Select(x => x.SizeId).ToList();
            //product.ProductColorsIds = exist.ProductColors.Select(x => x.ColorId).ToList();

            //product.ProductDetailsIds = exist.ProductDetails.Select(x => x.DetailId).ToList();

            if (exist == null) return RedirectToAction("notfound", "pages");

            _setData(product, exist);


            exist.ProductSizes.RemoveAll(x => !product.ProductSizesIds.Contains(x.SizeId));
            //exist.ProductDetails.RemoveAll(x => !product.ProductDetailsIds.Contains(x.DetailId));
            exist.ProductColors.RemoveAll(x => !product.ProductColorsIds.Contains(x.ColorId));

            foreach (var item in product.ProductSizesIds.Where(x => !exist.ProductSizes.Any(ps => ps.SizeId == x)))
            {
                ProductSize productSize = new ProductSize()
                {
                    SizeId = item,
                    Product = product
                };
                exist.ProductSizes.Add(productSize);
            }

            //foreach (var item in product.ProductDetailsIds.Where(x => !exist.ProductDetails.Any(pd => pd.DetailId == x)))
            //{
            //    ProductDetail productDetail = new ProductDetail()
            //    {
            //        DetailId= item,
            //        Product = product
            //    };
            //    exist.ProductDetails.Add(productDetail);
            //}

            foreach (var item in product.ProductColorsIds.Where(x => !exist.ProductColors.Any(pd => pd.ColorId == x)))
            {
                
                foreach (var imageItem in exist.ProductImages)
                {
                    if (imageItem.Color.Id != item)
                    {
                        _context.ProductsImages.Remove(imageItem);
                    }
                }
                _context.SaveChanges();

                ProductColor productColor = new ProductColor()
                {
                    ColorId = item,
                    Product = product
                };
                exist.ProductColors.Add(productColor);
            }

            for (int i = 0; i < product.ProductSizesIds.Count(); i++)
            {
                ProductSize existProductSize = _context.ProductSizes.FirstOrDefault(x => x.SizeId == product.ProductSizesIds[i] && x.ProductId == product.Id);
                if (existProductSize == null)
                {
                    break;
                }
                if (product.SizeCountValues[i] <= -1)
                {
                    ModelState.AddModelError("", "Count must be positive numbers");
                    return View(exist);
                }
                existProductSize.SizeCount = product.SizeCountValues[i];
            }

            if (!ModelState.IsValid) return View();

            _context.SaveChanges();

            return RedirectToAction("index",new {page = TempData["Page"]});
        }

        public IActionResult Comment(int id)
        {
            Product product = _context.Products.Include(x => x.ProductComments).ThenInclude(x=>x.AppUser).Include(x=>x.ProductImages).FirstOrDefault(x => x.Id == id);

            if (product == null) return RedirectToAction("notfound", "pages");

            return View(product);
        }
        public IActionResult ImageColorCreate(int id)
        {
            Product product = _context.Products.Include(x=>x.ProductImages).Include(x=>x.ProductSizes).ThenInclude(x=>x.Size).Include(x => x.ProductColors).ThenInclude(x=>x.Color).FirstOrDefault(x => x.Id == id);

            ProductImage productImage = _context.ProductsImages.FirstOrDefault(x => x.ProductId == id&&x.PosterStatus==null);

            if (product == null)
            {
                return RedirectToAction("notfound","pages");
            }
            if (productImage != null)
            {
                return RedirectToAction("index");
            }

            ViewBag.ProductColors = product.ProductColors;
            ViewBag.ProductSizes = product.ProductSizes;
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ImageColorCreate(Product product)
        {

            ViewBag.ProductColors = _context.ProductColors.Where(x=>x.ProductId == product.Id).ToList();

            if (product == null)
            {
                ModelState.AddModelError("", "You must be add image");
                return View();
            }

            product.ProductImages = new List<ProductImage>();

            foreach (var item in product.ProductColorsIds)
            {
                Color color = _context.Colors.FirstOrDefault(x => x.Id == item); // ColorId

                if (product.PosterImagesFile != null)
                {
                    foreach (var posterItem in product.PosterImagesFile)
                    {
                        if (posterItem.Length > 10485760)
                        {
                            ModelState.AddModelError("", "PosterImage must be lower 10 mb");
                            return View();
                        }
                        if (posterItem.ContentType != "image/jpeg" && posterItem.ContentType != "image/png")
                        {
                            ModelState.AddModelError("", "PosterImage must be png,jpeg");
                            return View();
                        }
                        if (posterItem.FileName.Contains(color.Name)) 
                        {
                            ProductImage posterProductImage = new ProductImage
                            {
                                PosterStatus = true,
                                Image = FileManager.Save(_env.WebRootPath, "uploads/products", posterItem),
                                ProductId = product.Id,
                                ColorId = color.Id
                            };
                            _context.ProductsImages.Add(posterProductImage);
                        } 
                    }
                }
                else
                {
                    ModelState.AddModelError("PosterImageFile", "Poster Image is required");
                    TempData["Error"] = "Poster Image is required";
                    return View();
                };

                if (product.HoverImagesFile != null)
                {
                   
                    foreach (var hoverItem in product.HoverImagesFile)
                    {
                        if (hoverItem.Length > 10485760)
                        {
                            ModelState.AddModelError("", "HoverImage must be lower 10 mb");
                            return View();
                        }
                        if (hoverItem.ContentType != "image/jpeg" && hoverItem.ContentType != "image/png")
                        {
                            ModelState.AddModelError("", "HoverImage must be png,jpeg");
                            return View();
                        }

                        if (hoverItem.FileName.Contains(color.Name))
                        {
                            ProductImage hoverProductImage = new ProductImage
                            {
                                PosterStatus = false,
                                Image = FileManager.Save(_env.WebRootPath, "uploads/products", hoverItem),
                                ProductId = product.Id,
                                ColorId = color.Id
                            };
                            _context.ProductsImages.Add(hoverProductImage);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("HoverImageFile", "HoverImage is reiqured");
                    TempData["Error"] = "HoverImage is reiqured";
                    return View();
                }

                if (product.ProductImagesFile != null)
                {
                    foreach (var images in product.ProductImagesFile)
                    {
                        if (images.Length > 10485760)
                        {
                            ModelState.AddModelError("", "ProductImage must be lower 10 mb");
                            TempData["Error"] = "ProductImage must be lower 10 mb";
                            return View();
                        }
                        if (images.ContentType != "image/jpeg" && images.ContentType != "image/png")
                        {
                            ModelState.AddModelError("", "ProductImage must be png,jpeg");
                            TempData["Error"] = "ProductImage must be png,jpeg";
                            return View();
                        }

                        if (images.FileName.Contains(color.Name))
                        {
                            ProductImage productImage = new ProductImage()
                            {
                                PosterStatus = null,
                                ProductId = product.Id,
                                Image = FileManager.Save(_env.WebRootPath, "uploads/products", images),
                                ColorId = item
                            };
                            _context.ProductsImages.Add(productImage);
                        }
                    }
                }
            }

            _context.SaveChanges();

            return RedirectToAction("index", "product");
        }

        public IActionResult ImageColorEdit(int id)
        {
            Product product = _context.Products.Include(x=>x.ProductImages).Include(x=>x.ProductColors).ThenInclude(x=>x.Color).Include(x => x.ProductSizes).ThenInclude(x=>x.Size).FirstOrDefault(x => x.Id == id);

            if (product == null) return RedirectToAction("notfound", "pages");

            if (product.ProductImages.Count == 0)
            {
                return RedirectToAction("imagecolorcreate", new { id = product.Id });
            }

            ViewBag.ProductColors = product.ProductColors;
            ViewBag.ProductSizes = product.ProductSizes;
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ImageColorEdit(Product product)
        {
            if (product.ProductImageIds == null)
            {
                return RedirectToAction("imagecolorcreate" , new { id = product.Id});
            }

            Product exist = _context.Products.Include(x => x.ProductImages).Include(x => x.ProductSizes)
                .Include(x => x.ProductColors).FirstOrDefault(x => x.Id == product.Id);
            
            if (exist == null) return RedirectToAction("notfound", "pages");

            exist.ProductImages.RemoveAll(x => x.PosterStatus == null && !product.ProductImageIds.Contains(x.Id));

            if (product.PosterImageFile != null)
            {
                ProductImage posterImage = exist.ProductImages.FirstOrDefault(x => x.PosterStatus == true);
                if (posterImage == null) return RedirectToAction("notfound", "pages");
                _setProductImages(posterImage, product.PosterImageFile);
            }

            if (product.HoverImageFile != null)
            {
                ProductImage hoverImage = exist.ProductImages.FirstOrDefault(x => x.PosterStatus == false);
                if (hoverImage == null) return RedirectToAction("notfound", "pages");
                _setProductImages(hoverImage, product.HoverImageFile);
            }

            if (product.ProductImagesFile != null)
            {
                foreach (var item in product.ProductImagesFile)
                {
                    if (item.Length > 10485760)
                    {
                        ModelState.AddModelError("ProductImagesFile", "Image must be lower 10mb");
                        TempData["Error"] = "You must select Hover Image";
                        break;
                    }
                    if (item.ContentType != "image/jpeg" && item.ContentType != "image/png" && item.ContentType != "image/gif")
                    {
                        ModelState.AddModelError("ProductImagesFile", "Image must be png,jpeg,gif");
                        break;
                    }
                }
            }

            foreach (var colorId in product.ProductColorsIds)
            {
                if (product.ProductImagesFile != null)
                {
                    //foreach (var item in exist.ProductImages.Where(x => x.PosterStatus == null))
                    //{
                    //    FileManager.Delete(_env.WebRootPath, "uploads/products", item.Image);
                    //    _context.ProductsImages.Remove(item);
                    //}

                    Color color = _context.Colors.FirstOrDefault(x => x.Id == colorId);

                    foreach (var item in product.ProductImagesFile)
                    {
                        if (item.FileName.Contains(color.Name))
                        {
                            ProductImage productImage = new ProductImage()
                            {
                                PosterStatus = null,
                                ProductId = product.Id,
                                Image = FileManager.Save(_env.WebRootPath, "uploads/products", item),
                                ColorId = colorId
                            };
                            _context.ProductsImages.Add(productImage);
                        }
                    }
                }
            }

            _context.SaveChanges();

            return RedirectToAction("index", new { page = TempData["Page"] });
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult ProductCount(ProductSize productSizes)
        //{
        //    //foreach (var item in productSizes)
        //    //{
        //    //    ProductSize exist = _context.ProductSizes.FirstOrDefault(x => x.Id == item.Id);

        //    //    if (exist == null) return RedirectToAction("notfound","pages");
        //    //    if(item.SizeCount < -1)
        //    //    {
        //    //        ModelState.AddModelError("SizeCount", "Bigger than 0");
        //    //        return View();
        //    //    }

        //    //    exist.SizeCount = item.SizeCount;
        //    //}

        //    _context.SaveChanges();

        //    return RedirectToAction("index");

        //    // Men gedib edit-de 0 olanlarin selectini silmeliyem,
        //}

        private void _setData(Product product , Product exist)
        {
            exist.CategoryId = product.CategoryId;
            exist.GenderId = product.GenderId;
            exist.CostPrice = product.CostPrice;
            exist.ModifiedAt = DateTime.UtcNow.AddHours(4);
            exist.Desc = product.Desc;
            exist.Discount = product.Discount;
            //exist.InStock = product.InStock;
            exist.IsNew = product.IsNew;
            exist.Name = product.Name;
            exist.SalePrice = product.SalePrice;
            exist.Detail = product.Detail;
        }

        private void _setProductImages(ProductImage productImage , IFormFile productImageFile)
        {
            string filename = FileManager.Save(_env.WebRootPath, "uploads/products", productImageFile);

            FileManager.Delete(_env.WebRootPath, "uploads/products", productImage.Image);
            productImage.Image = filename;
        }

        public IActionResult Active(int id)
        {
            ProductComment comment = _context.ProductComments.FirstOrDefault(x => x.Id == id);

            if (comment == null) return RedirectToAction("notfound", "pages");

            comment.Status = true;
            _context.SaveChanges();

            return RedirectToAction("comment", new { id = comment.ProductId });
        }

        public IActionResult Reject(int id)
        {
            ProductComment comment = _context.ProductComments.FirstOrDefault(x => x.Id == id);

            if (comment == null) return RedirectToAction("notfound", "pages");

            comment.Status = false;
            _context.SaveChanges();

            return RedirectToAction("comment", new { id = comment.ProductId });
        }


    }
}
