﻿using Ardalis.GuardClauses;

namespace Domain.Users
{
    public class ExterneKlant : Klant
    {

        private string _bedrijfsNaam;
        public String Bedrijfsnaam { get { return _bedrijfsNaam; } set { Guard.Against.NullOrEmpty(_bedrijfsNaam, nameof(_bedrijfsNaam)); } }


        public ExterneKlant(string name, string phoneNumber, string email, string password, Gebruiker contactPersoon, Gebruiker contactPersoon2, string project, string bedrijfsnaam) : base(name, phoneNumber, email, password, contactPersoon, contactPersoon2, project)
        {
            this.Bedrijfsnaam = bedrijfsnaam;

        }



    }
}
