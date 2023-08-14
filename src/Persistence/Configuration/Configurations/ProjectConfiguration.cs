using Domain.Projecten;
using Domain.VirtualMachines.BackUp;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration.Configurations;

internal class ProjectConfiguration : EntityConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        base.Configure(builder);
        builder.Property(p => p.Name).IsRequired();
        builder.HasMany(p => p.VirtualMachines);
    }
}
