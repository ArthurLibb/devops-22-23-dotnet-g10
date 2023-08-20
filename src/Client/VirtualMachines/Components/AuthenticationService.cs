using Shared.Authentication;
using System.Net.Http.Json;

namespace Client.VirtualMachines.Components
{
    public class AuthenticationService : IAuthenticationService
    {
        private string endpoint = "api/login";
        private readonly HttpClient client;

        public AuthenticationService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<AuthenticationResponse> Login(AuthenticationRequest.Login request)
        {
            var response = await client.PostAsJsonAsync($"{endpoint}/android", request);
            return await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

        }

        public Task<AuthenticationResponse> Register(AuthenticationRequest.Register request)
        {
            throw new NotImplementedException();
        }
    }
}
