namespace Crm.Configurator.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20151208102400)]
	public class AddVariableIdToOrderItem : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("[CRM].[OrderItem]", "VariableId"))
			{
				Database.ExecuteNonQuery("ALTER TABLE [CRM].[OrderItem] ADD [VariableId] uniqueidentifier NULL");
				Database.ExecuteNonQuery("ALTER TABLE [CRM].[OrderItem] WITH CHECK ADD CONSTRAINT [FK_OrderItem_VariableId] FOREIGN KEY ([VariableId]) REFERENCES [CRM].[Contact] ([ContactId])");
			}
		}
	}
}