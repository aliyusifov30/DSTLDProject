using DSLTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Areas.Manage.ViewModels
{
    public class DashboardViewModel
    {

        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }


        public List<Order> NewOrders { get; set; }
        public List<ProductComment> ProductComments { get; set; }

    }
}
