using Ardalis.GuardClauses;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.VirtualMachines.VirtualMachine
{
    public class VMConnection : Entity
    {

        private string _fqdn;
        private IPAddress _hostname;
        private string _username;
        private string _password;

        public string FQDN { get { return _fqdn; } set { _fqdn = Guard.Against.NullOrEmpty(value, nameof(_fqdn)); } }
        public IPAddress Hostname { get { return _hostname; } set { _hostname = Guard.Against.Null(value, nameof(_hostname)); } }
        public string Username { get { return _username; } set { _username = Guard.Against.NullOrEmpty(value, nameof(_username)); } }
        public string Password { get { return _password; } set { _password = Guard.Against.InvalidFormat(value, nameof(_password), "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*()_+=\\[{\\]};:<>|./?,-]).{8,}$"); } }

        /*
         
         * Password validation: 
             *  Min length: 8
             *  Max Length: ? 
             *  1 Uppercase letter
             *  1 Lowercase letter
             *  1 Digit
             *  1 Special character
         
         */


        public VMConnection(string FQDN, IPAddress hostname, string username, string password)
        {
            this.FQDN = FQDN;
            Hostname = hostname;
            Username = username;
            Password = password;
        }
        public VMConnection() { }
    }
}