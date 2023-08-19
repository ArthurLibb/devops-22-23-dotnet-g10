﻿using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Administrator")]
    public async Task<ProjectResponse.All> GetAllProjects()
    {
        var log = await _projectService.GetIndexAsync(new ProjectRequest.All());
        return log;
    }

    [HttpGet("{ProjectId}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ProjectResponse.Detail> GetDetails([FromRoute] ProjectRequest.Detail request)
    {
        Console.WriteLine($"-------- Start Controller get details proj met id = {request.ProjectId}--------\n");
        var proj = await _projectService.GetDetailAsync(request);
        Console.WriteLine($"-------- End Controller get details proj id = {request.ProjectId}---------");
        return proj;
    }

    [HttpGet("customer/{Id}")]
    public async Task<ActionResult<ProjectResponse.App>> GetProjectsByUserId([FromRoute] int id)
    {
        var porjects = await _projectService.GetProjectsByUserId(id);
        return Ok(porjects);
    }
}
