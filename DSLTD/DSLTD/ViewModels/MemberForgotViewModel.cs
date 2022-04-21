using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.ViewModels
{
    public class MemberForgotViewModel
    {

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 10)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
