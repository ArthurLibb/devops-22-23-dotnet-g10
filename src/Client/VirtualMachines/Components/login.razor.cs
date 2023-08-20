using Microsoft.AspNetCore.Components;
using Shared.Authentication;

namespace Client.VirtualMachines.Components;

public partial class Login 
{ 

    private bool Loading = false;
    [Parameter] public EventCallback<int> ReturnUserId { get; set; }
    [Parameter] public EventCallback<bool> OnShowParameterChanged { get; set; }
    [Inject] public IAuthenticationService AuthenticationService { get; set; }

    private AuthenticationRequest.Login model = new();
    private String text = string.Empty;

    private async Task LoginUser()
    {
        Loading = true;
        if (model.Email == null || model.Password == null) { text = "Vul alle gegevens in!" ; }
        else {
            var response = await AuthenticationService.Login(model);
            if (response == null || response.Id == -1)
            {
                text = "Password/Email zijn fout.";
            }
            else
            {
                await ReturnuserId(response.Id);
                await ChangeShowParameter();
            }
        }
        Loading = false;
    }

    private async Task ChangeShowParameter()
    {
        await OnShowParameterChanged.InvokeAsync(false);
    }

    private async Task ReturnuserId(int id)
    {
        await ReturnUserId.InvokeAsync(id);
    }
}
