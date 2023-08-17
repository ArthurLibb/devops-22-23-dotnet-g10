using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;
using Shared.Authentication;
using Shared.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.NewFolder;

internal class AuthenticationService : IAuthenticationService
{
    private readonly IUserService userService;
    private readonly HerExamenDBContext dBContext;

    public AuthenticationService(IUserService userService, HerExamenDBContext herExamenDBContext)
    {
        this.userService = userService;
        this.dBContext = herExamenDBContext;
    }

    public async Task<AuthenticationResponse> Login(AuthenticationRequest.Login request)
    {
        var user = await userService.GetUserByEmail(request.Email);
        if (user == null) { return null; }
        var correct = await dBContext.gebruikers.Where(g => g.Email == request.Email && g.Password == request.Password).FirstOrDefaultAsync();
        if(correct == null) { return new AuthenticationResponse { Correct = false }; }
        var reponse = new AuthenticationResponse { Correct = true};
        return reponse;
    }

    public Task<AuthenticationResponse> Register(AuthenticationRequest.Register request)
    {
        throw new NotImplementedException();
    }
}
