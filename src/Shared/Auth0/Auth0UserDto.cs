using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Auth0;


public static class Auth0UserDto
{
    public class Index
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Blocked { get; set; }
    }

    public class Details : Index
    {
        public string Roles { get; set; }
    }
}

