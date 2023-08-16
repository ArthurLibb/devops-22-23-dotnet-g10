using Append.Blazor.Sidepanel;
using Microsoft.AspNetCore.Components;
using Shared.Projects;
using Client.VirtualMachines.Components;
using Microsoft.AspNetCore.Components.Web;
using JetBrains.Annotations;
using System;
using Microsoft.AspNetCore.Components.Routing;


namespace Client.VirtualMachines
{
    public partial class Index
    {
        [Inject] public IProjectService ProjectService { get; set; }
        [Inject] public ISidepanelService Sidepanel { get; set; }


        [Inject] NavigationManager Router { get; set; }

        private List<ProjectDto.Index> _projects;

        private Dictionary<int, ProjectDto.Detail> _details = new Dictionary<int, ProjectDto.Detail>();

        protected override async Task OnInitializedAsync()
        {

            ProjectRequest.All request = new();

            var response = await ProjectService.GetIndexAsync(request);
            _projects = response.Projects;

        }


        public async Task GetVirtualMachines(int projectId)
        {
            ProjectRequest.Detail request = new();

            request.ProjectId = projectId;

            var response = await ProjectService.GetDetailAsync(request);
            ProjectDto.Detail resp = new ProjectDto.Detail()
            {
                Id = response.Project.Id,
                Klant = response.Project.Klant,
                Name = response.Project.Name,
                VirtualMachines = response.Project.VirtualMachines
            };


            _details.Add(projectId, resp);
        }
        public void NavigateToVMDetails(int id)
        {
            Router.NavigateTo("virtualmachine/" + id);
        }

    }
}