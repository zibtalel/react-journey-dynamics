namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20200612094300)]
	public class AddCrmPotentialToNumberingSequence : Migration
	{
		public override void Up()
		{
			if ((int)Database.ExecuteScalar("SELECT COUNT(*) FROM [dbo].[NumberingSequence] WHERE SequenceName = 'CRM.Potential'") == 0 && (int)Database.ExecuteScalar("SELECT COUNT(*) FROM [CRM].[Potential]") == 0)
			{
				Database.ExecuteNonQuery(@"INSERT INTO [dbo].[NumberingSequence] ([SequenceName], [LastNumber], [Prefix], [Format], [Suffix], [MaxLow])	
										VALUES(
											'CRM.Potential', 
											0,
											'POT-', 
											'000000',
											NULL, 
											NULL)");
			}
		}
	}
}