namespace Crm.Project.Extensions
{
	using Crm.Library.BaseModel;
	using Crm.Library.Globalization.Lookup;
	using Crm.Model.Lookups;

	public class DocumentCategoryExtension : EntityExtension<DocumentCategory>
	{
		[LookupProperty(Shared = true)]
		public virtual bool SalesRelated { get; set; }
	}
}
