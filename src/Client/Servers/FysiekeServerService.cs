using Client.Extentions;
using Client.Infrastructure;
using Domain.Server;
using Shared.Servers;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;

namespace Client.Servers
{
    public class FysiekeServicerService : IFysiekeServerService
    {

        private readonly HttpClient _httpClient;
        private const string endpoint = "api/fysiekeserver";

        public FysiekeServicerService(HttpClient _httpClient)
        {
            this._httpClient = _httpClient;
        }
        public Task<FysiekeServerResponse.Launched> DeployVirtualMachine(FysiekeServerRequest.Approve request)
        {
            throw new NotImplementedException();
        }

        public async Task<FysiekeServerResponse.Available> GetAllServers()
        {
            var response = await _httpClient.GetFromJsonAsync<FysiekeServerResponse.Available>($"{endpoint}");
            return response;
        }

        public async Task<FysiekeServerResponse.ResourcesAvailable> GetAvailableHardWareOnDate(FysiekeServerRequest.Date date)
        {
            var response = await _httpClient.PostAsJsonAsync($"{endpoint}/available", date);
            return await response.Content.ReadFromJsonAsync<FysiekeServerResponse.ResourcesAvailable>();
        }

        public Task<FysiekeServerResponse.Available> GetAvailableServersByHardWareAsync(FysiekeServerRequest.Order request)
        {
            throw new NotImplementedException();
        }

        public async Task<FysiekeServerResponse.Details> GetDetailsAsync(FysiekeServerRequest.Detail request)
        {
            var response = await _httpClient.GetFromJsonAsync<FysiekeServerResponse.Details>($"{endpoint}/{request.ServerId}");
            return response;
        }

        public async Task<FysiekeServerResponse.GraphValues> GetGraphValueForServer(FysiekeServerRequest.Date date)
        {
            var response = await _httpClient.PostAsJsonAsync($"{endpoint}/graph", date);
            return await response.Content.ReadFromJsonAsync<FysiekeServerResponse.GraphValues>();
        }
    }
}
  
