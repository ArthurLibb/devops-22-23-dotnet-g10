using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.VirtualMachines;
using System;
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


    [HttpGet]
    public Task<VirtualMachineResponse.GetIndex> GetIndexAsync([FromQuery] VirtualMachineRequest.GetIndex request)
    {
        return virtualMachineService.GetIndexAsync(request);
    }

    [HttpGet("{VirtualMachineId}")]
    [Authorize(Roles = "Administrator")]
    public Task<VirtualMachineResponse.GetDetail> GetDetailAsync([FromRoute] VirtualMachineRequest.GetDetail request)
    {
        return virtualMachineService.GetDetailAsync(request);
    }

    [HttpDelete("{VirtualMachineId}")]
    [Authorize(Roles = "Administrator")]
    public Task DeleteAsync([FromRoute] VirtualMachineRequest.Delete request)
    {
        return virtualMachineService.DeleteAsync(request);
    }

    [HttpGet("customer/{Id}")]
    public async Task<ActionResult<VirtualMachineResponse.GetDetail>> GetDetailsAsync([FromRoute] int id)
    {
        Console.WriteLine($"----Getting vm detais with id={id}----");
        var requestObj = new VirtualMachineRequest.GetDetail { VirtualMachineId = id };
        var response = await virtualMachineService.GetDetailAsync(requestObj);
        return Ok(response);
    }

    [HttpGet("project/{Id}")]
    public async Task<ActionResult<VirtualMachineResponse.GetIndex>> GetVirtualmachineProjectsAsync([FromRoute] int id)
    {
        Console.WriteLine($"----Getting vms of project with id={id}----");
        var response = await virtualMachineService.GetVirtualMachinesByProjectId(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<VirtualMachineResponse.Create>> CreateAsync([FromBody] VirtualMachineRequest.Create request)
    {
        Console.WriteLine($"---creating vm with these name= ${request.VirtualMachine.Name} for user={request.CustomerId}----");
        var response = await virtualMachineService.CreateAsync(request);
        return Ok(response);
    }

    [HttpPut]
    public Task<VirtualMachineResponse.Edit> EditAsync([FromBody] VirtualMachineRequest.Edit request)
    {
        return virtualMachineService.EditAsync(request);
    }
}