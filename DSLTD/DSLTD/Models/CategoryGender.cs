using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class CategoryGender:BaseEntity
    {
        public int GenderId { get; set; }
        public int CategoryId { get; set; }

        public Gender Gender { get; set; }
        public Category Category { get; set; }
    }
}
