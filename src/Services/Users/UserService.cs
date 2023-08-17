using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Domain.Common;
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
    public async Task<UserResponse.Edit> EditAsync(UserRequest.Edit request)
    {
        var klant = await _dbContext.klanten.FirstAsync(k => k.Id == request.KlantId);

        if (klant is not null)
        {
            klant.Name = request.Klant.Name;
            klant.FirstName = request.Klant.FirstName;
            klant.Email = request.Klant.Email;
            klant.PhoneNumber = request.Klant.PhoneNumber;

            if (klant is ExterneKlant)
            {
                ExterneKlant kl = await _dbContext.externeKlanten.Include(k => k.ContactPersoon).
                                                                Include(k => k.TweedeContactPersoon)
                                                                .FirstOrDefaultAsync(k => k.Id == request.KlantId);
                kl.Bedrijfsnaam = request.Klant.Bedrijf;
                if (!HasNullOrEmptyAttributes(request.Klant.contactPersoon))
                {
                    kl.ContactPersoon = new ContactDetails
                    {
                        FirstName = request.Klant.contactPersoon.FirstName,
                        LastName = request.Klant.contactPersoon.LastName,
                        PhoneNumber = request.Klant.contactPersoon.PhoneNumber,
                        Email = request.Klant.contactPersoon.Email,
                    };
                }
                _dbContext.Entry(kl).State = EntityState.Modified;
            }
            _dbContext.Entry(klant).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        return new UserResponse.Edit{ Id = klant.Id};
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
        var klant = await _dbContext.klanten.FirstAsync(k => k.Id == request.KlantId);
        if (klant is null) return null;
        UserResponse.DetailKlant details = new();
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
            details.contactPersoon = new ContactdetailsDto.Index 
            { 
                FirstName = kl.ContactPersoon?.FirstName,                                                            
                LastName = kl.ContactPersoon?.LastName,          
                PhoneNumber = kl.ContactPersoon?.PhoneNumber,                                                 
                Email = kl.ContactPersoon?.Email 
            };
            details.ReserveContactPersoon = new ContactdetailsDto.Index
            {
                FirstName = kl.TweedeContactPersoon?.FirstName,
                LastName = kl.TweedeContactPersoon?.LastName,
                PhoneNumber = kl.TweedeContactPersoon?.PhoneNumber,
                Email = kl.TweedeContactPersoon?.Email
            };
        }

        return details;
    }

    public static bool HasNullOrEmptyAttributes(ContactdetailsDto.Index obj)
    {
        var properties = typeof(ContactdetailsDto.Index).GetProperties();

        foreach (var property in properties)
        {
            if (property.PropertyType == typeof(string))
            {
                string value = (string)property.GetValue(obj);
                if (string.IsNullOrEmpty(value))
                {
                    return true; 
                }
            }
        }

        return false;
    }

    public async Task<UserResponse.CurrentUser> GetUserByEmail(string email)
    {
        var user = await _dbContext.gebruikers.FirstOrDefaultAsync(k => k.Email == email);
        if(user == null) { return null; }
        var response = new UserResponse.CurrentUser{ UserId = user.Id, Email = user.Email, Password = user.Password };
        return response;
    }
}
