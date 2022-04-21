using DSLTD.Models;
using DSLTD.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Services
{
    public class LayoutService
    {
        DataContext _context;
        IHttpContextAccessor _httpContextAccessor;
        UserManager<AppUser> _userManager;
        public LayoutService(DataContext context , IHttpContextAccessor httpContextAccessor , UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Setting>> GetSettings()
        {
            return await _context.Settings.ToListAsync();
        }

        public async Task<List<SosialMedia>> GetSosialMedias()
        {
            return await _context.SosialMedias.ToListAsync();
        }

        public List<BasketItemViewModel> GetBasketItemViewModels()
        {
            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();

            AppUser member = null;

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName.Contains(_httpContextAccessor.HttpContext.User.Identity.Name) && !x.IsAdmin);
            }

            if(member == null)
            {
                var itemStr = _httpContextAccessor.HttpContext.Request.Cookies["BasketItems"];

                if(itemStr != null)
                {
                    basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(itemStr);
                }
            }
            else
            {
                List<BasketItem> basketItemsDb = _context.BasketItems
                    .Include(x => x.Product).ThenInclude(x => x.ProductImages)
                    .Include(x => x.Product).ThenInclude(x=>x.ProductSizes).ThenInclude(x=>x.Size).Include(x=>x.Color)
                    .Where(x => x.AppUserId == member.Id).ToList();

                basketItems = basketItemsDb.Select(x => new BasketItemViewModel
                {
                    Count = x.Count,
                    Discount = x.Product.Discount,
                    Name = x.Product.Name,
                    SalePrice = x.Product.SalePrice,
                    SizeName = x.Product.ProductSizes.FirstOrDefault(ps=>ps.SizeId == x.SizeId).Size.Name,
                    SizeId = x.Product.ProductSizes.FirstOrDefault(ps=>ps.SizeId == x.SizeId).Size.Id,
                    ProductId = x.ProductId,
                    ColorId = x.ColorId,
                    ColorName = x.Color.Name,
                    Price = x.Product.SalePrice,
                    PosterImage = x.Product.ProductImages.FirstOrDefault(ps=>ps.PosterStatus==true && ps.ColorId == x.ColorId).Image
                }).ToList();
            }
            return basketItems;
        }

        public async Task<List<HeaderTop>> GetHeaderTops()
        {
            return await _context.HeaderTops.Where(x=>x.IsDeleted==false).ToListAsync();
        }

        public async Task<List<Gender>> GetGenders ()
        {
            return await _context.Genders.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<List<CategoryGender>> GetCategoryGenders()
        {
            var action = _context.CategoryGenders.Include(x => x.Category).Include(x => x.Gender).Where(x => x.IsDeleted == false).ToListAsync();

            return await action;
        }
        public SearchViewModel GetSearchViewModel()
        {
            SearchViewModel searchViewModel = new SearchViewModel()
            {
                SimpleListProducts = _context.Products.Include(x=>x.ProductImages).Where(x=>x.ProductImages.Count!=0).ToList(),
                Categories = _context.Categories.ToList()
            };

            return searchViewModel;
        } 

        public async Task<List<Product>> GetWarningProduct()
        {
            return await _context.Products.Where(x => x.ProductImages.Count == 0).ToListAsync();
        }

    }
}
