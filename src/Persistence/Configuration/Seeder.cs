using Domain.Projecten;
using Domain.Users;
using Domain.VirtualMachines.VirtualMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration;

public class Seeder
{
    private readonly HerExamenDBContext DbContext;

    public Seeder(HerExamenDBContext herExamenDBContext)
    {
        this.DbContext = herExamenDBContext;
    }

    public void Seed()
    {
        DbContext.Database.EnsureDeleted();
        if (DbContext.Database.EnsureCreated())
        {
            //SeedVirtualMachines();
            seedKlanten();
            seedProjects();
        }
    }

    private void SeedVirtualMachines()
    {
        var vmList = VirtualMachineFaker.Instance.Generate(15);
        DbContext.Virtualmachine.AddRange(vmList);
        DbContext.SaveChanges();

    }
    private void seedKlanten()
    {
        var amins = UserFaker.Administrators.Instance.Generate(3);
        var klanten = UserFaker.Klant.Instance.Generate(15);

        DbContext.klanten.AddRange(klanten);
        DbContext.gebruikers.AddRange(amins);
        DbContext.SaveChanges();
    }

    private void seedProjects()
    {
        var projecten = ProjectFaker.Instance.Generate(4);
        DbContext.projecten.AddRange(projecten);
        DbContext.SaveChanges();
    }
}
