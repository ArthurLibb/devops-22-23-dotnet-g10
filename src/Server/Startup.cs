using Persistence.Configuration;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Services.Projecten;
using Services.Users;
using System.Data.SqlClient;
using Shared.VirtualMachines;
using Microsoft.Extensions.Logging;
using Shared.Servers;
using Shared.Projects;
using Shared.Users;
using Services.VirtualMachines;
using Services.FysiekeServers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shared.Authentication;
using Microsoft.AspNetCore.Authentication;
using Client.Pages;
using Client.Users.Auth0;
using System.Text.RegularExpressions;

namespace Server;


public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        
        services.AddDbContext<HerExamenDBContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("Dotnet"));
            options.EnableSensitiveDataLogging(Configuration.GetValue<bool>("Logging:EnableSqlParameterLogging"));
        });

        services.AddLogging(log =>
        {
            log.ClearProviders();
            log.AddConsole();
        });
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = Configuration["Auth0:Authority"];
            options.Audience = Configuration["Auth0:ApiIdentifier"];
        });

        services.AddAuth0AuthenticationClient(config =>
        {
            config.Domain = Configuration["Auth0:Authority"];
            config.ClientId = Configuration["Auth0:ClientId"];
            config.ClientSecret = Configuration["Auth0:ClientSecret"];
        });

        services.AddAuth0ManagementClient().AddManagementAccessToken();


        services.AddRazorPages();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<Shared.Authentication.IAuthenticationService, Services.Auth.AuthenticationService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IVirtualMachineService, VirtualMachineService>();
        services.AddScoped<IFysiekeServerService, FysiekeServerService>();
        services.AddScoped<DataInitializer>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, HerExamenDBContext dbContext,
        DataInitializer dataInitializer)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        dataInitializer.Seed();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("index.html");
        });
    }
}
