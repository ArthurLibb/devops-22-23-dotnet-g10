using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.VirtualMachines;
using System.Threading.Tasks;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VirtualMachineController : ControllerBase
{
    private readonly IVirtualMachineService virtualMachineService;

    public VirtualMachineController(IVirtualMachineService virtualMachineService)
    {
        this.virtualMachineService = virtualMachineService;
    }


    //[Authorize(Roles = "Klant")]
    [HttpGet]
    public Task<VirtualMachineResponse.GetIndex> GetIndexAsync([FromQuery] VirtualMachineRequest.GetIndex request)
    {
        return virtualMachineService.GetIndexAsync(request);
    }

    //[Authorize(Roles = "Klant")]
    [HttpGet("{VirtualMachineId}")]
    public Task<VirtualMachineResponse.GetDetail> GetDetailAsync([FromRoute] VirtualMachineRequest.GetDetail request)
    {
        return virtualMachineService.GetDetailAsync(request);
    }

    [Authorize(Roles = "BeheerderBeheren")]
    [HttpDelete("{VirtualMachineId}")]
    public Task DeleteAsync([FromRoute] VirtualMachineRequest.Delete request)
    {
        return virtualMachineService.DeleteAsync(request);
    }

    [Authorize(Roles = "BeheerderBeheren")]
    [HttpPost]
    public Task<VirtualMachineResponse.Create> CreateAsync([FromBody] VirtualMachineRequest.Create request)
    {
        return virtualMachineService.CreateAsync(request);
    }

    [Authorize(Roles = "BeheerderBeheren")]
    [HttpPut]
    public Task<VirtualMachineResponse.Edit> EditAsync([FromBody] VirtualMachineRequest.Edit request)
    {
        return virtualMachineService.EditAsync(request);
    }
}