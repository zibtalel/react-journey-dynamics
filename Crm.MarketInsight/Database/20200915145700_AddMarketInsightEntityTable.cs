namespace Crm.MarketInsight.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	[Migration(20200915145700)]
	public class AddMarketInsightEntityTable : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@"
						IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='MarketInsight' AND xtype='U')
							CREATE TABLE [CRM].[MarketInsight] (
								ContactKey uniqueidentifier NOT NULL,
								ProductFamilyKey uniqueidentifier NOT NULL,
								ReferenceKey nvarchar(100),
								StatusKey nvarchar(100) NOT NULL)

						IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'FK_ProductFamilyKey_Contact' and xtype = 'F')
							ALTER TABLE [CRM].[MarketInsight] WITH CHECK ADD  CONSTRAINT [FK_ProductFamilyKey_Contact] FOREIGN KEY([ProductFamilyKey])
							REFERENCES [CRM].[Contact] ([ContactId])
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'FK_MarketInsight_Contact' and xtype = 'F')
							ALTER TABLE [CRM].[MarketInsight] WITH CHECK ADD  CONSTRAINT [FK_MarketInsight_Contact] FOREIGN KEY([ContactKey])
							REFERENCES [CRM].[Contact] ([ContactId])
            ");
		}
	}
}