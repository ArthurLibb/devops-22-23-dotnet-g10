using Microsoft.AspNetCore.Components;
using Shared.Servers;

namespace Client.Servers;

public partial class Index
{
    [Inject] public IFysiekeServerService FysiekeServerService { get; set; }
    [Inject] public NavigationManager Router { get; set; }
    private List<FysiekeServerDto.Index> Servers { get; set; }
    private bool Loading = false;
    protected override async Task OnInitializedAsync()
    {
        Loading = true;
        var response = await FysiekeServerService.GetAllServers();
        Servers = response.Servers;
        Loading = false;
    }

    public void RedirectToDetailsPage(int id)
    {
        Router.NavigateTo($"servers/{id}");

    }
}