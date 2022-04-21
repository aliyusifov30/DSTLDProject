using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class Product:BaseEntity
    {
        public int CategoryId { get; set; }
        public int GenderId { get; set; }
        public int GiftId { get; set; }

        [Required]
        [StringLength(maximumLength:300)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; }

        [Required]
        public bool IsNew { get; set; }
        public int Rating { get; set; }
        //public bool InStock { get; set; }
        [Required]
        [StringLength(maximumLength:500)]
        public string Desc { get; set; }

        [Required]
        public string Detail { get; set; }
        public int LoveCount { get; set; }
        public Category Category { get; set; }
        public Gender Gender { get; set; }
        public Gift Gift { get; set; }

        [NotMapped]
        public IFormFile HoverImageFile { get; set; } 
        [NotMapped]
        public IFormFile PosterImageFile { get; set; }
        [NotMapped]
        public List<IFormFile> ProductImagesFile { get; set; }

        //public List<ProductDetail> ProductDetails { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<ProductImage> ProductImages { get; set; }


        [NotMapped]
        public List<int> ProductColorsIds { get; set; }
        [NotMapped]
        public List<int> ProductSizesIds { get; set; } 
        //[NotMapped]
        //public List<int> ProductDetailsIds { get; set; } 
        [NotMapped]
        public List<int> ProductImageIds { get; set; }

        [NotMapped]
        public List<int> SizeCountValues { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public List<WishListItem> WishListItems { get; set; }
        public List<ProductComment> ProductComments { get; set; }
        public List<OrderItem> OrderItems { get; set; }


        [NotMapped]
        public List<IFormFile> HoverImagesFile { get; set; }
        [NotMapped]
        public List<IFormFile> PosterImagesFile { get; set; }

    }
}
