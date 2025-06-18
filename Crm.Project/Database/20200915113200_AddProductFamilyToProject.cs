namespace Crm.Project.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20200915113200)]
	public class AddProductFamilyToProject : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("CRM.Project", "ProductFamilyKey"))
			{
				Database.AddColumn("CRM.Project", "ProductFamilyKey", DbType.Guid, ColumnProperty.Null);
				Database.ExecuteNonQuery(@"
					ALTER TABLE [CRM].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Contact_ProductFamily] FOREIGN KEY([ProductFamilyKey])
					REFERENCES [CRM].[Contact] ([ContactId])");
			}
			if (!Database.ColumnExists("CRM.Project", "MasterProductFamilyKey"))
			{
				Database.AddColumn("CRM.Project", "MasterProductFamilyKey", DbType.Guid, ColumnProperty.Null);
				Database.ExecuteNonQuery(@"
					ALTER TABLE [CRM].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Contact_MasterProductFamily] FOREIGN KEY([MasterProductFamilyKey])
					REFERENCES [CRM].[Contact] ([ContactId])");
			}
		}
	}
}