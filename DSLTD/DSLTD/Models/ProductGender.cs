using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class ProductGender:BaseEntity
    {
        public int ProductId { get; set; }

        public int GenderId { get; set; }

        public Product Product { get; set; }
        public Gender Gender { get; set; }
    }
}
