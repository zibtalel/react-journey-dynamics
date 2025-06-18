namespace Crm.ErpExtension.Model.Configuration
{
	using Crm.Library.EntityConfiguration;
	using Crm.Model;

	public class DeliveryNotePositionConfiguration : EntityConfiguration<DeliveryNotePosition>
	{
		public override void Initialize()
		{
			NestedProperty(x => x.Parent.DeliveryNoteDate, c =>
			{
				c.Filterable(f => f.Definition(new DateFilterDefinition { AllowPastDates = true, AllowFutureDates = false }));
				c.Sortable();
			});

			NestedProperty(x => x.Parent.ContactKey, m =>
			{
				m.Filterable(f => f.Definition(new AutoCompleterFilterDefinition<Company>("CompanyAutocomplete", new { Plugin = "Main" }, "Main_Company", "Helper.Company.getDisplayName", x => x.Id, x => x.LegacyId, x => x.Name) { Caption = "Company" }));
				m.Sortable(s => s.SortCaption("Company"));
			});

			NestedProperty(x => x.Parent.OrderNo, c =>
			{
				c.Filterable(f => f.Caption("T_OrderNo"));
				c.Sortable(s => s.SortCaption("T_OrderNo"));
			});

			NestedProperty(x => x.Parent.DeliveryNoteNo, c =>
			{
				c.Filterable(f => f.Caption("T_DeliveryNoteNo"));
				c.Sortable(s => s.SortCaption("T_DeliveryNoteNo"));
			});

			Property(x => x.ItemNo, m => m.Filterable());
			Property(x => x.Description, m => m.Filterable());
			NestedProperty(x => x.Parent.Commission, m => m.Filterable(f => f.Caption("T_Reference")));
			Property(x => x.PositionNo, m => m.Filterable());

			Property(x => x.Quantity, m =>
			{
				m.Filterable(f => f.Definition(new ScaleFilterDefinition(50, 1000, 50, Operator.GreaterThan)));
				m.Sortable();
			});

			Property(x => x.RemainingQuantity, m =>
			{
				m.Filterable(f =>
				{
					f.Definition(new ScaleFilterDefinition(50, 1000, 50, Operator.GreaterThan));
					f.Caption("T_RemainingQuantity");
				});
				m.Sortable(s => s.SortCaption("T_RemainingQuantity"));
			});
		}
		public DeliveryNotePositionConfiguration(IEntityConfigurationHolder<DeliveryNotePosition> entityConfigurationHolder)
			: base(entityConfigurationHolder)
		{
		}
	}
}