namespace Aritter.Domain.Aggregates
{
	public class UserPasswordPolicy : Auditable
	{
		public int RequiredLength { get; set; }
		public bool RequireNonLetterOrDigit { get; set; }
		public bool RequireDigit { get; set; }
		public bool RequireLowercase { get; set; }
		public bool RequireUppercase { get; set; }

		public virtual UserPolicy UserPolicy { get; set; }
	}
}