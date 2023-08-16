
using Append.Blazor.Sidepanel;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shared.VirtualMachines;
using Client.Infrastructure;
using Shared.Projects;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Client.VirtualMachines;
using Shared.Users;
using Client.Users;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            /*allservices*/
            builder.Services.AddScoped<IVirtualMachineService, VirtualMachineService>();
            builder.Services.AddScoped<IUserService, UsersService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            //builder.Services.AddScoped<IFysiekeServerService, FysiekeServerService>();
            //builder.Services.AddScoped<IVMContractService, VMContractService>();

            //AUTHENTICATION
            builder.Services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin-Consultant", "Admin-Beheer"));
                options.AddPolicy("BeheerOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin-Beheer"));
                options.AddPolicy("LoggedIn", policy => policy.RequireAuthenticatedUser());
                options.AddPolicy("Guest", policy => policy.RequireClaim(ClaimTypes.Name, "Guest"));
            });

             //Disble both to do login via Auth0
            builder.Services.AddScoped<Shared.FakeAuthenticationProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<Shared.FakeAuthenticationProvider>());

            //builder.Services.AddHttpClient("AuthenticatedServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            builder.Services.AddScoped(s => s.GetRequiredService<IHttpClientFactory>().CreateClient("Project.Api"));
            builder.Services.AddSidepanel();
            builder.Services.AddHttpClient("AuthenticatedServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                  .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                   .CreateClient("AuthenticatedServerAPI"));
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            await builder.Build().RunAsync();

        }
    }
}
