namespace Crm.Project.Model.Lookups
{
	using Crm.Library.Globalization.Lookup;

	[Lookup("[LU].[ProjectCategory]", "ProjectCategoryId")]
	public class ProjectCategory : EntityLookup<string>, ILookupWithColor, ILookupWithGroups
	{
		[LookupProperty(Shared = true)]
		public virtual string Color { get; set; }
		
		[LookupProperty(Shared = true, ReplaceEmptyString = true)]
		public virtual string Groups { get; set; }

		public ProjectCategory()
		{
			Color = "#AAAAAA";
		}
	}
}
