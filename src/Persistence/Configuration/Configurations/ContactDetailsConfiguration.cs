using Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration.Configurations;

internal class ContactDetailsConfiguration : EntityConfiguration<ContactDetails>
{
    public override void Configure(EntityTypeBuilder<ContactDetails> builder)
    {
        base.Configure(builder);
        builder.Property(c => c.FirstName).IsRequired();
        builder.Property(c => c.LastName).IsRequired();
        builder.Property(c => c.Email).IsRequired();
        builder.Property(c => c.PhoneNumber).IsRequired();

    }
}
