using Aritter.Domain.Aggregates;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aritter.Infrastructure.Data.Mapping
{
	public class UserPolicyMap : EntityMap<UserPolicy>
	{
		public UserPolicyMap()
		{
			Property(p => p.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			HasRequired(p => p.UserPasswordPolicy)
				.WithRequiredPrincipal(p => p.UserPolicy);
		}
	}
}
