using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;
using Shared.Users;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Users;

public class UserService : IUserService
{
    private readonly HerExamenDBContext _dbContext;

    public UserService(HerExamenDBContext dBContext)
    {
        _dbContext = dBContext;
    }
    public Task<UserResponse.Edit> EditAsync(UserRequest.Edit request)
    {
        throw new NotImplementedException();
    }

    public async Task<UserResponse.DetailAdmin> GetAdminDetails(UserRequest.Detailadmin request)
    {
        Console.WriteLine($"------Start Service Get deatils admin id = {request.AdminId}------");
        var admin = await _dbContext.admins.FirstOrDefaultAsync(a => a.Id == request.AdminId);
        AdminUserDto.Details details = new();
        details.PhoneNumber = admin.PhoneNumber;
        details.Email = admin.Email;
        details.Id = admin.Id;
        details.Name = admin.Name;
        details.FirstName = admin.FirstName;
        details.Password = admin.Password;
        details.Role = admin.Role;
        UserResponse.DetailAdmin response = new UserResponse.DetailAdmin { Admin =  details };
        Console.WriteLine($"------End Service Get deatils admin id = {request.AdminId}------\n");
        return response;
    }

    public async Task<UserResponse.AllAdminsIndex> GetAllAdminsIndex(UserRequest.AllAdminUsers request)
    {
        var admins =  await _dbContext.admins.Select(a => new AdminUserDto.Index { Id = a.Id, Name = a.Name, FirstName = a.FirstName, Password = a.Password, Role = a.Role }).ToListAsync();
        UserResponse.AllAdminsIndex response = new();
        response.Admins = admins;
        response.Total = admins.Count;
        return response;
    }

    public async Task<UserResponse.AllKlantenIndex> GetAllKlanten(UserRequest.AllKlantenIndex request)
    {
        UserResponse.AllKlantenIndex response = new();
        var klanten = await _dbContext.klanten.Select(k => new KlantDto.Index {Id = k.Id, FirstName = k.FirstName, Email = k.Email, PhoneNumber = k.PhoneNumber }).ToListAsync();
        response.Klanten = klanten;
        return response;
    }

    public async Task<UserResponse.DetailKlant> GetDetailKlant(UserRequest.DetailKlant request)
    {
        UserResponse.DetailKlant response = new();
        var klant = await _dbContext.klanten.FirstAsync(k => k.Id == request.KlantId);
        if (klant is null) return null;

        KlantDto.Detail details = new();
        details.Name = klant.Name; 
        details.Email = klant.Email;
        details.PhoneNumber = klant.PhoneNumber; 
        details.FirstName = klant.FirstName;

        if(klant is InterneKlant)
        {
            details.Opleiding = ((InterneKlant)klant).Opleiding;
        }
        else
        {
            ExterneKlant kl = await _dbContext.externeKlanten.Include(k => k.ContactPersoon).
                                                                Include(k => k.TweedeContactPersoon)
                                                                .FirstOrDefaultAsync(k => k.Id == request.KlantId);
            details.Bedrijf = kl.Bedrijfsnaam;
            details.contactPersoon = kl.ContactPersoon;
            details.ReserveContactPersoon = kl.TweedeContactPersoon;
        }
        response.Klant = details;
        return response;
    }
}
