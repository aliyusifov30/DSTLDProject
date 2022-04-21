using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.ViewModels
{
    public class MemberLoginViewModel
    {

        [Required]
        [StringLength(maximumLength: 100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 25 ,MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
