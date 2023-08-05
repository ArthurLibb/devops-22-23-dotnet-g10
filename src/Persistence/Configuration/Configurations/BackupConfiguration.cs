using Domain.Server;
using Domain.VirtualMachines.BackUp;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration.Configurations;

internal class BackupConfiguration : EntityConfiguration<Backup>
{
    public void Configure(EntityTypeBuilder<Backup> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.Type);
        builder.Property(b => b.IsEnabled);
        builder.Property(b => b.LastBackup);
    }
}
