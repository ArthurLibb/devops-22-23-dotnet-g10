using Domain.VirtualMachines.VirtualMachine;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Domain;
using Domain.Users;
using Domain.VirtualMachines.BackUp;
using Domain.Projecten;
using Domain.VirtualMachines.Contract;
using Domain.Server;

namespace Persistence.Configuration;


public class HerExamenDBContext : DbContext
{
public DbSet<VirtualMachine> Virtualmachines => Set<VirtualMachine>();
public DbSet<Gebruiker> gebruikers=> Set<Gebruiker>();
public DbSet<Klant> klanten => Set<Klant>();
public DbSet<InterneKlant> interneKlanten => Set<InterneKlant>();
public DbSet<ExterneKlant> externeKlanten => Set<ExterneKlant>();
public DbSet<Backup> backups => Set<Backup>();
public DbSet<VMConnection> connections => Set<VMConnection>();
public DbSet<Project> projecten => Set<Project>();
public DbSet<VMContract> vMContracts=> Set<VMContract>();
public DbSet<FysiekeServer> fysiekeServers=> Set<FysiekeServer>();
public DbSet<Administrator> admins => Set<Administrator>();

public HerExamenDBContext(DbContextOptions<HerExamenDBContext> options) : base(options){ }
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    base.OnConfiguring(optionsBuilder);
    optionsBuilder.EnableDetailedErrors();
    optionsBuilder.EnableSensitiveDataLogging();
}
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
}


