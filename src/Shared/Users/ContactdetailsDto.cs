using Shared.CustomValidators;
using Shared.CustomValidators.ContactPersoon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Users;

public class ContactdetailsDto
{
    public class Index
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [PhoneValidator(ErrorMessage = "Geef een correcte gsm-nummer voor de contractpersoon.")]
        public string? PhoneNumber { get; set; }
        [EmailValidator(ErrorMessage = "Geef een correcte e-mail voor de contractpersoon.")]
        public string? Email { get; set; }

    }
}
