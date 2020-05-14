using ProjectWarships_Web.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWarships_Web.Models
{
    public class RegisterUser
    {
        [Required]
        [MaxLength(25)]
        public string Login { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name = "Confirmation Password")]
        public string Confirm { get; set; }

        [Required]
        [MaxLength(25)]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [Required]  
        [AgeValidator(ErrorMessage = "You must have 12+ years")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime BirthDate { get; set; }   
    }
}
