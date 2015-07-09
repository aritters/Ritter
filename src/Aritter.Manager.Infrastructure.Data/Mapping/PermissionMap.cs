using Aritter.Manager.Domain.Aggregates;
using Aritter.Manager.Infrastructure.Data.Extensions;

namespace Aritter.Manager.Infrastructure.Data.Mapping
{
	public class PermissionMap : EntityMap<Permission>
	{
		public PermissionMap()
		{
			Property(p => p.ResourceId)
				.HasUniqueIndex("UQ_Permission", 1)
				.IsRequired();

			Property(p => p.OperationId)
				.HasUniqueIndex("UQ_Permission", 2)
				.IsRequired();

			HasRequired(p => p.Operation)
				.WithMany(p => p.Permissions)
				.HasForeignKey(p => p.OperationId);

			HasRequired(p => p.Resource)
				.WithMany(p => p.Permissions)
				.HasForeignKey(p => p.ResourceId);
		}
	}
}
