namespace Shared.Users;

public interface IUserService
{
    Task<UserResponse.AllKlantenIndex> GetAllKlanten(UserRequest.AllKlantenIndex request);
    Task<UserResponse.DetailKlant> GetDetailKlant(UserRequest.DetailKlant request);
    Task<UserResponse.AllAdminsIndex> GetAllAdminsIndex(UserRequest.AllAdminUsers request);
    Task<UserResponse.Edit> EditAsync(UserRequest.Edit request);
    Task<UserResponse.DetailAdmin> GetAdminDetails(UserRequest.Detailadmin request);
}