using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class SosialMedia:BaseEntity
    {
        [Required]
        [StringLength(maximumLength:100)]
        public string Icon { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Url { get; set; }
    }
}
