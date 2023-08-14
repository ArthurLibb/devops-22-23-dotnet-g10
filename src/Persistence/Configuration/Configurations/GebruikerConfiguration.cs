using Domain;
using Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration.Configurations;

internal class GebruikerConfiguration : EntityConfiguration<Gebruiker>
{
    public void Configure(EntityTypeBuilder<Gebruiker> builder)
    {
        base.Configure(builder);
        builder.HasDiscriminator<string>("user_type")
            .HasValue<Administrator>("admin")
            .HasValue<ExterneKlant>("extern klant")
            .HasValue<InterneKlant>("intern klant");
    }
}
