using ProjectWarships_Web.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWarships_Web.Models
{
    public class EditUser
    {
        [Required]
        [MaxLength(25)]
        public string Login { get; set; }       

        [Required]
        [MaxLength(25)]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [Required]
        [AgeValidator(ErrorMessage = "You must have between 12 and 266 years")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime BirthDate { get; set; }
    }
}
