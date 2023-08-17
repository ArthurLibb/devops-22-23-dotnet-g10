using Microsoft.AspNetCore.Mvc;
using Shared.Authentication;
using System;
using System.Threading.Tasks;

namespace Server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IAuthenticationService authenticationService;

    public LoginController(IAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] AuthenticationRequest.Login request)
    {
        var reponse =await authenticationService.Login(request);
        if(reponse == null || !reponse.Correct) { return StatusCode(403); }
        return Ok(reponse.Correct);
    }

    [HttpPost]
    public async Task<ActionResult<AuthenticationResponse>> Register([FromBody] AuthenticationRequest.Register request)
    {
        //var response = await authenticationService.Register(request);
        throw new NotImplementedException();
    }

}
