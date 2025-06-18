namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20231207174500)]
	public class IX_DispatchOrderKey_Type_IsActive : Migration
	{
		public override void Up()
		{
			if (Database.IndexExists("[RPL].[Dispatch]", "IX_DispatchOrderKey_Type_IsActive"))
			{
				Database.ExecuteNonQuery("DROP INDEX [IX_DispatchOrderKey_Type_IsActive] ON [RPL].[Dispatch]");
			}
			Database.ExecuteNonQuery(@"
			CREATE NONCLUSTERED INDEX [IX_DispatchOrderKey_Type_IsActive] ON [RPL].[Dispatch]
			(
				[DispatchOrderKey] ASC,
				[Type] ASC,
				[IsActive] ASC
			)");

		}
	}
}