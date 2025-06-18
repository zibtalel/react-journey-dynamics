namespace Crm.MarketInsight.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20200915150900)]
	public class AddTableCrmMarketInsightContactRelationship : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[CRM].[MarketInsightContactRelationship]"))
			{
				Database.ExecuteNonQuery(@"
						IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='MarketInsightContactRelationship' AND xtype='U')
							CREATE TABLE [CRM].[MarketInsightContactRelationship](
	                        [RelationshipType] [nvarchar](50) NOT NULL,
	                        [CreateDate] [datetime] NOT NULL,
	                        [ModifyDate] [datetime] NOT NULL,
	                        [CreateUser] [nvarchar](256) NOT NULL,
	                        [ModifyUser] [nvarchar](256) NOT NULL,
	                        [Information] [nvarchar](max) NULL,
	                        [IsActive] [bit] NOT NULL,
	                        [MarketInsightKey] [uniqueidentifier] NOT NULL,
	                        [ContactKey] [uniqueidentifier] NOT NULL,
	                        [MarketInsightContactRelationshipId] [uniqueidentifier] NOT NULL,
                            CONSTRAINT [PK_MarketInsightContactRelationship] PRIMARY KEY CLUSTERED 
                            (
	                        [MarketInsightContactRelationshipId] ASC
                                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
            ");
			}
		}
	}
}