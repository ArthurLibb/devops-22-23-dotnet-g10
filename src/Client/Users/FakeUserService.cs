using Domain.Users;
using Shared.Projects;
using Shared.Users;


namespace Services.Users
{
    public class FakeUserService : IUserService
    {
        private List<Klant> _klanten;
        private List<Administrator> _admins;


        public FakeUserService()
        {
            _klanten = UserFaker.Klant.Instance.Generate(20);
            _admins = UserFaker.Administrators.Instance.Generate(3);

        }
        public async Task EditAsync(UserRequest.Edit request)
        {
            /*await Task.Delay(100);
            Klant klant = _klanten.Find(k => k.Id == request.KlantId);
            klant.FirstName = request.Klant.FirstName;
            klant.Name = request.Klant.Name;
            klant.Email = request.Klant.Email;
            klant.PhoneNumber = request.Klant.PhoneNumber;*/
            throw new NotImplementedException();
        }

        public Task<UserResponse.Edit> EditAsync(int id, UserRequest.Edit request)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse.DetailAdmin> GetAdminDetails(UserRequest.Detailadmin request)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse.AllAdminsIndex> GetAllAdminsIndex(UserRequest.AllAdminUsers request)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponse.AllKlantenIndex> GetAllKlanten(UserRequest.AllKlantenIndex request)
        {
            /*await Task.Delay(100);
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

            return response;*/
            throw new NotImplementedException();
        }

        public async Task<UserResponse.DetailKlant> GetDetailKlant(UserRequest.DetailKlant request)
        {
            /*await Task.Delay(100);
            List<ProjectDto.Index> projecten = new();
            UserResponse.DetailKlant response = new();
            Klant k = _klanten.Single(x => x.Id == request.KlantId);
            if (k is not null)
            {
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
                response.Klant = kdto;
            }
            else
            {
                response.Klant = new KlantDto.Detail { Id = -1 };
            }
            return response;*/
            throw new NotImplementedException ();
        }

        Task<UserResponse.Edit> IUserService.EditAsync(UserRequest.Edit request)
        {
            throw new NotImplementedException();
        }

        /*public async Task<UserResponse.AllAdminsIndex> GetAllAdminsIndex(UserRequest.AllAdminUsers request)
        {
            await Task.Delay(100);
            UserResponse.AllKlantenIndex response = new();
            List<Administrator> admins;
           response.Klanten = admins.Select(x => new AdminUserDto.Index
            {
                Id = x.Id,
                FirstName = x.FirstName,
                Name = x.Name,
                Password = x.Password,
                Role = x.Role,
            })
        }*/

        // create shit
    }
}