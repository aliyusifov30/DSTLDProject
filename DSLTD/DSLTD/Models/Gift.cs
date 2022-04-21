﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class Gift:BaseEntity
    {
        [StringLength(maximumLength:20)]
        public string Code { get; set; }

        public int GiftDiscount { get; set; }

        public List<Product> Products { get; set; }
    }
}
