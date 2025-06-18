namespace Crm.Project.Database {
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20201126102300)]
	public class AddDocumentEntryEntity : Migration {
		public override void Up() {
			if (!Database.TableExists("CRM.DocumentEntry")) {
				Database.ExecuteNonQuery(
					@"
				CREATE TABLE [CRM].[DocumentEntry](
					[Id] [uniqueidentifier] PRIMARY KEY NOT NULL,
					[IsActive] [bit] NOT NULL,
					[CreateDate] [datetime] NOT NULL,
					[ModifyDate] [datetime] NOT NULL,
					[CreateUser] [nvarchar](60) NOT NULL,
					[ModifyUser] [nvarchar](60) NOT NULL,
					[ContactKey] [uniqueidentifier] NOT NULL,
					[PersonKey] [uniqueidentifier] NOT NULL,
					[DocumentKey] [uniqueidentifier] NOT NULL,
					[SendDate] [datetime] NOT NULL,
					[FeedbackReceived] [bit] NOT NULL
				) ON [PRIMARY]
				ALTER TABLE [CRM].[DocumentEntry] ADD  DEFAULT ((1)) FOR [IsActive]
				ALTER TABLE [CRM].[DocumentEntry] ADD  DEFAULT (getutcdate()) FOR [CreateDate]
				ALTER TABLE [CRM].[DocumentEntry] ADD  DEFAULT (getutcdate()) FOR [ModifyDate]
				ALTER TABLE [CRM].[DocumentEntry] ADD  DEFAULT (getutcdate()) FOR [SendDate]
				ALTER TABLE [CRM].[DocumentEntry] ADD  DEFAULT ((0)) FOR [FeedbackReceived]
				ALTER TABLE [CRM].[DocumentEntry]  WITH CHECK ADD FOREIGN KEY([ContactKey])
				REFERENCES [CRM].[Contact] ([ContactId])
				ALTER TABLE [CRM].[DocumentEntry]  WITH CHECK ADD FOREIGN KEY([PersonKey])
				REFERENCES [CRM].[Contact] ([ContactId])
				");
			}
		}
	}
}