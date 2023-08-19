using Domain;

namespace Shared.Authentication
{
    public static class AuthenticationRequest
    {
        public class Login
        {
            public string Email { get; set; }
            public string Password { get; set; }

        }
        public class Register
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

        }

    }
}
