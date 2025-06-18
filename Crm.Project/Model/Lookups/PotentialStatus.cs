namespace Crm.Project.Model.Lookups
{
	using Crm.Library.Globalization.Lookup;

	[Lookup("[LU].[PotentialStatus]", "PotentialStatusId")]
	public class PotentialStatus : EntityLookup<string>, ILookupWithColor
	{
		[LookupProperty(Shared = true)]
		public virtual string Color { get; set; }

		public PotentialStatus()
		{
			Color = "#AAAAAA";
		}

		public static readonly string NewKey = "new";
		public static readonly string ClosedKey = "closed";
		public static readonly string UnqualifiedKey = "unqualified";
	}
}
