using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWarships_Web.Models
{
    public class EditPassword
    {       
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name = "Password Confirmation")]
        public string Confirm { get; set; }
    }
}
