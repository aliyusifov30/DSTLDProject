using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.ViewModels
{
    public class MemberResetPasswordViewModel
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 10)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        public string Token { get; set; }
        
        [StringLength(maximumLength: 25, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }

    }
}
