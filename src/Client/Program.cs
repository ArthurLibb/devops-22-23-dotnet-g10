using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Append.Blazor.Sidepanel;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shared.VirtualMachines;
using Client.Infrastructure;
using Shared.Projects;
using Client.VirtualMachines;
using Shared.Users;
using Client.Users;
using Client.Shared;
using Shared.Servers;
using Client.Servers;
using Shared.Authentication;
using Client.VirtualMachines.Components;
using Microsoft.AspNetCore.Authorization;
using Blazored.Toast;
using Blazored.Toast.Services;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddSidepanel();
            builder.Services.AddBlazoredToast();

            /*allservices*/
            builder.Services.AddScoped<IVirtualMachineService, VirtualMachineService>();
            builder.Services.AddScoped<IUserService, UsersService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IFysiekeServerService, FysiekeServicerService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<AuthorizationHandlerContext>();
            builder.Services.AddScoped<IToastService,  ToastService>();
            //builder.Services.AddScoped<IVMContractService, VMContractService>();


            //Auth0
            builder.Services.AddHttpClient("DevopsAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                   .CreateClient("DevopsAPI"));

            builder.Services.AddHttpClient<PublicClient>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("Auth0", options.ProviderOptions);
                options.ProviderOptions.ResponseType = "code";
                options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
            }).AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();

            await builder.Build().RunAsync();
        }
    }
}
