namespace Crm.Project.Model.Lookups
{
	using Crm.Library.Globalization.Lookup;

	[Lookup("[LU].[ProjectStatus]", "ProjectStatusId")]
	public class ProjectStatus : EntityLookup<string>, ILookupWithColor, ILookupWithGroups
	{
		public const string OpenGroupKey = "Open";
		public const string WonGroupKey = "Won";
		public const string LostGroupKey = "Lost";

		[LookupProperty(Shared = true)]
		public virtual string Color { get; set; }
		
		[LookupProperty(Shared = true)]
		public virtual string Groups { get; set; }
		
		public ProjectStatus()
		{
			Color = "#AAAAAA";
		}

		public static readonly string OpenKey = "100";
		public static readonly string WonKey = "101";
		public static readonly string LostKey = "102";
	}
}
