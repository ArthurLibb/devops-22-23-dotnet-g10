using Microsoft.AspNetCore.Mvc;
using Shared.Authentication;
using System;
using System.Threading.Tasks;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IAuthenticationService authenticationService;

    public LoginController(IAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    [HttpPost("android")]
    public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] AuthenticationRequest.Login request)
    {
        Console.WriteLine($"-----Loggin in with {request.Email} {request.Password}-----");
        var reponse =await authenticationService.Login(request);
        if (reponse == null) { return new AuthenticationResponse{Id = -1 }; }
        Console.WriteLine($"return: {Ok(reponse)}");
        return Ok(reponse);
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthenticationResponse>> Register([FromBody] AuthenticationRequest.Register request)
    {
        Console.WriteLine($"-----Registering User with:\n {request.FirstName} & {request.LastName} \n {request.PhoneNumber} & {request.Email} & {request.Password}-----");
        var response = await authenticationService.Register(request);
        return Ok(response);
    }

}
