using Domain.VirtualMachines.VirtualMachine;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkCore.Triggers;
using Persistence.Configuration.Virtualmachines;
using Domain;
using Domain.Users;
using Domain.VirtualMachines.BackUp;
using Domain.Projecten;
using Domain.VirtualMachines.Contract;
using Domain.Server;

namespace Persistence.Configuration;


public class HerExamenDBContext : DbContext
{
public DbSet<VirtualMachine> Virtualmachine => Set<VirtualMachine>();
public DbSet<Gebruiker> gebruikers=> Set<Gebruiker>();
public DbSet<Klant> klanten => Set<Klant>();
public DbSet<InterneKlant> interneKlanten => Set<InterneKlant>();
public DbSet<ExterneKlant> externeKlanten => Set<ExterneKlant>();
public DbSet<Backup> backups => Set<Backup>();
public DbSet<VMConnection> connections => Set<VMConnection>();
public DbSet<Project> projecten => Set<Project>();
public DbSet<VMContract> vMContracts=> Set<VMContract>();
public DbSet<FysiekeServer> fysiekeServers=> Set<FysiekeServer>();

public HerExamenDBContext(DbContextOptions<HerExamenDBContext> options) : base(options){ }
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    base.OnConfiguring(optionsBuilder);
    optionsBuilder.EnableDetailedErrors();
    optionsBuilder.EnableSensitiveDataLogging();

    
    }
protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
{
    // All decimals should have 2 digits after the comma
    configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
    // Max Length of a NVARCHAR that can be indexed
    configurationBuilder.Properties<string>().HaveMaxLength(4_000);
}
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
}


