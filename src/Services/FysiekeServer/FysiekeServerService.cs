using System.Linq;
using Persistence;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Server;
using System;
using Domain.Common;
using Shared.Servers;

namespace Services.FysiekeServers
{
    public class FysiekeServerService : IFysiekeServerService
    {
        public Task<FysiekeServerResponse.Launched> DeployVirtualMachine(FysiekeServerRequest.Approve request)
        {
            throw new NotImplementedException();
        }

        public Task<FysiekeServerResponse.Available> GetAllServers()
        {
            throw new NotImplementedException();
        }

        public Task<FysiekeServerResponse.ResourcesAvailable> GetAvailableHardWareOnDate(FysiekeServerRequest.Date date)
        {
            throw new NotImplementedException();
        }

        public Task<FysiekeServerResponse.Available> GetAvailableServersByHardWareAsync(FysiekeServerRequest.Order request)
        {
            throw new NotImplementedException();
        }

        public Task<FysiekeServerResponse.Details> GetDetailsAsync(FysiekeServerRequest.Detail request)
        {
            throw new NotImplementedException();
        }

        public Task<FysiekeServerResponse.GraphValues> GetGraphValueForServer()
        {
            throw new NotImplementedException();
        }
    }
}

