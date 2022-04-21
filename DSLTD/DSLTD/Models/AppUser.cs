using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }
        public int AddressLimit { get; set; }
        public List<ProductComment> ProductComments { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public List<Order> Orders { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
