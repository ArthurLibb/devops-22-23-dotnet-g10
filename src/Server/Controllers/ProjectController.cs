using Microsoft.AspNetCore.Mvc;
using Shared.Projects;
using System;
using System.Threading.Tasks;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    public ProjectController(IProjectService projectenSerivce)
    {
        this._projectService = projectenSerivce;
    }

    [HttpGet]
    public async Task<ProjectResponse.All> GetAllProjects()
    {
        var log = await _projectService.GetIndexAsync(new ProjectRequest.All());
        return log;
    }

    [HttpGet("id")]
    public async Task<ProjectResponse.Detail> GetDetails(ProjectRequest.Detail request)
    {
        var proj = await _projectService.GetDetailAsync(request);
        return proj;
    }
}
