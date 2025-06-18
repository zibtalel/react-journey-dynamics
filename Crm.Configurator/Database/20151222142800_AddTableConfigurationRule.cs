namespace Crm.Configurator.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20151222142800)]
	public class AddTableConfigurationRule : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[CRM].[ConfigurationRule]"))
			{
				Database.ExecuteNonQuery(@"CREATE TABLE [CRM].[ConfigurationRule] (
												[ConfigurationRuleId] [uniqueidentifier] NOT NULL,
												[ConfigurationBaseId] [uniqueidentifier] NOT NULL,
												[Validation] [nvarchar](50) NOT NULL,
												[CreateDate] [datetime] NOT NULL,
												[ModifyDate] [datetime] NOT NULL,
												[CreateUser] [nvarchar](256) NOT NULL,
												[ModifyUser] [nvarchar](256) NOT NULL,
												[IsActive] [bit] NOT NULL,
												[TenantKey] [int] NULL,
												CONSTRAINT [PK_ConfigurationRule] PRIMARY KEY CLUSTERED ([ConfigurationRuleId] ASC) ON [PRIMARY]
												) ON [PRIMARY]");
				Database.ExecuteNonQuery(@"ALTER TABLE [CRM].[ConfigurationRule] ADD CONSTRAINT [DF_ConfigurationRule_ConfigurationRuleId]  DEFAULT (newsequentialid()) FOR [ConfigurationRuleId]");
				Database.ExecuteNonQuery(@"ALTER TABLE [CRM].[ConfigurationRule] ADD CONSTRAINT [DF_ConfigurationRule_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate]");
				Database.ExecuteNonQuery(@"ALTER TABLE [CRM].[ConfigurationRule] ADD CONSTRAINT [DF_ConfigurationRule_ModifyDate] DEFAULT (getutcdate()) FOR [ModifyDate]");
				Database.ExecuteNonQuery(@"ALTER TABLE [CRM].[ConfigurationRule] ADD CONSTRAINT [DF_ConfigurationRule_IsActive] DEFAULT ((1)) FOR [IsActive]");
				Database.ExecuteNonQuery(@"ALTER TABLE [CRM].[ConfigurationRule] WITH CHECK ADD CONSTRAINT [FK_ConfigurationRule_ConfigurationBaseId] FOREIGN KEY([ConfigurationBaseId]) REFERENCES [CRM].[Contact] ([ContactId])");
				Database.ExecuteNonQuery(@"ALTER TABLE [CRM].[ConfigurationRule] CHECK CONSTRAINT [FK_ConfigurationRule_ConfigurationBaseId]");
			}
		}
	}
}