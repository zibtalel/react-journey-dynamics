namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20200616114600)]
	public class AddTableCrmPotentialContactRelationship : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[CRM].[PotentialContactRelationship]"))
			{
				Database.ExecuteNonQuery(@"
						IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PotentialContactRelationship' AND xtype='U')
							CREATE TABLE [CRM].[PotentialContactRelationship](
							[RelationshipType] [nvarchar](50) NOT NULL,
							[CreateDate] [datetime] NOT NULL,
							[ModifyDate] [datetime] NOT NULL,
							[CreateUser] [nvarchar](256) NOT NULL,
							[ModifyUser] [nvarchar](256) NOT NULL,
							[Information] [nvarchar](max) NULL,
							[IsActive] [bit] NOT NULL,
							[PotentialKey] [uniqueidentifier] NOT NULL,
							[ContactKey] [uniqueidentifier] NOT NULL,
							[PotentialContactRelationshipId] [uniqueidentifier] NOT NULL,
						 CONSTRAINT [PK_PotentialContactRelationship] PRIMARY KEY CLUSTERED 
						(
							[PotentialContactRelationshipId] ASC
						)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
						) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

								ALTER TABLE [CRM].[PotentialContactRelationship] ADD  CONSTRAINT [DF__PotentialCo__IsAct]  DEFAULT ((1)) FOR [IsActive]
								ALTER TABLE [CRM].[PotentialContactRelationship] ADD  CONSTRAINT [DF_PotentialContactRelationship_PotentialContactRelationshipId]  DEFAULT (newsequentialid()) FOR [PotentialContactRelationshipId]
								ALTER TABLE [CRM].[PotentialContactRelationship]  WITH CHECK ADD  CONSTRAINT [FK_PotentialContactRelationship_Contact] FOREIGN KEY([ContactKey])
								REFERENCES [CRM].[Contact] ([ContactId])
								ALTER TABLE [CRM].[PotentialContactRelationship] CHECK CONSTRAINT [FK_PotentialContactRelationship_Contact]
								ALTER TABLE [CRM].[PotentialContactRelationship]  WITH CHECK ADD  CONSTRAINT [FK_PotentialContactRelationship_Potential] FOREIGN KEY([PotentialKey])
								REFERENCES [CRM].[Contact] ([ContactId])
								ALTER TABLE [CRM].[PotentialContactRelationship] CHECK CONSTRAINT [FK_PotentialContactRelationship_Potential]
            ");
			}
		}
	}
}