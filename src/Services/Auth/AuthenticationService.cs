using Domain;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;
using Shared.Authentication;
using Shared.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserService userService;
    private readonly HerExamenDBContext dBContext;

    public AuthenticationService(IUserService userService, HerExamenDBContext herExamenDBContext)
    {
        this.userService = userService;
        dBContext = herExamenDBContext;
    }

    public async Task<AuthenticationResponse> Login(AuthenticationRequest.Login request)
    {
        var user = await userService.GetUserByEmail(request.Email);
        if (user == null) { return null; }
        var correct = await dBContext.gebruikers.Where(g => g.Email == request.Email && g.Password == request.Password).FirstOrDefaultAsync();
        if (correct == null) { return null; }
        var reponse = new AuthenticationResponse { Id = correct.Id };
        return reponse;
    }

    public async Task<AuthenticationResponse> Register(AuthenticationRequest.Register request)
    {
        dBContext.gebruikers.Add(new ExterneKlant
        {
            FirstName = request.FirstName,
            Name = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Password = request.Password,
            Bedrijfsnaam = "android"
        });
        dBContext.SaveChanges();
        var user = await userService.GetUserByEmail(request?.Email);
        return new AuthenticationResponse { Id = user.UserId};
    }
}
