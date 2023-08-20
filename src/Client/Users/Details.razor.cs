using Domain.Common;
using Microsoft.AspNetCore.Components;
using Shared.Users;

namespace Client.Users;

public partial class Details
{
    [Parameter] public int Id { get; set; }
    [Inject] public IUserService UserService { get; set; }

    private ContactdetailsDto.Index contactDetails = new();
    private KlantDto.Mutate model = new();
    private KlantDto.Detail Klant;

    public bool Edit = false;
    public bool Loading = false;
    public bool Intern = false;

    protected override async Task OnInitializedAsync()
    {
        
        await GetKlantAsync();
        ObjectToMutate();

    }

    private async Task GetKlantAsync()
    {
        Loading = true;
        var request = new UserRequest.DetailKlant { KlantId = Id};
        Klant = await UserService.GetDetailKlant(request);

        if (Klant.Opleiding is not null)
        {
            Intern = true;
        }

        Loading = false;
    }
    public void Toggle()
    {
        Edit = !Edit;
    }

    private async void EditKlant()
    {
        model.contactPersoon = contactDetails;
        UserRequest.Edit request = new(){KlantId = Id,Klant = model};
        await UserService.EditAsync(request);

        Klant = await UserService.GetDetailKlant(new UserRequest.DetailKlant() { KlantId = Id });

        ObjectToMutate();
        Toggle();
        StateHasChanged();
    }

    public void ObjectToMutate()
    {
        model.FirstName = Klant.FirstName;
        model.Name = Klant.Name;
        model.Email = Klant.Email;
        model.PhoneNumber = Klant.PhoneNumber;
        model.Opleiding = Klant.Opleiding;
        model.Bedrijf = Klant.Bedrijf;
        if(Klant.contactPersoon is not null)
        {
            contactDetails = Klant.contactPersoon;
            model.contactPersoon = Klant.contactPersoon;
        }
    }
}