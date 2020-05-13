using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWarships_Web.ValidationAttributes
{
    public class AgeValidatorAttribute : ValidationAttribute
    {
        public int Legal { get; set; }

        public AgeValidatorAttribute()
        {            
            this.Legal = 12;
        }

        public override bool IsValid(object value)
        {
            DateTime birthValue = (DateTime)value;
            if (DateTime.Today.Year - birthValue.Year >= Legal)
            {
                return true;
            }
            return false;
        }
    }
}
