using Aritter.Domain.Seedwork.Aggregates;
using System.Collections.Generic;

namespace Aritter.Domain.Security.Aggregates
{
	public class Resource : Entity
	{
		public Resource()
		{
			Permissions = new HashSet<Permission>();
		}

		public string Name { get; set; }
		public string Description { get; set; }
		public int ModuleId { get; set; }
		public virtual Module Module { get; set; }
		public virtual ICollection<Permission> Permissions { get; set; }
	}
}