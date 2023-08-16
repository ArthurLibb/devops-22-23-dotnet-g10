using Microsoft.AspNetCore.Components;
using Shared.Users;

namespace Client.Users;

public partial class Details
{
    private KlantDto.Mutate model = new();
    public bool Loading = false;
    public bool Edit = false;
    public bool Intern = false;
    private KlantDto.Detail Klant;
    [Parameter] public int Id { get; set; }
    [Inject] public IUserService UserService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetKlantAsync();
        ObjectToMutate();
    }

    private async Task GetKlantAsync()
    {
        Loading = true;
        var request = new UserRequest.DetailKlant();
        request.KlantId = Id;
        var response = await UserService.GetDetailKlant(request);
        Klant = response.Klant;
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

    /*    private async void CheckboxChanged()
        {
            if (!Edit)
            {
                GetKlantAsync();
                Console.WriteLine(Loading);
                Loading = false;
            }

        }*/

    private async void EditKlant()
    {

        UserRequest.Edit request = new()
        {
            KlantId = Klant.Id,
            Klant = model
        };
        await UserService.EditAsync(request);

        var response = await UserService.GetDetailKlant(new UserRequest.DetailKlant() { KlantId = Klant.Id });
        Klant = response.Klant;
    }

    public void ObjectToMutate()
    {
        model.FirstName = Klant.FirstName;
        model.Name = Klant.Name;
        model.Email = Klant.Email;
        model.PhoneNumber = Klant.PhoneNumber;
        if (Klant.Opleiding is not null)
        {
            model.Opleiding = Klant.Opleiding;
        }
        else
        {
            model.Bedrijf = Klant.Bedrijf;
        }

    }
}