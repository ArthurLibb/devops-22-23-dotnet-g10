﻿using Domain.Users;
using Shared.Projects;
using Shared.Users;


namespace Services.Users
{
    public class FakeUserService: IUserService
    {
        private List<Klant> _klanten;
        private List<Administrator> _admins;


        public FakeUserService()
        {
            _klanten = new UserFaker.Klant().Generate(20);
            _admins = new UserFaker.Administrators().Generate(3);

        }

        public async Task<UserResponse.AllKlantenIndex> GetAllKlantenIndexAsync(UserRequest.AllKlantenIndex request)
        {
            await Task.Delay(100);
            UserResponse.AllKlantenIndex response = new();
            response.Klanten = _klanten.Select(x => new KlantDto.Index
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
            }).ToList();
            response.Total = _klanten.Count;

            return response;
        }

        public async Task<UserResponse.DetailKlant> GetDetailKlantAsync(UserRequest.DetailKlant request)
        {
            await Task.Delay(100);
            List<ProjectDto.Index> projecten = new();
            UserResponse.DetailKlant response = new();
            Klant k = _klanten.Single(x => x.Id == request.KlantId);
            k.Projecten.ForEach(p => projecten.Add(new ProjectDto.Index()
            {
                Id = p.Id,
                Name = p.Name,
                Klant = k
            }));
            var kdto = new KlantDto.Detail()
            {
                Id = k.Id,
                Name = k.Name,
                FirstName = k.FirstName,
                Email = k.Email,
                PhoneNumber = k.PhoneNumber,
                Projects = projecten,
                contactPersoon = k.ContactPersoon,
                ReserveContactPersoon = k.ContactPersoonReserv
        };

                
            if (k is InterneKlant)
            {
                InterneKlant kI = (InterneKlant)k;
                kdto.Opleiding = kI.Opleiding;
            }
            else
            {
                ExterneKlant kE = (ExterneKlant)k;
                kdto.Bedrijf = kE.Bedrijfsnaam;
            }
            
            return response;
        }

        // create shit
    }
}
