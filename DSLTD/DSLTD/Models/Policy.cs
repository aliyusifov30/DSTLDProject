using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class Policy:BaseEntity
    {
        [StringLength(maximumLength:50)]
        public string Name { get; set;}

        public string Text { get; set; }
    }
}
