using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Servers;
using System;
using System.Threading.Tasks;

namespace Server.Controllers;

[Authorize(Roles = "Administrator")]
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
    public async Task<FysiekeServerResponse.Available> GetAllServers()
    {
        Console.WriteLine("-----Getting all servers-----");
        var response = await fysiekeServerService.GetAllServers();
        return response;
    }
    [HttpGet("{ServerId}")]
    public async Task<FysiekeServerResponse.Details> GetDetailServer([FromRoute] FysiekeServerRequest.Detail request)
    {
        Console.WriteLine($"-----Getting details server with id = {request.ServerId}-----");
        var reponse = await fysiekeServerService.GetDetailsAsync(request);
        return reponse;
    }
}
