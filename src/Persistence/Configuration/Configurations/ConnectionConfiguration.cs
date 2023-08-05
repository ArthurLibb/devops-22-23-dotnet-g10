using Domain.Server;
using Domain.VirtualMachines.VirtualMachine;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration.Configurations;

internal class ConnectionConfiguration : EntityConfiguration<VMConnection>
{
    public void Configure(EntityTypeBuilder<VMConnection> builder)
    {
        base.Configure(builder);
        builder.Property(c => c.Username).IsRequired();
        builder.Property(c => c.Hostname).IsRequired();
        builder.Property(c => c.Password).IsRequired();
        builder.Property(c => c.FQDN).IsRequired();
    }
}
