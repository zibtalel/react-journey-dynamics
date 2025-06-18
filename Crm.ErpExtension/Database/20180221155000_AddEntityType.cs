namespace Crm.ErpExtension.Database
{
	using Crm.ErpExtension.Model;
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Helper;

	[Migration(20180221155000)]
	public class AddEntityType : Migration
	{
		public override void Up()
		{
			var helper = new UnicoreMigrationHelper(Database);
			if ((int)Database.ExecuteScalar($"SELECT COUNT(*) FROM [dbo].[EntityType] WHERE[Name] = 'Crm.ErpExtension.Model.{nameof(CreditNote)}'") == 0)
				helper.AddEntityTypeAndAuthDataColumnIfNeeded<CreditNote>("CRM", "ERPDocument");
			if ((int)Database.ExecuteScalar($"SELECT COUNT(*) FROM [dbo].[EntityType] WHERE[Name] = 'Crm.ErpExtension.Model.{nameof(DeliveryNote)}'") == 0)
				helper.AddEntityType<DeliveryNote>();
			if ((int)Database.ExecuteScalar($"SELECT COUNT(*) FROM [dbo].[EntityType] WHERE[Name] = 'Crm.ErpExtension.Model.{nameof(Invoice)}'") == 0)
				helper.AddEntityType<Invoice>();
			if ((int)Database.ExecuteScalar($"SELECT COUNT(*) FROM [dbo].[EntityType] WHERE[Name] = 'Crm.ErpExtension.Model.{nameof(MasterContract)}'") == 0)
				helper.AddEntityType<MasterContract>();
			if ((int)Database.ExecuteScalar($"SELECT COUNT(*) FROM [dbo].[EntityType] WHERE[Name] = 'Crm.ErpExtension.Model.{nameof(Quote)}'") == 0)
				helper.AddEntityType<Quote>();
			if ((int)Database.ExecuteScalar($"SELECT COUNT(*) FROM [dbo].[EntityType] WHERE[Name] = 'Crm.ErpExtension.Model.{nameof(SalesOrder)}'") == 0)
				helper.AddEntityType<SalesOrder>();
			if ((int)Database.ExecuteScalar($"SELECT COUNT(*) FROM [dbo].[EntityType] WHERE[Name] = 'Crm.ErpExtension.Model.{nameof(ErpTurnover)}'") == 0)
				helper.AddEntityTypeAndAuthDataColumnIfNeeded<ErpTurnover>("CRM", "Turnover");
		}
	}
}