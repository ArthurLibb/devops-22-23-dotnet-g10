using Shared.Users;

namespace Shared.Users;

public static class UserResponse
{
    public class AllKlantenIndex
    {
        public List<KlantDto.Index> Klanten { get; set; } = new();
        public int Total { get; set; }
    }

    public class DetailKlant
    {
        public KlantDto.Detail Klant { get; set; }
    }

    public class Edit
    {
        public int Id { get; set; }
    }

    public class Create
    {
        public int Id { get; set; }
    }

    public class CurrentUser
    {
        public int UserId { get; set; }
    }

    public class AllAdminsIndex
    {
        public List<AdminUserDto.Index> Admins { get; set; } = new();
        public int Total { get; set; }
    }
}