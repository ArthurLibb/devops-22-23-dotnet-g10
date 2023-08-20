using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Shared.Users;

namespace Client.Users;

public partial class Index
{
    [Inject] public IUserService UserService { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    private List<KlantDto.Index> Klanten { get; set; }
    private bool Loading = false;

    protected override async Task OnInitializedAsync()
    {
        Loading = true;
        UserRequest.AllKlantenIndex request = new();
        var response = await UserService.GetAllKlanten(request);
        Klanten = response.Klanten;
        Loading = false;
    }
    private void NavToDetail(int id)
    {
        NavigationManager.NavigateTo($"klant/{id}");
    }
}