using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject.Validations
{
    public class EyeglassesNameValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var upperWord = "qwertyuiopasdfghjklzxcvbnm".ToUpper();
            string validationChar = @"qwertyuiopasdfghjklzxcvbnm@$&,1234567890".ToUpper();

            if (value is string artTitle)
            {
                if (!(artTitle.Length < 10))
                {
                    return new ValidationResult("Invalid value.");
                }
                var words = artTitle.Split(' ');
                foreach (var word in words)
                {
                    if (!upperWord.Contains(word.First()))
                    {
                        return new ValidationResult("Invalid value.");
                    }
                    char[] letters = word.ToUpper().ToCharArray();
                    foreach (var letter in letters)
                    {
                        if (!validationChar.Contains(letter))
                        {
                            return new ValidationResult("Invalid value.");
                        }
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
