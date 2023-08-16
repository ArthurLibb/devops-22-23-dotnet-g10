using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Utility
{
    public class PasswordGenerator
    {

        public static string Generate(int length, int amount_upper, int amount_lower, int amount_numbers, int amount_symbols)
        {
            StringBuilder output = new StringBuilder();

            string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lower = upper.ToLower();
            string numbers = "0123456789";
            string symbols = "?=.*?[#?!@$%^&*-]";

            string all = upper + lower + numbers + symbols;


            for (int i = 0; i < amount_upper; i++)
            {
                output.Append(upper[RandomNumberGenerator.GetInt32(upper.Length)]);
            }

            for (int i = 0; i < amount_lower; i++)
            {
                output.Append(lower[RandomNumberGenerator.GetInt32(lower.Length)]);
            }

            for (int i = 0; i < amount_numbers; i++)
            {
                output.Append(numbers[RandomNumberGenerator.GetInt32(numbers.Length)]);
            }
            for (int i = 0; i < amount_symbols; i++)
            {
                output.Append(symbols[RandomNumberGenerator.GetInt32(symbols.Length)]);
            }

            for (int i = 0; i < (length - amount_upper - amount_lower - amount_numbers - amount_symbols); i++)
            {
                output.Append(all[RandomNumberGenerator.GetInt32(all.Length)]);
            }



            return new string(output.ToString().ToCharArray().OrderBy(e => (new Random().Next(2) % 2) == 0).ToArray());

        }

    }
}
