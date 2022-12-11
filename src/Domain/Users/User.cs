using Ardalis.GuardClauses;
using Domain.Projecten;
using System;
using System.Collections.Generic;

namespace Domain.Users
{
    public class User : Gebruiker
    {

        private string _bedrijfsNaam;
        private User _contactpersoon;
        private List<Project> _projecten;
        private Role _role;
        private Type _type;
        private string _course;
        private string _typeExtern;

        public String BedrijfsNaam { get { return _bedrijfsNaam; } set { _bedrijfsNaam = Guard.Against.NullOrEmpty(value, nameof(_bedrijfsNaam)); } }
        public User Contactpersoon { get; set; }
        public List<Project> Projecten { get; set; }
        public Role Role { get; set; }
        public Type Type { get; set; }
        public String Course { get { return _course; } set { _course = Guard.Against.NullOrEmpty(value, nameof(_course)); } }
        public String TypeExtern { get { return _typeExtern; } set { _typeExtern = Guard.Against.NullOrEmpty(value, nameof(_typeExtern)); } }





        public User(string name, string firstname, string phoneNumber, string email, string password, Role role, string bedrijfsnaam, Type type, List<Project> projecten, User user, string course) : base(name, firstname, phoneNumber, email, password)
        {
            this.Name = name;
            this.FirstName = firstname;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.Password = password;
            this._role = role;
            this._bedrijfsNaam = bedrijfsnaam;
            this._type = type;
            this._contactpersoon = user;
            this._projecten = projecten;
            this._course = course;
        }
        public User()
        {
        }
        public void addProject(Project p)
        {
            if (_projecten == null)
            {
                _projecten = new List<Project>();
            }
            _projecten.Add(p);
        }


    }
}