using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Users;

public static class AdminUserDto
{
    public class Index
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public AdminRole Role { get; set; }
    }

    public class Details : Index
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
