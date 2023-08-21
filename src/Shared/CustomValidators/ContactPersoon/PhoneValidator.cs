using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.CustomValidators.ContactPersoon;

public class PhoneValidator : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        string phoneNumber = value.ToString();

        if (!phoneNumber.StartsWith("0"))
            return new ValidationResult("Contractpersoon gsm-nummer moet beginnen met 04.");

        if (phoneNumber.StartsWith("04") && phoneNumber.Length != 10)
            return new ValidationResult("Geef een correcte gsm-nummer voor contactpersoon.");

        if (!phoneNumber.StartsWith("04") && phoneNumber.Length != 9)
            return new ValidationResult("Geef een correcte gsm-nummer voor contactpersoon.");

        if (!phoneNumber.Any(char.IsDigit))
            return new ValidationResult("Geef een correcte gsm-nummer voor contactpersoon.");

        return ValidationResult.Success;
    }
}
