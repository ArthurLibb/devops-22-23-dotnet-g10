using Domain.Server;
using Domain.VirtualMachines.Contract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration.Configurations;

internal class ContractConfiguration : EntityConfiguration<VMContract>
{

    public void Configure(EntityTypeBuilder<VMContract> builder)
    {
        base.Configure(builder);
        builder.Property(c => c.StartDate).IsRequired();
        builder.Property(c => c.EndDate).IsRequired();  

    }
}
