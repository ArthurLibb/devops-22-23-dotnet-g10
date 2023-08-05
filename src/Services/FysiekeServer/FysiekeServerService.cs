using System.Linq;
using Persistence;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Server;
using System;
using Domain.Common;
using Shared.Servers;
/*
namespace Services.FysiekeServers
{
    public class FysiekeServerService : IFysiekeServerService
    {
        public FysiekeServerService(DotNetDbContext dbContext)
        {
            _dbContext = dbContext;
            _fysiekeServers = dbContext.FysiekeServers;
        }

        private readonly DotNetDbContext _dbContext;
        private readonly DbSet<FysiekeServer> _fysiekeServers;


        private IQueryable<FysiekeServer> GetFysiekeServerById(int id) => _fysiekeServers
                .AsNoTracking()
                .Where(p => p.Id == id);

        public async Task<FysiekeServerResponse.Create> CreateAsync(FysiekeServerRequest.Create request)
        {
            FysiekeServerResponse.Create response = new();
            var fysiekeServer = _fysiekeServers.Add(new FysiekeServer(
                request.FysiekeServer.Name,
                new Hardware(request.FysiekeServer.Memory, request.FysiekeServer.Storage, request.FysiekeServer.Amount_vCPU)
                , request.FysiekeServer.ServerAddress
             )HardWareAvailable = new Hardware(request.FysiekeServer.MemoryAvailable, request.FysiekeServer.StorageAvailable, request.FysiekeServer.VCPUsAvailable));
            await _dbContext.SaveChangesAsync();
            response.FysiekeServerId = fysiekeServer.Entity.Id;
            return response;
        }

        public async Task DeleteAsync(FysiekeServerRequest.Delete request)
        {
            _fysiekeServers.RemoveIf(p => p.Id == request.FysiekeServerId);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<FysiekeServerResponse.Edit> EditAsync(FysiekeServerRequest.Edit request)
        {
            FysiekeServerResponse.Edit response = new();
            var fysiekeServer = await GetFysiekeServerById(request.FysiekeServerId).SingleOrDefaultAsync();

            if (fysiekeServer is not null)
            {
                var model = request.FysiekeServer;

                // You could use a FysiekeServer.Edit method here.
                fysiekeServer.Name = model.Name;
                fysiekeServer.ServerAddress = model.ServerAddress;
                fysiekeServer.HardWare.Memory = model.Memory;
                fysiekeServer.HardWare.Storage = model.Storage;
                fysiekeServer.HardWare.Amount_vCPU = model.Amount_vCPU;
                fysiekeServer.HardWareAvailable.Memory = model.MemoryAvailable;
                fysiekeServer.HardWareAvailable.Storage = model.StorageAvailable;
                fysiekeServer.HardWareAvailable.Amount_vCPU = model.VCPUsAvailable;



                _dbContext.Entry(fysiekeServer).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                response.FysiekeServerId = fysiekeServer.Id;
            }

            return response;
        }


        public async Task<FysiekeServerResponse.GetDetail> GetDetailAsync(FysiekeServerRequest.GetDetail request)
        {
            FysiekeServerResponse.GetDetail response = new();
            response.FysiekeServer = await GetFysiekeServerById(request.FysiekeServerId)
                .Select(x => new FysiekeServerDto.Detail
                {
                    Id = x.Id,
                    Name = x.Name,
                    ServerAddress = x.ServerAddress,
                    Hardware = x.HardWare,
                    HardWareAvailable = x.HardWareAvailable,

                })
                .SingleOrDefaultAsync();
            return response;
        }

        public async Task<FysiekeServerResponse.GetIndex> GetIndexAsync(FysiekeServerRequest.GetIndex request)
        {
            FysiekeServerResponse.GetIndex response = new();
            var query = _fysiekeServers.AsQueryable().AsNoTracking();
            response.TotalAmount = query.Count();

            query.OrderBy(x => x.Name);
            response.FysiekeServers = await query.Select(x => new FysiekeServerDto.Index
            {
                Id = x.Id,
                Name = x.Name,
                ServerAddress = x.ServerAddress,
                Hardware = x.HardWare,
                HardWareAvailable = x.HardWareAvailable
            }).ToListAsync();
            return response;
        }

        public Task<FysiekeServerResponse.Available> GetAvailableServersByHardWareAsync(FysiekeServerRequest.Order request)
        {
            throw new NotImplementedException();
        }

        public Task<FysiekeServerResponse.Launched> DeployVirtualMachine(FysiekeServerRequest.Approve request)
        {
            throw new NotImplementedException();
        }

        public Task<FysiekeServerResponse.Details> GetDetailsAsync(FysiekeServerRequest.Detail request)
        {
            throw new NotImplementedException();
        }


        public async Task<FysiekeServerResponse.ResourcesAvailable> GetAvailableHardWareOnDate(FysiekeServerRequest.Date date)
        {
            FysiekeServerResponse.ResourcesAvailable response = new();
            var query = _fysiekeServers.AsQueryable().AsNoTracking();

            query.OrderBy(x => x.Name);
            response.Servers = await query.Select(x => new FysiekeServerDto.Beschikbaarheid
            {
                Id = x.Id,
                AvailableHardware = x.HardWareAvailable
            }).ToListAsync();
            return response;
        }

        public async Task<FysiekeServerResponse.GraphValues> GetGraphValueForServer(FysiekeServerRequest.GetIndex request)
        {

            var query = _fysiekeServers.AsQueryable().AsNoTracking();

            query.OrderBy(x => x.Name);
            var _servers = await query.Select(x => new FysiekeServer
            {
                Id = x.Id,
                Name = x.Name,
                ServerAddress = x.ServerAddress,
                HardWare = x.HardWare,
                HardWareAvailable = x.HardWareAvailable
            }).ToListAsync();

            Dictionary<DateTime, Hardware> max = new();

            Hardware maxHardware = GetMaxCapacity(_servers);
            DateTime today = DateTime.Now;
            DateTime end = DateTime.Parse($"{today.AddDays(90).Day}/{today.AddDays(90).Month}/{today.AddDays(90).Year} 23:00");
            DateTime start;

            if (_servers.Count > 0)
            {
                foreach (var _server in _servers)
                {
                    if (_server.VirtualMachines.Count > 0)
                    {
                        foreach (var _vm in _server.VirtualMachines)
                        {
                            if (_vm.Contract.EndDate > today)
                            {

                                if (_vm.Contract.StartDate <= today)
                                {
                                    start = DateTime.Parse($"{today.Day}/{today.Month}/{today.Year} 00:00");
                                }
                                else
                                {
                                    start = DateTime.Parse($"{_vm.Contract.StartDate.Day}/{_vm.Contract.StartDate.Month}/{_vm.Contract.StartDate.Year} 00:00");
                                }

                                DateTime value = start;
                                for (int i = 0; i < end.Subtract(start).TotalDays; i++)
                                {
                                    if (!max.ContainsKey(value))
                                    {
                                        max.Add(value, new Hardware(maxHardware.Memory - _vm.Hardware.Memory, maxHardware.Storage - _vm.Hardware.Storage, maxHardware.Amount_vCPU - _vm.Hardware.Amount_vCPU));
                                    }
                                    else
                                    {
                                        Hardware current = max[value];
                                        max.Remove(value);
                                        max.Add(value, new Hardware(current.Memory - _vm.Hardware.Memory, current.Storage - _vm.Hardware.Storage, current.Amount_vCPU - _vm.Hardware.Amount_vCPU));

                                    }
                                    value = value.AddDays(1);
                                }
                            }
                        }
                    }
                }
            }
            foreach (var kv in max)
            {
                Console.WriteLine($"{kv.Key}:{kv.Value}");
            }

            return new FysiekeServerResponse.GraphValues() { GraphData = max };
        }

        private Hardware GetMaxCapacity(List<FysiekeServer> _servers)
        {
            Hardware maxHardware = new Hardware(0, 0, 0);

            foreach (var server in _servers)
            {
                maxHardware.Memory += server.HardWare.Memory;
                maxHardware.Storage += server.HardWare.Storage;
                maxHardware.Amount_vCPU += server.HardWare.Amount_vCPU;
            }
            return maxHardware;

        }

    }
}

*/