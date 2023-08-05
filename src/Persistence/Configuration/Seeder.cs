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
        DbContext.Database.EnsureCreated();
    }
}
