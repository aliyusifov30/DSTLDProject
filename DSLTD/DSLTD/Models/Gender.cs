using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class Gender:BaseEntity
    {
        [Required]
        [StringLength(maximumLength:50)]
        public string Name { get; set; }
        public List<CategoryGender> CategoryGenders { get; set; }
    }
}
