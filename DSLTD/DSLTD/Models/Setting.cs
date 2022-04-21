using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class Setting
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:100)]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
