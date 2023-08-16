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
            VirtualMachineResponse.GetDetail response = new();
            VirtualMachine? vm = await _dbContext.Virtualmachines.Include(v => v.BackUp).Include(v => v.Connection)
                                                                    .Include(v => v.Contract).Include(v => v.FysiekeServer)
                                                                    .SingleOrDefaultAsync(v => v.Id == request.VirtualMachineId);

            if (vm is not null)
            {
                response.VirtualMachine = new VirtualMachineDto.Detail
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Mode = vm.Mode,

                    Hardware = vm.Hardware,
                    OperatingSystem = vm.OperatingSystem,
                    Contract = new VMContractDto.Index { Id = vm.Contract.Id, EndDate = vm.Contract.EndDate, StartDate = vm.Contract.StartDate, KlantId = vm.Contract.CustomerId },
                    BackUp = vm.BackUp,
                    FysiekeServer = new FysiekeServerDto.Index { Id = vm.FysiekeServer.Id, Name =  vm.FysiekeServer.Naam, Hardware = vm.Hardware, ServerAddress = vm.FysiekeServer.ServerAddress, HardWareAvailable = vm.Hardware },
                    VMConnection = new VMConnectionDto.Index { FQDN = vm.Connection.FQDN, Hostname = vm.Connection.Hostname.ToString(), Password = vm.Connection.Password, Username = vm.Connection.Username }
                };
            }
            else
            {
                response.VirtualMachine = new VirtualMachineDto.Detail
                {
                    Id = -1
                };
            }
            return response;
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