namespace Crm.ProjectOrders.Model.Configuration
{
	using Crm.Library.EntityConfiguration;
	using Crm.Order.Model;
	using Crm.Project.Model;

	public class OfferExtensionConfiguration : EntityExtensionConfiguration<OfferExtension, Offer>
	{
		public override void Initialize()
		{
			Property(x => x.ProjectId, m => m.Filterable(f => f.Definition(new AutoCompleterFilterDefinition<Project>("ProjectAutocomplete", new { Plugin = "Crm.Project" }, "CrmProject_Project", "Helper.Project.getName", x => x.Id, x => x.ProjectNo, x => x.Name) { Caption = "Project" })));
		}
		public OfferExtensionConfiguration(IEntityConfigurationHolder<Offer> entityConfigurationHolder)
			: base(entityConfigurationHolder)
		{
		}
	}
}