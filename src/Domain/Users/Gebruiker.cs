using Ardalis.GuardClauses;
using Bogus.DataSets;
using Domain.Common;
using Domain.Utility;
using System;

namespace Domain
{
    public abstract class Gebruiker : Entity
    {
        public String Name { get; set; }
        public String FirstName { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

        /*
         * Password validation: 
             *  Min length: 6
             *  Max Length: ? 
             *  1 Uppercase letter
             *  1 Lowercase letter
             *  1 Digit
         */

        public Gebruiker(string name, string firstname, string phoneNumber, string email, string password)
        {
            this.Name = name;
            this.FirstName = firstname;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.Password = password;
        }

    }
}