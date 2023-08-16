using Client.Extentions;
using Shared.Users;
using System.Net.Http.Json;

namespace Client.Users
{
    public class UsersService : IUserService
    {

        private readonly IHttpClientFactory _IHttpClientFactory;
        private const string endpoint = "api/User";
        public UsersService(IHttpClientFactory _IHttpClientFactory)
        {
            this._IHttpClientFactory = _IHttpClientFactory;
        }


        public async Task<UserResponse.AllAdminsIndex> GetAllAdminsIndex(UserRequest.AllAdminUsers request)
        {
            var HttpClient = _IHttpClientFactory.CreateClient("AuthenticatedServerAPI");

            var queryParam = request.GetQueryString();
            var response = await HttpClient.GetFromJsonAsync<UserResponse.AllAdminsIndex>($"{endpoint}?{queryParam}");
            return response;
        }

        public async Task<UserResponse.AllKlantenIndex> GetIndexAsync(UserRequest.AllKlantenIndex request)
        {
            var HttpClient = _IHttpClientFactory.CreateClient("AuthenticatedServerAPI");

            var queryParam = request.GetQueryString();
            var response = await HttpClient.GetFromJsonAsync<UserResponse.AllKlantenIndex>($"{endpoint}?{queryParam}");
            return response;
        }

        public async Task<UserResponse.Create> CreateAsync(UserRequest.Edit request)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponse.DetailKlant> GetDetail(UserRequest.DetailKlant request)
        {
            var HttpClient = _IHttpClientFactory.CreateClient("AuthenticatedServerAPI");

            var queryParam = request.KlantId;
            var response = await HttpClient.GetFromJsonAsync<UserResponse.DetailKlant>($"{endpoint}/{queryParam}");
            return response;
        }

        public async Task<UserResponse.Edit> EditAsync(UserRequest.Edit request)
        {
            var HttpClient = _IHttpClientFactory.CreateClient("AuthenticatedServerAPI");

            var queryParam = request.GetQueryString();
            var response = await HttpClient.GetFromJsonAsync<UserResponse.Edit>($"{endpoint}?{queryParam}");
            return response;
        }

        public Task<UserResponse.AllKlantenIndex> GetAllKlanten(UserRequest.AllKlantenIndex request)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse.DetailKlant> GetDetailKlant(UserRequest.DetailKlant request)
        {
            throw new NotImplementedException();
        }

        Task IUserService.EditAsync(UserRequest.Edit request)
        {
            throw new NotImplementedException();
        }
    }
}
