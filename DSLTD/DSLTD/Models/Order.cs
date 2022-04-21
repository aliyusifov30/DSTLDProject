using DSLTD.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }

        [StringLength(maximumLength: 100)]
        public string Email { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 5)]
        public string FullName { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Addresses { get; set; }

        [StringLength(maximumLength: 100)]
        public string Aparment { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string City { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Country { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string State { get; set; }

        [Required]
        [StringLength(maximumLength: 10)]
        public int ZipCode { get; set; }

        [Required]
        [StringLength(maximumLength: 35)]
        public string Phone { get; set; }

        public string RejectText { get; set; }

        public int OrderCode { get; set; }
        public OrderStatus Status { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public AppUser AppUser { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
