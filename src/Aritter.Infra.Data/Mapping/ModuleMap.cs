using Aritter.Domain.Security.Aggregates;
using Aritter.Infra.Data.Seedwork.Mapping;
using Aritter.Infra.Data.SeedWork.Extensions;

namespace Aritter.Infra.Data.Mapping
{
    internal sealed class ModuleMap : EntityMap<Module>
    {
        public ModuleMap()
        {
            Property(p => p.Name)
                .HasMaxLength(50)
                .HasUniqueIndex("UQ_Module");

            Property(p => p.Description)
                .HasMaxLength(255)
                .IsOptional();

            HasMany(p => p.Roles)
                .WithRequired(p => p.Module)
                .HasForeignKey(p => p.ModuleId);
        }
    }
}
