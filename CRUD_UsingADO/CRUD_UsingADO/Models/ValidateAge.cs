using System.ComponentModel.DataAnnotations;
using System;

namespace CRUD_UsingADO.Models
{
   
        public class ValidateAge : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var employee = (Employee)validationContext.ObjectInstance;

                if (employee.DateOfBirth == null)
                    return new ValidationResult("Date of Birth is required.");

                var age = DateTime.Today.Year - DateTime.Parse(employee.DateOfBirth.ToString()).Year;

                return (age >= 18)
                    ? ValidationResult.Success
                    : new ValidationResult("Employee should be at least 18 years old.");
            }
        }
    
}
