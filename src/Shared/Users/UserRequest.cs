using Domain.Users;
using Shared.Projects;
using System.Runtime.CompilerServices;

namespace Shared.Users;

public static class UserRequest
{
    public class AllKlantenIndex
    {

    }
    public class AllAdminUsers
    {
    }
    public class DetailKlant
    {
        public int KlantId { get; set; }
    }

    public class Detailadmin
    {
        public int AdminId { get; set; }
    }

    public class Edit
    {
        public int KlantId { get; set; }
        public KlantDto.Mutate Klant { get; set; }
    }
}