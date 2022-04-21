using DSLTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.ViewModels
{
    public class MemberProfileViewModel
    {

        public List<Order> Orders { get; set; }
        public Address MainAddress { get; set; }
    }
}
