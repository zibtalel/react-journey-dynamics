namespace Crm.ErpExtension.Model.Configuration
{
	using Crm.Library.EntityConfiguration;
	using Crm.Model;

	public class SalesOrderConfiguration : EntityConfiguration<SalesOrder>
	{
		public override void Initialize()
		{
			Property(x => x.Status, c => c.Filterable());
			
			Property(x => x.ContactKey, m => m.Filterable(f => f.Definition(new AutoCompleterFilterDefinition<Company>("CompanyAutocomplete", new { Plugin = "Main" }, "Main_Company", "Helper.Company.getDisplayName", x => x.Id, x => x.LegacyId, x => x.Name) { Caption = "Company" })));

			Property(x => x.OrderNo,
				c =>
				{
					c.Filterable(f => f.Caption("T_OrderNo"));
					c.Sortable(s => s.SortCaption("T_OrderNo"));
				});
			
			Property(x => x.OrderConfirmationNo, c => c.Filterable(f => f.Caption("T_OrderConfirmationNo")));

			Property(x => x.OrderConfirmationDate, c =>
			{
				c.Filterable(f => f.Definition(new DateFilterDefinition { AllowPastDates = true, AllowFutureDates = false }));
				c.Sortable();
			});
			
			Property(x => x.Description, c => c.Filterable());
			
			Property(x => x.Commission, c => c.Filterable(f => f.Caption("T_Reference")));
			
			Property(x => x.OrderType, c => c.Filterable());
			
			Property(x => x.Total, c =>
			{
				c.Filterable(f =>
				{
					f.Definition(new ScaleFilterDefinition(10000, 100000, 10000, Operator.GreaterThan));
					f.Caption("T_GrossAmount");
				});
				c.Sortable();
			});

			Property(x => x.TotalWoTaxes, c =>
			{
				c.Filterable(f =>
				{
					f.Definition(new ScaleFilterDefinition(10000, 100000, 10000, Operator.GreaterThan));
					f.Caption("T_NetAmount");
				});
				c.Sortable(s =>
				{
					s.SortCaption("T_NetAmount");
				});
			});
			
		}
		public SalesOrderConfiguration(IEntityConfigurationHolder<SalesOrder> entityConfigurationHolder)
			: base(entityConfigurationHolder)
		{
		}
	}
}
