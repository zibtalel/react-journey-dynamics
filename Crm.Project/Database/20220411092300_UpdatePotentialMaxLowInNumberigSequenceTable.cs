namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20220411092300)]
	public class UpdatePotentialMaxLowInNumberigSequenceTable : Migration
	{
		public override void Up()
		{
			if (Database.TableExists("[dbo].[NumberingSequence]"))
			{
				Database.ExecuteNonQuery("Update dbo.NumberingSequence Set MaxLow = 32 Where SequenceName = 'CRM.Potential'");
			}
		}
	}
}