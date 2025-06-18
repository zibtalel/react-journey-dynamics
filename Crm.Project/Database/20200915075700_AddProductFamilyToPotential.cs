namespace Crm.Project.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20200915075700)]
	public class AddProductFamilyToPotential : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("CRM.Potential", "ProductFamilyKey"))
			{
				Database.AddColumn("CRM.Potential", "ProductFamilyKey", DbType.Guid, ColumnProperty.Null);
			}
			if (!Database.ColumnExists("CRM.Potential", "MasterProductFamilyKey"))
			{
				Database.AddColumn("CRM.Potential", "MasterProductFamilyKey", DbType.Guid, ColumnProperty.Null);
				Database.ExecuteNonQuery(@"
					ALTER TABLE [CRM].[Potential]  WITH CHECK ADD  CONSTRAINT [FK_Contact_PotentialMasterProductFamily] FOREIGN KEY([MasterProductFamilyKey])
					REFERENCES [CRM].[Contact] ([ContactId])");
			}
		}
		}
	}
