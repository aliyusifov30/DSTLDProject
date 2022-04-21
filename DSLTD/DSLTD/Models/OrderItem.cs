using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class OrderItem:BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        [StringLength(maximumLength:100)]
        public string ProdName { get; set; }
        public string SizeName { get; set; }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public Color Color { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
