using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject.Validations
{
    internal class DatetimeValidation
    {
    }
    public class Person
    {
        [CustomValidation(typeof(DateValidator), nameof(DateValidator.ValidateDate))]
        public DateTime? Birthday { get; set; }
    }

    public static class DateValidator
    {
        public static ValidationResult ValidateDate(DateTime? date, ValidationContext context)
        {
            if (!date.HasValue)
            {
                return ValidationResult.Success;
            }

            DateTime minDate = new DateTime(1910, 1, 1);
            DateTime maxDate = DateTime.UtcNow;

            if (date.Value < minDate || date.Value > maxDate)
            {
                return new ValidationResult($"Birthday must be between {minDate.ToShortDateString()} and {maxDate.ToShortDateString()}.");
            }

            return ValidationResult.Success;
        }
    }
}
