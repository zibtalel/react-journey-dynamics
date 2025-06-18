namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20200610133200)]
	public class AddPotentialEntityTable : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[CRM].[Potential]"))
			{
				Database.ExecuteNonQuery(@"
				CREATE TABLE [CRM].[Potential](
				[ContactKey] [uniqueidentifier] NOT NULL,
				[StatusKey] [nvarchar](20) NULL,
				[CloseDate] [datetime] NULL,
				[StatusDate] [datetime] NULL,
				[PriorityKey] [nvarchar](255) NULL);

				ALTER TABLE [CRM].[Potential]  WITH CHECK ADD  CONSTRAINT [FK_Potential_Contact] FOREIGN KEY([ContactKey])
				REFERENCES [CRM].[Contact] ([ContactId])

				ALTER TABLE [CRM].[Potential] CHECK CONSTRAINT [FK_Potential_Contact]");
			}
		}
	}
}