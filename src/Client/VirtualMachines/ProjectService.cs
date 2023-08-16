using Client.Extentions;
using Shared.Projects;
using System.Net;
using System.Net.Http.Json;

namespace Client.VirtualMachines
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient client;
        private string endpoint = "api/project";


        public ProjectService(HttpClient client)
        {
            this.client = client;
        }

        public Task<ProjectResponse.Create> CreateAsync(ProjectRequest.Create request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ProjectRequest.Delete request)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectResponse.Edit> EditAsync(ProjectRequest.Edit request)
        {
            throw new NotImplementedException();
        }

        public async Task<ProjectResponse.Detail> GetDetailAsync(ProjectRequest.Detail request)
        {
            var response = await client.GetFromJsonAsync<ProjectResponse.Detail>($"{endpoint}/{request.ProjectId}");
            Console.WriteLine(response);
            return response;
        }

        public async Task<ProjectResponse.All> GetIndexAsync(ProjectRequest.All request)
        {
            var response = await client.GetFromJsonAsync<ProjectResponse.All>($"{endpoint}");
            return response;
        }
    }
}
