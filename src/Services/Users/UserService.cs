using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Shared.Users;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Users;

public class UserService : IUserService
{

    public Task EditAsync(UserRequest.Edit request)
    {
        throw new NotImplementedException();
    }

    public Task<UserResponse.AllAdminsIndex> GetAllAdminsIndex(UserRequest.AllAdminUsers request)
    {
        throw new NotImplementedException();
    }

    public Task<UserResponse.AllKlantenIndex> GetAllKlanten(UserRequest.AllKlantenIndex request)
    {
        throw new NotImplementedException();
    }

    public Task<UserResponse.DetailKlant> GetDetailKlant(UserRequest.DetailKlant request)
    {
        throw new NotImplementedException();
    }
}
