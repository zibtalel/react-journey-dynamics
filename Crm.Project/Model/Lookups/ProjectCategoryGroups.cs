namespace Crm.Project.Model.Lookups
{
	using Crm.Library.Globalization.Lookup;

	[Lookup("[LU].[ProjectCategoryGroups]", "ProjectCategoryGroupId")]
	public class ProjectCategoryGroups : EntityLookup<string>
	{
		public static readonly ProjectCategoryGroups Default = new() { Key = "100" };
	}
}
