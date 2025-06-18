namespace Crm.Configurator.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20151222142801)]
	public class AddTableConfigurationRuleVariableValue : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[CRM].[ConfigurationRuleVariableValue]"))
			{
				Database.ExecuteNonQuery(@"CREATE TABLE [CRM].[ConfigurationRuleVariableValue] (
													[ConfigurationRuleId] [uniqueidentifier] NOT NULL,
													[VariableValueId] [uniqueidentifier] NOT NULL,
													CONSTRAINT [PK_ConfigurationRuleVariableValue] PRIMARY KEY CLUSTERED ([ConfigurationRuleId] ASC, [VariableValueId] ASC) ON [PRIMARY]
													) ON [PRIMARY]");
				Database.ExecuteNonQuery(@"ALTER TABLE [CRM].[ConfigurationRuleVariableValue] WITH CHECK ADD CONSTRAINT [FK_ConfigurationRuleVariableValue_ConfigurationRuleId] FOREIGN KEY ([ConfigurationRuleId]) REFERENCES [CRM].[ConfigurationRule] ([ConfigurationRuleId])");
				Database.ExecuteNonQuery(@"ALTER TABLE [CRM].[ConfigurationRuleVariableValue] CHECK CONSTRAINT [FK_ConfigurationRuleVariableValue_ConfigurationRuleId]");
				Database.ExecuteNonQuery(@"ALTER TABLE [CRM].[ConfigurationRuleVariableValue] WITH CHECK ADD CONSTRAINT [FK_ConfigurationRuleVariableValue_VariableValueId] FOREIGN KEY ([VariableValueId]) REFERENCES [CRM].[Contact] ([ContactId])");
				Database.ExecuteNonQuery(@"ALTER TABLE [CRM].[ConfigurationRuleVariableValue] CHECK CONSTRAINT [FK_ConfigurationRuleVariableValue_VariableValueId]");
			}
		}
	}
}