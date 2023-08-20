using Client.Pages;
using Domain.Common;
using Domain.VirtualMachines.BackUp;
using Domain.VirtualMachines.VirtualMachine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Projects;
using Shared.VirtualMachines;
using System.Runtime.CompilerServices;

namespace Client.VirtualMachines;

public partial class CreateVM
{
    private bool Loading = false;
    private int UserId = 0;
    private bool AskForLogin { get;  set; }
    private  List<ProjectDto.Index> _projects = new();
    private AuthenticationState AuthenticationState { get; set; }
    public VirtualMachineDto.Mutate model = new();

    [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }
    [Inject] public IProjectService ProjectService { get; set; }
    [Inject] public IVirtualMachineService VirtualMachineService { get; set; }
    [Inject] NavigationManager NavMan { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Loading = true;
        AuthenticationState = await authenticationStateTask;
        model.Hardware = new Hardware(4, 64, 1);
        model.Backup = new Backup(BackUpType.CUSTOM, null);
        model.Start = DateTime.Now;
        model.End = DateTime.Now.AddDays(1);


        if (AuthenticationState.User.IsInRole("Administrator"))
        {
            var request = new ProjectRequest.All { SearchTerm = string.Empty };
            var response = await ProjectService.GetIndexAsync(request);
            _projects = response.Projects;

        }
        else
        {
            AskForLogin = true;
            _projects = null;
        }
        Loading = false;
    }

    private async Task MaakVirtualMachine()
    {
        if(UserId == 0)
        {
            var klant = _projects.Where(p => p.Id == model.ProjectId).Select(p => p.Klant).FirstOrDefault();
            VirtualMachineRequest.Create request = new()
            {
                CustomerId = klant.Id,
                VirtualMachine = model

            };
            await VirtualMachineService.CreateAsync(request);
            NavMan.NavigateTo($"/virtualmachines");
        }
        else
        {
            VirtualMachineRequest.Create request = new()
            {
                CustomerId = UserId,
                VirtualMachine = model
                       
            };
            await VirtualMachineService.CreateAsync(request);
        }

    }

    private void HandleShowParameterChanged(bool newValue)
    {
        AskForLogin = newValue;
    }

    private async void HandleGettingUserId(int id)
    {
        model.Hardware = new Hardware(4, 64, 1);
        model.Backup = new Backup(BackUpType.CUSTOM, null);
        model.Start = DateTime.Now;
        model.End = DateTime.Now.AddDays(1);

        UserId = id;
        var response = await ProjectService.GetProjectsByUserId(UserId);
        _projects = response.Projects.Select(project => new ProjectDto.Index { Id = project.Id, Name = project.Name }).ToList();
        if(_projects.Count() != 0)
        {
            model.ProjectId = _projects[0].Id;
        }
        else
        {
            model.ProjectId = 0;
        }
        StateHasChanged();
    }

    private void Logout()
    {
        AskForLogin = true;
    }

}
