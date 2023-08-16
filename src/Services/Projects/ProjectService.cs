using System.Linq;
using Persistence;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Projecten;
using System;
using Domain.Common;
using Shared.Projects;
using Persistence.Configuration;
using System.Collections.Generic;
using Shared.VirtualMachines;

namespace Services.Projecten
{
    public class ProjectService : IProjectService
    {
        public ProjectService(HerExamenDBContext dbContext)
        {
            _dbContext = dbContext;
            _projecten = dbContext.projecten;
        }

        private readonly HerExamenDBContext _dbContext;
        private readonly DbSet<Project> _projecten;


        private IQueryable<Project> GetProjectById(int id) => _projecten
                .AsNoTracking()
                .Where(p => p.Id == id);


        public async Task<ProjectResponse.All> GetIndexAsync(ProjectRequest.All request)
        {
            Console.WriteLine($"--------------------------------------------");
            ProjectResponse.All response = new();
            response.Projects = new();
            List<Project> projects;

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                projects = await _dbContext.projecten.Include( p => p.VirtualMachines).ThenInclude(v => v.Contract).ToListAsync();
                projects = projects.FindAll(e => e.Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase) || e.Klant.Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                projects = await _dbContext.projecten.Include(p => p.VirtualMachines).ThenInclude(v => v.Contract).ToListAsync();
            }

            foreach(var p in projects)
            {
                response.Projects.Add(new ProjectDto.Index {Id = p.Id, Klant = p.Klant, Name = p.Name });
            }

            response.Total = projects.Count();
            Console.WriteLine($"{response}");
            return response;
        }

        public async Task<ProjectResponse.Detail> GetDetailAsync(ProjectRequest.Detail request)
        {
            List<VirtualMachineDto.Index> vms = new();
            ProjectResponse.Detail response = new();

            Project project = await _dbContext.projecten.Include(p => p.VirtualMachines).ThenInclude(v => v.Contract).SingleOrDefaultAsync(p => p.Id == request.ProjectId);
            if(project == null)
            {
                response.Project.Id = -1;
                return response;
            }
            project.VirtualMachines.ForEach(v => vms.Add(new VirtualMachineDto.Index() {Id= v.Id, Mode = v.Mode, Name = v.Name }));
            response.Project = new ProjectDto.Detail() { Id = project.Id, Klant = project.Klant, Name = project.Name, VirtualMachines = vms};

            return response;
        }

        public Task DeleteAsync(ProjectRequest.Delete request)
        {
            /*_projecten.RemoveIf(p => p.Id == request.ProjectenId);
            await _dbContext.SaveChangesAsync();*/ 
            throw new NotImplementedException();
        }

        public Task<ProjectResponse.Create> CreateAsync(ProjectRequest.Create request)
        {
            /* ProjectResponse.Create response = new();
             var project = _projecten.Add(new Project(
                 request.Project.Name
             ));
             await _dbContext.SaveChangesAsync();
             response.ProjectenId = project.Entity.Id;
             return response;*/
            throw new NotImplementedException();
        }

        public Task<ProjectResponse.Edit> EditAsync(ProjectRequest.Edit request)
        {
            /*    ProjectResponse.Edit response = new();
                var project = await GetProjectById(request.ProjectenId).SingleOrDefaultAsync();

                if (project is not null)
                {
                    var model = request.Projecten;

                    // You could use a Project.Edit method here.
                    project.Name = model.Name;
                    project.User = model.user;
                    project.VirtualMachines = model.VirtualMachines;


                    _dbContext.Entry(project).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    response.ProjectenId = project.Id;
                }
    */
            throw new NotImplementedException();
        }
    }
}
