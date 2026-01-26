
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Domain.CustomValidationInjection
{
    public class PreventInjectionLessZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            int input = (int)value;
            if (input < 0)
            {
                return new ValidationResult("Input Less Than Zero.");
            }

            return ValidationResult.Success;
        }
    }
}
