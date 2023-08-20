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
using Domain.Exceptions;
using Domain.VirtualMachines.Contract;

namespace Services.VirtualMachines
{
    public class VirtualMachineService : IVirtualMachineService
    {
        private readonly HerExamenDBContext _dbContext;
        public VirtualMachineService(HerExamenDBContext dBContext) { 
            _dbContext = dBContext;
        }

        public async Task<VirtualMachineResponse.Create> CreateAsync(VirtualMachineRequest.Create request)
        {
            var project = await _dbContext.projecten.Where(p => p.Id == request.VirtualMachine.ProjectId).FirstOrDefaultAsync();
            var vm = request.VirtualMachine;
            if(project == null) { throw new EntityNotFoundException("Project", ""); }

            var newVm = new VirtualMachine
            {
                Name = vm.Name,
                OperatingSystem = vm.OperatingSystem,
                Hardware = vm.Hardware,
                BackUp = new Backup(vm.Backup.Type, vm.Backup.LastBackup)
            };
            var addedVm =  _dbContext.Virtualmachines.Add(newVm);
            await _dbContext.SaveChangesAsync();

            var contract = new VMContract(request.CustomerId, addedVm.Entity.Id, vm.Start, vm.End);
            newVm.Contract = contract;
            project.AddVirtualMachine(newVm);

            var server = await _dbContext.fysiekeServers.OrderBy(f => f.VirtualMachines.Count).FirstOrDefaultAsync();
            server.AddConnection(newVm);
            newVm.FysiekeServer = server;

            Console.WriteLine(server.Naam);

            await _dbContext.SaveChangesAsync();

            var reponse = new VirtualMachineResponse.Create { VirtualmachineId = addedVm.Entity.Id };
            return reponse;

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

        public Task<VirtualMachineResponse.GetIndex> GetIndexAsync(VirtualMachineRequest.GetIndex request)
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachineResponse.Rapport> RapporteringAsync(VirtualMachineRequest.GetDetail request)
        {
            throw new NotImplementedException();
        }

        public async Task<VirtualMachineResponse.GetIndex> GetVirtualMachinesByProjectId(int id)
        {
            var project = await _dbContext.projecten.Where(p => p.Id == id).Include(p => p.VirtualMachines).FirstOrDefaultAsync();
            if (project == null) return null;
            var listVirtualmachines = new List<VirtualMachineDto.Index>();
            project.VirtualMachines.ForEach(v => listVirtualmachines.Add(new VirtualMachineDto.Index { Id = v.Id, Mode = v.Mode, Name = v.Name}));
            var reponse = new VirtualMachineResponse.GetIndex
            {
                TotalAmount = project.VirtualMachines.Count,
                VirtualMachines = listVirtualmachines
            };
            return reponse;
        }
    }
}