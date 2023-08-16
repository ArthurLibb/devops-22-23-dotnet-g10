using Ardalis.GuardClauses;
using Domain.Common;
using System;

namespace Domain.Users
{
    public class ExterneKlant : Klant
    {
        private ContactDetails _contactDetails1;
        private ContactDetails _contactDetails2;
        private string _bedrijfsNaam;
        public String Bedrijfsnaam { get { return _bedrijfsNaam; } set { _bedrijfsNaam = Guard.Against.NullOrEmpty(value, nameof(_bedrijfsNaam)); } }
        public ContactDetails? ContactPersoon { get; set; }
        public ContactDetails? TweedeContactPersoon { get; set; }

        public ExterneKlant(string name, string firstname, string phoneNumber, string email, string password, string bedrijfsnaam) : base(name, firstname, phoneNumber, email, password)
        {
            this.Bedrijfsnaam = bedrijfsnaam;
            this.ContactPersoon = _contactDetails1;
            this.TweedeContactPersoon = _contactDetails2;
        }
        public ExterneKlant() : base("","","","","")
        {
        }
    }
}