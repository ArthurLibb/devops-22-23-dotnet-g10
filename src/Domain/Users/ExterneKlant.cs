using Ardalis.GuardClauses;
using System;

namespace Domain.Users
{
    public class ExterneKlant : Klant
    {
        
        private string _bedrijfsNaam;
        public String Bedrijfsnaam { get { return _bedrijfsNaam; } set { _bedrijfsNaam = Guard.Against.NullOrEmpty(value, nameof(_bedrijfsNaam)); } }


        public ExterneKlant(string name, string firstname, string phoneNumber, string email, string password, string bedrijfsnaam) : base(name, firstname, phoneNumber, email, password)
        {
            this.Bedrijfsnaam = bedrijfsnaam;

        }
        public ExterneKlant() : base("","","","","")
        {
        }
    }
}