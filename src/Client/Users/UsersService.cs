using Client.Extentions;
using Shared.Users;
using System.Net.Http.Json;

namespace Client.Users
{
    public class UsersService : IUserService
    {

        private readonly HttpClient client;
        private const string endpoint = "api/user";
        public UsersService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<UserResponse.AllAdminsIndex> GetAllAdminsIndex(UserRequest.AllAdminUsers request)
        {
            var response = await client.GetFromJsonAsync<UserResponse.AllAdminsIndex>($"{endpoint}/admins");
            return response;
        }

        public async Task<UserResponse.AllKlantenIndex> GetAllKlanten(UserRequest.AllKlantenIndex request)
        {
            var response = await client.GetFromJsonAsync<UserResponse.AllKlantenIndex>($"{endpoint}");
            return response;
        }

        public async Task<UserResponse.DetailKlant> GetDetailKlant(UserRequest.DetailKlant request)
        {
            var response = await client.GetFromJsonAsync<UserResponse.DetailKlant>($"{endpoint}/{request.KlantId}");
            return response;
        }
        public async Task<UserResponse.Edit> EditAsync(UserRequest.Edit request)
        {
            var response = await client.PutAsJsonAsync($"{endpoint}/{request.KlantId}", request);
            return await response.Content.ReadFromJsonAsync<UserResponse.Edit>();
        }

        public async Task<UserResponse.DetailAdmin> GetAdminDetails(UserRequest.Detailadmin request)
        {
            var response = await client.GetFromJsonAsync<UserResponse.DetailAdmin>($"{endpoint}/admin/{request.AdminId}");
            return response;
        }
    }
}
