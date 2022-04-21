using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class BasketItem:BaseEntity
    {
        public int SizeId { get; set; }
        public int ProductId { get; set; }
        public string AppUserId { get; set; }
        public int ColorId { get; set; }
        public int Count { get; set; }
        public Size Size { get; set; }
        public Product Product { get; set; }
        public Color Color { get; set; }
        public AppUser AppUser { get; set;}
    }
}
