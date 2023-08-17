using Domain.Common;
using Domain.Projecten;
using Domain.Server;
using Domain.Users;
using Domain.Utility;
using Domain.VirtualMachines.Contract;
using Domain.VirtualMachines.VirtualMachine;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration;

public class DataInitializer
{
    private readonly HerExamenDBContext DbContext;

    public DataInitializer(HerExamenDBContext herExamenDBContext)
    {
        this.DbContext = herExamenDBContext;
    }

    public void Seed()
    {
        DbContext.Database.EnsureDeleted();
        if (DbContext.Database.EnsureCreated())
        {
            
            seedAdmins();
            seedKlanten();
            SeedVirtualMachines();
            seedProjects();
            seedContracts();
        }
    }

    private void SeedVirtualMachines()
    {
        var vms = VirtualMachineFaker.Instance.Generate(20);
        var vmsWithoutContracts = vms.Select(e => { e.Contract = null; return e; });

        var backups = vms.Select(e => e.BackUp);
        DbContext.backups.AddRange(backups);
        DbContext.SaveChanges();

        var connections = vms.Select(e => e.Connection);
        DbContext.connections.AddRange(connections);
        DbContext.SaveChanges();

        var servers = vms.Select(e => e.FysiekeServer);


        vms.ForEach(e =>
        {
            string name = e.FysiekeServer.Naam;
            FysiekeServer server = servers.First(e => e.Naam == name);
            server.AddConnection(e);
        });


        DbContext.fysiekeServers.AddRange(servers);
        DbContext.SaveChanges();

        DbContext.Virtualmachines.AddRange(vms);
        DbContext.SaveChanges();
    }
    private void seedKlanten()
    {
        var amins = UserFaker.Administrators.Instance.Generate(3);
        var klanten = UserFaker.Klant.Instance.Generate(15);

        DbContext.klanten.AddRange(klanten);
        DbContext.admins.AddRange(amins);
        DbContext.SaveChanges();
    }

    private void seedProjects()
    {
        var customers = DbContext.klanten.ToList();
        var virtualMachines = DbContext.Virtualmachines.ToList();

        var project1 = new Project("een project");
        var project2 = new Project("Tweede Proj");
        var project3 = new Project("Dit is ook een project");


        project1.Klant = customers[4];
        project2.Klant = customers[1];
        project3.Klant = customers[2];

        project1.VirtualMachines = virtualMachines.Take(6).ToList();
        project2.VirtualMachines = virtualMachines.Skip(6).Take(6).ToList();
        project3.VirtualMachines = virtualMachines.Skip(12).Take(8).ToList();

        DbContext.projecten.AddRange(project1, project2, project3);
        DbContext.SaveChanges();
    }

    private void seedAdmins() 
    {
        var specialAdmin = new Administrator("Arthur", "L", "0474619443", "arthurlibberecht@gmail.com", "yowyow",AdminRole.BEHEREN);
        DbContext.admins.Add(specialAdmin);
        DbContext.SaveChanges();
    }

    private void seedContracts()
    {
        var proj = DbContext.projecten.ToList();
        var contracts = new List<VMContract>();

        foreach(var pro in proj)
        {
            var klant = pro.Klant;
            var vmList = pro.VirtualMachines;
            foreach (var vm in vmList)
            {
                    var newCon = new VMContract(klant.Id, vm.Id, DateTime.Now.Subtract(TimeSpan.FromDays(RandomNumberGenerator.GetInt32(300))), DateTime.Now.AddDays(RandomNumberGenerator.GetInt32(200)));
                    vm.Contract = newCon;
                    contracts.Add(newCon);
            }
        }
        DbContext.vMContracts.AddRange(contracts);
        DbContext.SaveChanges(true);
    }
}
