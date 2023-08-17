using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Auth0.ManagementApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Auth0;
using System.Linq;

namespace Server.Controllers;


[ApiController]
[Route("[controller]")]
public class Auth0UserController : ControllerBase
{
    private readonly IManagementApiClient _managementApiClient;

    public Auth0UserController(IManagementApiClient managementApiClient)
    {
        _managementApiClient = managementApiClient;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public async Task<IEnumerable<Auth0UserDto.Index>> GetUsers()
    {
        var users = await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
        return users.Select(x => new Auth0UserDto.Index
        {
            Email = x.Email,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Blocked = x.Blocked ?? false,
        });

    }

    /*[HttpGet("roles")]
    public async Task<IEnumerable<Auth0UserDto.Index>> getRolesUser()
    {
        var rolesUser = await _managementApiClient.Users.GetRolesAsync()
    }
*/

}

