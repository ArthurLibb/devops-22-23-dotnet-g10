using Microsoft.AspNetCore.Components;
using Shared.Users;

namespace Client.Admins;

partial class Details
{
    [Inject] public IUserService UserService { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    [Parameter] public int Id { get; set; }
    public bool Loading = false;
    private AdminUserDto.Details Admin { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Loading = true;
        UserRequest.Detailadmin request = new();
        request.AdminId = Id;
        var response = await UserService.GetAdminDetails(request);
        Admin = response.Admin;
        Loading = false;
    }

    
}
