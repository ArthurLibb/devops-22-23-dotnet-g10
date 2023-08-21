using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Servers;
using System;
using System.Threading.Tasks;

namespace Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class FysiekeServerController : ControllerBase
{
    private readonly IFysiekeServerService fysiekeServerService;
    public FysiekeServerController(IFysiekeServerService fysiekeServerService)
    {
        this.fysiekeServerService = fysiekeServerService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public async Task<FysiekeServerResponse.Available> GetAllServers()
    {
        Console.WriteLine("-----Getting all servers-----");
        var response = await fysiekeServerService.GetAllServers();
        return response;
    }
    [HttpGet("{ServerId}")]
    [Authorize(Roles = "Administrator")]
    public async Task<FysiekeServerResponse.Details> GetDetailServer([FromRoute] FysiekeServerRequest.Detail request)
    {
        Console.WriteLine($"-----Getting details server with id = {request.ServerId}-----");
        var reponse = await fysiekeServerService.GetDetailsAsync(request);
        return reponse;
    }

    [HttpPost("available")]
    public async Task<FysiekeServerResponse.ResourcesAvailable> GetAvailableAsync([FromBody] FysiekeServerRequest.Date request)
    {
        Console.WriteLine($"-----Getting hardware server with date={request.ToDate} / ${request.FromDate}-----");
        var response = await fysiekeServerService.GetAvailableHardWareOnDate(request);
        return response;
    }

    [HttpPost("graph")]
    public async Task<FysiekeServerResponse.GraphValues> GetGraphValues([FromBody] FysiekeServerRequest.Date request)
    {
        Console.WriteLine($"-----Getting graphvalues of server with date={request.ToDate} / {request.FromDate}-----");
        var response = await fysiekeServerService.GetGraphValueForServer(request);
        return response;
    }
}
