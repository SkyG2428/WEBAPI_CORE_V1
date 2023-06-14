using EcommApi.Models;
using System.ComponentModel.DataAnnotations;

namespace EcommApi.Validations
{
    public class UppercaseAttribute :ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            //var test = (Test)validationContext.ObjectInstance;


            if(value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var first = value.ToString()[0].ToString();
            if(first!=first.ToUpper())
            {
                return new ValidationResult("First latter should be Upper case");
            }
            return ValidationResult.Success;
        }
    }
}
