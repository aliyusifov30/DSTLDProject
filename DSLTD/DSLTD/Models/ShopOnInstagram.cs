using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class ShopOnInstagram
    {
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }
        [StringLength(maximumLength:100)]
        public string Image { get; set; }
        public DateTime CreateAt { get; set; }
        [StringLength(maximumLength:500)]
        public string Desc { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }


    }
}
