using Bogus;
using Domain.Projecten;
using Domain.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Domain.Users
{
    public static class UserFaker
    {
        
        public class Klant : Faker<Users.Klant>
        {
            private Random _random = new Random();
            private List<Users.Klant> _klanten = new();

            private static Klant _instance;
            public static Klant Instance
            {
                get
                {
                    if (_instance is null)
                    {
                        _instance = new Klant();
                    }
                    return _instance;
                }
            }



            public Klant()
            {
                if (_random.Next(1,3) % 2 == 2)
                {
                    CustomInstantiator(e => new InterneKlant(
                        e.Person.LastName,
                        e.Person.FirstName,
                        GeneratePhoneNumber(),
                        e.Person.Email,
                        PasswordGenerator.Generate(20, 2, 2, 2, 2),
                        e.PickRandom<Course>()
                    ));

                }
                else
                {
                    CustomInstantiator(e => new ExterneKlant(
                         e.Person.LastName,
                         e.Person.FirstName,
                         GeneratePhoneNumber(),
                         e.Person.Email,
                         PasswordGenerator.Generate(20, 2, 2, 2, 2),
                         e.Company.CompanyName()
                   ));
                }
            }

            public override List<Users.Klant> Generate(int count, string ruleSets = null)
            {
                List<Users.Klant> output;

                if (_klanten.Count() < count)
                {
                    output = base.Generate(count, ruleSets);
                    output.ForEach(e => _klanten.Add(e));
                }
                else
                {
                    if (count == 1)
                    {
                        output = new List<Users.Klant>() { _klanten[RandomNumberGenerator.GetInt32(0, _klanten.Count())] };
                    }
                    else
                    {
                        output = _klanten.GetRange(0, count);
                    }

                }

                return output;
            }

        }

        public class Administrators : Faker<Administrator>
        {
            private static Administrators _instance = null;

            public static Administrators Instance
            {
                get
                {
                    if (_instance is null)
                    {
                        _instance = new Administrators();
                    }
                    return _instance;
                }
            }

            public Administrators()
            {
                CustomInstantiator(e => new Administrator(
                    e.Person.LastName,
                    e.Person.FirstName,
                    GeneratePhoneNumber(),
                    e.Person.Email,
                    PasswordGenerator.Generate(20, 2, 2, 2, 2),
                    e.PickRandom<AdminRole>()
                    ));
            }

        }

        private static string GeneratePhoneNumber()
        {
            string output = "04";


            for (int i = 0; i < 8; i++)
            {
                output += (new Random().Next(0, 10)).ToString();
            }

            return output;
        }




    }
}