using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.CustomValidators;

public class PropertyValidator : ValidationAttribute
{
    public static ValidationResult IsValidEmail(string mail)
    {
        if (!mail.Contains("@")) return new ValidationResult("Geef een correct e-mail voor de contactpersoon.");
        if (mail.Split("@")[0].Length == 0) return new ValidationResult("Geef een correct e-mail voor de contactpersoon.");
        if (mail.Split("@")[1].Length == 0) return new ValidationResult("Geef een correct e-mail voor de contactpersoon.");
        if (!mail.Split("@")[1].Contains(".")) return new ValidationResult("Geef een correct e-mail voor de contactpersoon.");
        if (mail.Split("@")[1].Split(".")[1].Length < 2) return new ValidationResult("Geef een correct e-mail voor de contactpersoon.");

        return ValidationResult.Success;
    }

    public static ValidationResult IsPhoneNumberValid(string phoneNumber)
    {
        if (!phoneNumber.StartsWith("0"))
            return new ValidationResult("Contractpersoon gsm-nummer moet beginnen met 04.");

        if (phoneNumber.StartsWith("04") && phoneNumber.Length != 10)
            return new ValidationResult("Geef een correcte gsm-nummer voor contactpersoon.");

        if (!phoneNumber.StartsWith("04") && phoneNumber.Length != 9)
            return new ValidationResult("Geef een correcte gsm-nummer voor contactpersoon.");

        // Phone number validation passed
        return ValidationResult.Success;
    }
}
