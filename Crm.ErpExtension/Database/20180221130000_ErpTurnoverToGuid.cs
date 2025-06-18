namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20180221130000)]
	public class ErpTurnoverToGuid : Migration
	{
		public override void Up()
		{
			if (Database.GetColumnDataType("CRM", "Turnover", "TurnoverId") == "int")
			{
				Database.ExecuteNonQuery(@"
					DECLARE @key NVARCHAR(MAX) = (
						SELECT [name] FROM sys.key_constraints
						WHERE parent_object_id = object_id('CRM.Turnover')
							AND [type] = 'PK')

					DECLARE @sql NVARCHAR(MAX) = 'ALTER TABLE CRM.Turnover DROP CONSTRAINT ' + @key
					EXEC sp_executesql @sql
				");
				Database.RemoveColumn("CRM.Turnover", "TurnoverId");
				Database.ExecuteNonQuery(@"
					ALTER TABLE CRM.Turnover
					ADD TurnoverId UNIQUEIDENTIFIER
					CONSTRAINT DF_Turnover_TurnoverId DEFAULT(NEWSEQUENTIALID())
					CONSTRAINT PK_Turnover PRIMARY KEY");
				Database.ExecuteNonQuery(@"
					UPDATE CRM.Turnover
					SET ModifyDate = GETUTCDATE()
						,ModifyUser = 'Migration_20180221130000'");
			}
		}
	}
}