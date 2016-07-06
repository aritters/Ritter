using Aritter.Domain.SecurityModule.Aggregates.UserAgg;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aritter.Infra.Data.Configuration
{
    internal sealed class UserCredentialBuilder : EntityBuilder<UserCredential>
    {
        public override void Build(EntityTypeBuilder<UserCredential> builder)
        {
            base.Build(builder);

            builder.Property(p => p.PasswordHash)
                .HasMaxLength(100);
        }
    }
}