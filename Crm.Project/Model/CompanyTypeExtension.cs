namespace Crm.Project.Model
{
	using Crm.Library.BaseModel;
	using Crm.Library.Globalization.Lookup;
	using Crm.Model.Lookups;

	public class CompanyTypeExtension : EntityExtension<CompanyType>
	{
		[LookupProperty(Shared = true)]
		public bool Competitor { get; set; }
	}
}