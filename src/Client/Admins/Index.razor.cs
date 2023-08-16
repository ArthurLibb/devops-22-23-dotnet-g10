using Microsoft.AspNetCore.Components;
using Shared.Users;

namespace Client.Admins;

public partial class Index
{
    [Inject] public IUserService UserService { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    private List<AdminUserDto.Index> Beheerders { get; set; }

    protected override async Task OnInitializedAsync()
    {
        UserRequest.AllAdminUsers request = new();
        var response = await UserService.GetAllAdminsIndex(request);
        Beheerders = response.Admins;
    }

    private void NavToDetailAdmin(int id)
    {
        NavigationManager.NavigateTo($"beheerders/{id}");
    }

}
