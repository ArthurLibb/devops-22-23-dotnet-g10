using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualMachine = Domain.VirtualMachines.VirtualMachine.VirtualMachine;

namespace Persistence.Configuration.Virtualmachines;

internal class Virtualmachineconfiguration : EntityConfiguration<VirtualMachine>
{

    public void Configure(EntityTypeBuilder<VirtualMachine> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.OperatingSystem).IsRequired();
        builder.Property(x => x.Mode).IsRequired();
        builder.OwnsOne(x => x.Hardware, h =>
        {
            h.Property(ha => ha.Memory).HasColumnName("memory").IsRequired();
            h.Property(ha => ha.Amount_vCPU).HasColumnName("amount_vCPU").IsRequired();
            h.Property(ha => ha.Storage).HasColumnName("storage").IsRequired();
        });
        builder.HasOne(x => x.Statistics);
    }
}
