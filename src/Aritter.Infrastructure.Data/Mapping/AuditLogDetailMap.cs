using Aritter.Domain.Aggregates;

namespace Aritter.Infrastructure.Data.Mapping
{
	public class AuditLogDetailMap : EntityMap<AuditLogDetail>
	{
		public AuditLogDetailMap()
		{
			Property(p => p.FieldName)
				.IsMaxLength();

			Property(p => p.OldValue)
				.IsMaxLength()
				.IsOptional();

			Property(p => p.NewValue)
				.IsMaxLength()
				.IsOptional();

			HasRequired(p => p.AuditLog)
				.WithMany(p => p.AuditLogDetails)
				.HasForeignKey(p => p.AuditLogId);
		}
	}
}
