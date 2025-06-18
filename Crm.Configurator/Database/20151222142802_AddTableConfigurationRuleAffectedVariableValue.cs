namespace Crm.Configurator.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20151222142802)]
	public class AddTableConfigurationRuleAffectedVariableValue : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[CRM].[ConfigurationRuleAffectedVariableValue]"))
			{
				Database.ExecuteNonQuery(@"CREATE TABLE [CRM].[ConfigurationRuleAffectedVariableValue] (
													[ConfigurationRuleId] [uniqueidentifier] NOT NULL,
													[VariableValueId] [uniqueidentifier] NOT NULL,
													CONSTRAINT [PK_ConfigurationRuleAffectedVariableValue] PRIMARY KEY CLUSTERED ([ConfigurationRuleId] ASC, [VariableValueId] ASC) ON [PRIMARY]
													) ON [PRIMARY]");
				Database.ExecuteNonQuery(@"ALTER TABLE [CRM].[ConfigurationRuleAffectedVariableValue] WITH CHECK ADD CONSTRAINT [FK_ConfigurationRuleAffectedVariableValue_ConfigurationRuleId] FOREIGN KEY ([ConfigurationRuleId]) REFERENCES [CRM].[ConfigurationRule] ([ConfigurationRuleId])");
				Database.ExecuteNonQuery(@"ALTER TABLE [CRM].[ConfigurationRuleAffectedVariableValue] CHECK CONSTRAINT [FK_ConfigurationRuleAffectedVariableValue_ConfigurationRuleId]");
				Database.ExecuteNonQuery(@"ALTER TABLE [CRM].[ConfigurationRuleAffectedVariableValue] WITH CHECK ADD CONSTRAINT [FK_ConfigurationRuleAffectedVariableValue_VariableValueId] FOREIGN KEY ([VariableValueId]) REFERENCES [CRM].[Contact] ([ContactId])");
				Database.ExecuteNonQuery(@"ALTER TABLE [CRM].[ConfigurationRuleAffectedVariableValue] CHECK CONSTRAINT [FK_ConfigurationRuleAffectedVariableValue_VariableValueId]");
			}
		}
	}
}