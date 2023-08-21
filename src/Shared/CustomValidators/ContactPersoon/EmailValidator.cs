using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.CustomValidators.ContactPersoon;

public class EmailValidator : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        string mail = value.ToString();

        if (!mail.Contains("@")) return new ValidationResult("Geef een correct e-mail voor de contactpersoon.");
        if (mail.Split("@")[0].Length == 0) return new ValidationResult("Geef een correct e-mail voor de contactpersoon.");
        if (mail.Split("@")[1].Length == 0) return new ValidationResult("Geef een correct e-mail voor de contactpersoon.");
        if (!mail.Split("@")[1].Contains(".")) return new ValidationResult("Geef een correct e-mail voor de contactpersoon.");
        if (mail.Split("@")[1].Split(".")[1].Length < 2) return new ValidationResult("Geef een correct e-mail voor de contactpersoon.");

        return ValidationResult.Success;
    }
}
