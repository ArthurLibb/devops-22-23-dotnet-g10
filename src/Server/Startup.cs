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
            options.UseSqlServer(Configuration.GetConnectionString("DotNet"));

        });

        services.AddLogging(log =>
        {
            log.ClearProviders();
            log.AddConsole();
        });

        services.AddControllersWithViews();
        services.AddHttpContextAccessor();
        services.AddTransient(sp => sp.GetRequiredService<IHttpContextAccessor>().HttpContext.User);
        services.AddRazorPages();
        services.AddScoped<IUserService, UserService>();
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
            app.UseHsts();
        }

        dataInitializer.Seed();

        app.UseHttpsRedirection();
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
