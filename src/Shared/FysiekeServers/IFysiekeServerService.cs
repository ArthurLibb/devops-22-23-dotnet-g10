using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Servers
{
    public interface IFysiekeServerService
    {
        public Task<FysiekeServerResponse.Available> GetAvailableServersByHardWareAsync(FysiekeServerRequest.Order request); // when customer asks VM for certain date, it will check here if any server available for that day or not. (Can stream the VMs and map it on vm contracts. Then some simple logic to check what resources available for certain day:   _fysiekeServer.ResourcesAvailableAt(localDateVariable)

        public Task<FysiekeServerResponse.Launched> DeployVirtualMachine(FysiekeServerRequest.Approve request); //when admin confirms a virtual machine this request is fired so the vm can get a vmconnection, server gets it's available hardware lowered by the hardware demanded by the VM

        public Task<FysiekeServerResponse.Details> GetDetailsAsync(FysiekeServerRequest.Detail request); // admin can request all running VMs on a particular server

        public Task<FysiekeServerResponse.Available> GetAllServers();

        public Task<FysiekeServerResponse.ResourcesAvailable> GetAvailableHardWareOnDate(FysiekeServerRequest.Date date);
        public Task<FysiekeServerResponse.GraphValues> GetGraphValueForServer();


        /*
        
        ik denk dat dit genoeg is ?
        eventueel naar volgende sprints kunnen we hier nog:
         •  methode aan toevoegen om te zien hoeveel verbruik er is van alle VM's op een bepaalde server ( GetAnalytics(Timeperiods.Daily, ... )
        Backuptype veranderen naar Timeperiod enum, zodat we deze ook hier kunnen gebruiken.
        */
    }
}