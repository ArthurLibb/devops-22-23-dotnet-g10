using Client.Extentions;
using Shared.Projects;
using Shared.VirtualMachines;
using System.Net.Http.Json;

namespace Client.VirtualMachines
{

    public class VirtualMachineService : IVirtualMachineService
    {

        private readonly HttpClient client;
        private const string endpoint = "api/virtualmachine";
        public VirtualMachineService(HttpClient client)
        {
            this.client = client;
        }

        public Task<VirtualMachineResponse.Create> CreateAsync(VirtualMachineRequest.Create request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(VirtualMachineRequest.Delete request)
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachineResponse.Edit> EditAsync(VirtualMachineRequest.Edit request)
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachineResponse.GetIndex> GetIndexAsync(VirtualMachineRequest.GetIndex request)
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachineResponse.GetDetail> GetDetailAsync(VirtualMachineRequest.GetDetail request)
        {
            var response = client.GetFromJsonAsync<VirtualMachineResponse.GetDetail>($"{endpoint}/{request.VirtualMachineId}");
            return response;
        }

        public Task<VirtualMachineResponse.Rapport> RapporteringAsync(VirtualMachineRequest.GetDetail request)
        {
            throw new NotImplementedException();
        }
    }
}
