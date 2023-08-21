using Shared.CustomValidators;
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
        [CustomValidation(typeof(PropertyValidator), "IsPhoneNumberValid")]
        public string PhoneNumber { get; set; }
        [CustomValidation(typeof(PropertyValidator), "IsValidEmail")]
        public string? Email { get; set; }
        
    }
}
