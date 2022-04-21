using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Areas.Manage.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required]
        [StringLength(maximumLength:50 , MinimumLength = 5)]
        public string UserName{ get; set; }

        [Required]
        [StringLength(maximumLength:25 , MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
