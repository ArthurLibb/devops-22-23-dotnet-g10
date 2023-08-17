using System.Linq;
using Persistence;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Server;
using System;
using Domain.Common;
using Shared.Servers;
using Persistence.Configuration;
using Azure;
using Shared.VirtualMachines;

namespace Services.FysiekeServers
{
    public class FysiekeServerService : IFysiekeServerService
    {
        private readonly HerExamenDBContext herExamenDBContext;

        public FysiekeServerService(HerExamenDBContext contex)
        {
            this.herExamenDBContext = contex;
        }
        public Task<FysiekeServerResponse.Launched> DeployVirtualMachine(FysiekeServerRequest.Approve request)
        {
            throw new NotImplementedException();
        }

        public async Task<FysiekeServerResponse.Available> GetAllServers()
        {
            FysiekeServerResponse.Available response = new();
            var servers = await herExamenDBContext.fysiekeServers.Select(s => 
                    new FysiekeServerDto.Index { Id = s.Id, Name = s.Naam, ServerAddress = s.ServerAddress }).ToListAsync();
            response.Servers = servers;
            return response;
        }

        public Task<FysiekeServerResponse.ResourcesAvailable> GetAvailableHardWareOnDate(FysiekeServerRequest.Date date)
        {
            throw new NotImplementedException();
        }

        public Task<FysiekeServerResponse.Available> GetAvailableServersByHardWareAsync(FysiekeServerRequest.Order request)
        {
            throw new NotImplementedException();
        }

        public async Task<FysiekeServerResponse.Details> GetDetailsAsync(FysiekeServerRequest.Detail request)
        {
            FysiekeServerResponse.Details response = new();

           var server = await herExamenDBContext.fysiekeServers.Include(s => s.VirtualMachines).ThenInclude(v => v.Statistics)
                .SingleOrDefaultAsync(s => s.Id == request.ServerId);

            List<VirtualMachineDto.Rapportage> list = new();
            foreach (var vm in server.VirtualMachines)
            {
                list.Add(new VirtualMachineDto.Rapportage { Id = vm.Id, Name = vm.Name, Statistics = vm.Statistics });
            }
            FysiekeServerDto.Detail dto = new();
            dto.VirtualMachines = list;
            dto.Name = server.Naam;
            dto.ServerAddress = server.ServerAddress;
            dto.Id = server.Id;

            response.Server = dto;

            return response;
        }

        public Task<FysiekeServerResponse.GraphValues> GetGraphValueForServer()
        {
            throw new NotImplementedException();
        }
    }
}

