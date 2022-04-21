using DSLTD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.ViewModels
{
    public class CheckoutViewModel
    {

        public List<BasketItemViewModel> basketItemsVM { get; set; } = new List<BasketItemViewModel>();
        public List<Policy> Policies { get; set; }

        [Required]
        [StringLength(maximumLength:100)]
        public string Email { get; set; }

        //[Required]
        //[StringLength(maximumLength:50 , MinimumLength = 5)]
        public string FullName { get; set; }

        [Required]
        [StringLength(maximumLength:100)]
        public string Addresses { get; set; }

        [StringLength(maximumLength:100)]
        public string Aparment { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string City { get; set;}

        [Required]
        [StringLength(maximumLength:100)]
        public string Country { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string State { get; set; }

        [Required]
        //[StringLength(maximumLength: 10)]
        public int ZipCode { get; set; }

        [Required]
        [StringLength(maximumLength:35)]
        public string Phone { get; set; }

        public decimal TotalAmount { get; set; }
        public bool IsSubscribe { get; set; }

    }
}
