using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(maximumLength:100)]
        public string Name { get; set; }

        public List<CategoryGender> CategoryGenders { get; set; }

        [Required]
        [NotMapped]
        public List<int> GenderIds { get; set; } = new List<int>();
    }
}
