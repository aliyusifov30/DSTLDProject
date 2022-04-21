using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Models
{
    public class ProductComment
    {

        public int Id { get; set; }
        public string AppUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ProductId { get; set; }

        [Required]
        [StringLength(maximumLength:500)]
        public string Text { get; set; }
        public bool Status { get; set; }

        [StringLength(maximumLength:50)]
        public string UserName { get; set; }


        public Product Product { get; set; }
        public AppUser AppUser { get; set; }

    }
}
