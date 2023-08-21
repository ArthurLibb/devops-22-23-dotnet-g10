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
using System.Globalization;

namespace Services.FysiekeServers
{
    public class FysiekeServerService : IFysiekeServerService
    {
        private readonly HerExamenDBContext _dbContext;

        public FysiekeServerService(HerExamenDBContext contex)
        {
            this._dbContext = contex;
        }
        public Task<FysiekeServerResponse.Launched> DeployVirtualMachine(FysiekeServerRequest.Approve request)
        {
            throw new NotImplementedException();
        }

        public async Task<FysiekeServerResponse.Available> GetAllServers()
        {
            FysiekeServerResponse.Available response = new();
            var servers = await _dbContext.fysiekeServers.Select(s =>
                    new FysiekeServerDto.Index { Id = s.Id, Name = s.Naam, ServerAddress = s.ServerAddress }).ToListAsync();
            response.Servers = servers;
            return response;
        }

        public async Task<FysiekeServerResponse.ResourcesAvailable> GetAvailableHardWareOnDate(FysiekeServerRequest.Date date)
        {
            var servers = await _dbContext.fysiekeServers.Include(f => f.VirtualMachines).ThenInclude(v => v.Contract).ToListAsync();
            var response = new FysiekeServerResponse.ResourcesAvailable();
            response.Servers = new List<FysiekeServerDto.Beschikbaarheid>();
            
            foreach (var server in servers)
            {
                var hardware = new Hardware(0, 0, 0);
                foreach (var vm in server.VirtualMachines)
                {
                    if(date.FromDate > vm.Contract.EndDate ||date.ToDate > vm.Contract.EndDate)
                    {
                        continue;
                    }
                    if (!(vm.Contract.StartDate > date.ToDate) || !(vm.Contract.EndDate < date.FromDate))
                    {

                        hardware.Memory += vm.Hardware.Memory;
                        hardware.Storage += vm.Hardware.Storage;
                        hardware.Amount_vCPU += vm.Hardware.Amount_vCPU;
                    }
                }
                response.Servers.Add(new FysiekeServerDto.Beschikbaarheid() { Id = server.Id, HardwareInUse = hardware });
            }
            return response;
        }

        public Task<FysiekeServerResponse.Available> GetAvailableServersByHardWareAsync(FysiekeServerRequest.Order request)
        {
            throw new NotImplementedException();
        }

        public async Task<FysiekeServerResponse.Details> GetDetailsAsync(FysiekeServerRequest.Detail request)
        {
            FysiekeServerResponse.Details response = new();

            var server = await _dbContext.fysiekeServers.Include(s => s.VirtualMachines).ThenInclude(v => v.Statistics)
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

        public async Task<FysiekeServerResponse.GraphValues> GetGraphValueForServer(FysiekeServerRequest.Date date)
        {
            var response = new FysiekeServerResponse.GraphValues();
            response.GraphData = new Dictionary<DateTime, Hardware> { };
            var servers = await _dbContext.fysiekeServers.Include(f => f.VirtualMachines).ThenInclude(v => v.Contract).ToListAsync();
            Console.WriteLine(date.ToDate.Subtract(date.FromDate).TotalDays);
            var changeDate = date.FromDate;
            for (int i = 0; i < date.ToDate.Subtract(date.FromDate).TotalDays; i++)
            {
                var hardwareServerInUse = new Hardware(0, 0, 0);

                foreach (var server in servers)
                {
                    var AlleData = server.VirtualMachines.Select(v => (v.Hardware, v.Contract.StartDate, v.Contract.EndDate));

                    foreach (var data in AlleData)
                    {
                        
                        if (changeDate >= data.StartDate && changeDate <= data.EndDate)
                        {
                            hardwareServerInUse.Storage += data.Hardware.Storage;
                            hardwareServerInUse.Memory += data.Hardware.Memory;
                            hardwareServerInUse.Amount_vCPU += data.Hardware.Amount_vCPU;
                        }
                    }
                }
                response.GraphData.Add(changeDate, hardwareServerInUse);
                changeDate = changeDate.AddDays(1);
            };
            return response;
        }

    }
}

