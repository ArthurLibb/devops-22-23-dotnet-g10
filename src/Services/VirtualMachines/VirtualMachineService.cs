using Shared.VirtualMachines;
using System.Linq;
using Persistence;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.VirtualMachines.VirtualMachine;
using System;
using Domain.Common;
using Domain.VirtualMachines.BackUp;
using Persistence.Configuration;
using Shared.VMContracts;
using Shared.Servers;
using Shared.VMConnection;
using Azure;

namespace Services.VirtualMachines
{
    public class VirtualMachineService : IVirtualMachineService
    {
        private readonly HerExamenDBContext _dbContext;
        public VirtualMachineService(HerExamenDBContext dBContext) { 
            _dbContext = dBContext;
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

        public async Task<VirtualMachineResponse.GetDetail> GetDetailAsync(VirtualMachineRequest.GetDetail request)
        {
            Console.WriteLine($"-----Getting VM detail in service id ={request.VirtualMachineId}");
            var response = new VirtualMachineResponse.GetDetail();
            VirtualMachine? vm = await _dbContext.Virtualmachines.Include(v => v.BackUp).Include(v => v.Connection)
                                                                    .Include(v => v.Contract).Include(v => v.FysiekeServer)
                                                                    .SingleOrDefaultAsync(v => v.Id == request.VirtualMachineId);
            if (vm is not null)
            {
                response.Id = vm.Id;
                response.Name = vm.Name;
                response.Mode = vm.Mode;

                response.Hardware = vm.Hardware;
                response.OperatingSystem = vm.OperatingSystem;
                response.Contract = new VMContractDto.Index { Id = vm.Contract.Id, EndDate = vm.Contract.EndDate, StartDate = vm.Contract.StartDate, KlantId = vm.Contract.CustomerId };
                response.BackUp = vm.BackUp;
                response.FysiekeServer = new FysiekeServerDto.Index { Id = vm.FysiekeServer.Id, Name = vm.FysiekeServer.Naam, ServerAddress = vm.FysiekeServer.ServerAddress };
                response.VMConnection = new VMConnectionDto.Index { FQDN = vm.Connection.FQDN, Hostname = vm.Connection.Hostname.ToString(), Password = vm.Connection.Password, Username = vm.Connection.Username };
            }
            else
            {
                response.Id = -1;
            }
            return response;
        }

        public Task<VirtualMachineResponse.GetIndex> GetVirtualmachineByProjectId(int projectId)
        {
            //_dbContext.Virtualmachines.Where(v => v.Pro)
                throw new NotImplementedException();
        }

        public Task<VirtualMachineResponse.GetIndex> GetIndexAsync(VirtualMachineRequest.GetIndex request)
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachineResponse.Rapport> RapporteringAsync(VirtualMachineRequest.GetDetail request)
        {
            throw new NotImplementedException();
        }
    }
}