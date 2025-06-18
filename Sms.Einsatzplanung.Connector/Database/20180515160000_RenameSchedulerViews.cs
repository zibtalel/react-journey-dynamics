namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20180515160000)]
	public class RenameSchedulerViews : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@"
				IF EXISTS (SELECT * FROM sys.objects WHERE [type] = 'V' AND [schema_id] = SCHEMA_ID('RPL') AND [name] = 'Service')
				BEGIN
					EXEC SP_RENAME 'RPL.Service', 'OldService_20180515160000'
				END");
			Database.ExecuteNonQuery(@"
				IF EXISTS (SELECT * FROM sys.objects WHERE [type] = 'V' AND [schema_id] = SCHEMA_ID('RPL') AND [name] = 'Person')
				BEGIN
					EXEC SP_RENAME 'RPL.Person', 'OldPerson_20180515160000'
				END");
			Database.ExecuteNonQuery(@"
				IF EXISTS (SELECT * FROM sys.objects WHERE [type] = 'V' AND [schema_id] = SCHEMA_ID('RPL') AND [name] = 'Resource')
				BEGIN
					EXEC SP_RENAME 'RPL.Resource', 'OldResource_20180515160000'
				END");
			Database.ExecuteNonQuery(@"
				IF EXISTS (SELECT * FROM sys.objects WHERE [type] = 'V' AND [schema_id] = SCHEMA_ID('RPL') AND [name] = 'Skills')
				BEGIN
					EXEC SP_RENAME 'RPL.Skills', 'OldSkills_20180515160000'
				END");
			Database.ExecuteNonQuery(@"
				IF EXISTS (SELECT * FROM sys.objects WHERE [type] = 'V' AND [schema_id] = SCHEMA_ID('RPL') AND [name] = 'Users')
				BEGIN
					EXEC SP_RENAME 'RPL.Users', 'OldUsers_20180515160000'
				END");
		}
	}
}