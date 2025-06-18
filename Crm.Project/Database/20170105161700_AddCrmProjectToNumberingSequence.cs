namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20170105161700)]
	public class AddCrmProjectToNumberingSequence : Migration
	{
		public override void Up()
		{
			if ((int)Database.ExecuteScalar("SELECT COUNT(*) FROM [dbo].[NumberingSequence] WHERE SequenceName = 'CRM.Project'") == 0 && (int)Database.ExecuteScalar("SELECT COUNT(*) FROM [CRM].[Project]") == 0)
			{
				Database.ExecuteNonQuery(@"INSERT INTO [dbo].[NumberingSequence] ([SequenceName], [LastNumber], [Prefix], [Format], [Suffix], [MaxLow])	
										VALUES(
											'CRM.Project', 
											0,
											'PRJ-', 
											'000000',
											NULL, 
											32)");
			}
		}
	}
}