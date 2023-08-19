using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Users;
using System;
using System.Threading.Tasks;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        this._userService = userService;
    }

    [HttpGet]
    public async Task<UserResponse.AllKlantenIndex> GetAllKlanten()
    {
        var klanten = await _userService.GetAllKlanten(new UserRequest.AllKlantenIndex());
        return klanten;
    }

    [HttpGet("{KlantId}")]
    public async Task<UserResponse.DetailKlant> GetDetailKlant([FromRoute] UserRequest.DetailKlant request)
    {
        Console.WriteLine($"------Start  Get deatils klant id = {request.KlantId}------");
        var klant = await _userService.GetDetailKlant(request);
        Console.WriteLine($"------End Controller details klant------");
        return klant;
    }

    [HttpGet("admins")]
    [Authorize(Roles = "Administrator")]
    public async Task<UserResponse.AllAdminsIndex> GetAllAdmins()
    {
        var admins = await _userService.GetAllAdminsIndex(new UserRequest.AllAdminUsers());
        return admins;
    }

    [HttpPut("{KlantId}")]
    [Authorize(Roles = "Administrator")]
    public async Task<UserResponse.Edit> UpdateKlant([FromBody] UserRequest.Edit request)
    {
        Console.WriteLine($"----Start Update klant controller id ={request.KlantId}----");
        var reponse = await _userService.EditAsync(request);
        Console.WriteLine($"----End Update klant controller----");
        return reponse;
    }

    [HttpGet("admin/{AdminId}")]
    public async Task<UserResponse.DetailAdmin> GetDetailsAdmin([FromRoute] UserRequest.Detailadmin request)
    {
        Console.WriteLine($"------Start Controller Get deatils admin id = {request.AdminId}------\n");
        var admin = await _userService.GetAdminDetails(request);
        Console.WriteLine($"------End Controller Get deatils admin------\n");
        return admin;
    }

    [HttpGet("android/{Id}")]
    public async Task<ActionResult<UserResponse.DetailKlant>> GetUserById(int id)
    {
        Console.WriteLine($"----Getting user with id={id}-----");
        var request = new UserRequest.DetailKlant { KlantId = id};
        var user = await _userService.GetDetailKlant(request);
        return user;
    }

    [HttpPut("android/{Id}")]
    public async Task<ActionResult<UserResponse.Edit>> UpdateUser([FromBody] UserRequest.Edit request)
    {
        Console.WriteLine($"----Udpating user with id={request.KlantId}");
        var reponse = await _userService.EditAsync(request);
        return reponse;
    }
}
