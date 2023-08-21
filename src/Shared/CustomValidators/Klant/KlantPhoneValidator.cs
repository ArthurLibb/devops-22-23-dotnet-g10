using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.CustomValidators.Klant;

public class KlantPhoneValidator : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        string phoneNumber = value.ToString();
        if(phoneNumber == null)
            return new ValidationResult("Geef een gsm-nummer bij contactpersoon.");

        if (!phoneNumber.StartsWith("0"))
            return new ValidationResult("Contractpersoon gsm-nummer moet beginnen met 04.");

        if (phoneNumber.StartsWith("04") && phoneNumber.Length != 10)
            return new ValidationResult("Geef een correcte gsm-nummer voor contactpersoon.");

        if (!phoneNumber.StartsWith("04") && phoneNumber.Length != 9)
            return new ValidationResult("Geef een correcte gsm-nummer voor contactpersoon.");

        return ValidationResult.Success;
    }
}
