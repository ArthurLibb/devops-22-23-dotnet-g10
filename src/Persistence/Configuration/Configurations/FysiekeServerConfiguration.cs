using Domain.Server;
using Domain.VirtualMachines.VirtualMachine;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration.Configurations;

internal class FysiekeServerConfiguration : EntityConfiguration<FysiekeServer>
{
    public void Configure(EntityTypeBuilder<FysiekeServer> builder)
    {
        base.Configure(builder);
        builder.Property(f => f.Naam).IsRequired();
        builder.HasMany(f => f.VirtualMachines);
        builder.Property(f => f.ServerAddress).IsRequired();
    }
}
