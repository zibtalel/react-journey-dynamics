namespace Sms.Einsatzplanung.Connector.Database
{
	using System.Text;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20120101100000)]
	public class CreateRPLIX_LegacyId_Type : Migration
	{
		public override void Up()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("IF NOT EXISTS (SELECT * FROM sys.indexes ");
			stringBuilder.AppendLine("WHERE object_id = OBJECT_ID(N'[RPL].[Dispatch]') ");
			stringBuilder.AppendLine("AND name = N'IX_LegacyId_Type')");
			stringBuilder.AppendLine("BEGIN");
			stringBuilder.AppendLine("CREATE NONCLUSTERED INDEX [IX_LegacyId_Type] ON [RPL].[Dispatch] ");
			stringBuilder.AppendLine("(");
			stringBuilder.AppendLine("[LegacyId] ASC,");
			stringBuilder.AppendLine("[Type] ASC");
			stringBuilder.AppendLine(")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]");
			stringBuilder.AppendLine("END");

			Database.ExecuteNonQuery(stringBuilder.ToString());
		}
		public override void Down()
		{

		}
	}
}
