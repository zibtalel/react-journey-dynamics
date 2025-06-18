namespace Crm.Project.Model.Lookups
{
	using Crm.Library.Globalization.Lookup;

	[Lookup("[LU].[PotentialPriority]", "PotentialPriorityId")]
	public class PotentialPriority : EntityLookup<string>, ILookupWithColor
	{
		[LookupProperty(Shared = true)]
		public virtual string Color { get; set; }

		public PotentialPriority()
		{
			Color = "#AAAAAA";
		}

		public static readonly string FirstPrioKey = "prio1";
		public static readonly string SecondPrioKey = "prio2";
	}
}
