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
        public int Max { get; set; }

        public AgeValidatorAttribute()
        {            
            this.Legal = 12;
            this.Max = 266;
        }

        public override bool IsValid(object value)
        {
            DateTime birthValue = (DateTime)value;
            if (DateTime.Today.Year - birthValue.Year >= Legal && birthValue.Year >= 1754)
            {
                return true;
            }
            return false;
        }
    }
}
