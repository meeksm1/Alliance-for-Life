using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Alliance_for_Life.ViewModels

{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dueDate;

            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                "mm/dd/yyyy",CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out dueDate);

            return (isValid && dueDate > DateTime.Now);
        }
    }

    public class BirthDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dob;

            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                "mm/dd/yyyy", CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out dob);

            return (isValid && dob < DateTime.Now);
        }
    }
}