namespace Sms.Einsatzplanung.Team.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20170608190000)]
	public class CreateTeamDispatchUser : Migration
	{
		public override void Up()
		{
			if (Database.TableExists("[SMS].[TeamDispatchUser]") == false)
			{
				Database.ExecuteNonQuery(@"
					CREATE TABLE [SMS].[TeamDispatchUser] (
						[TeamDispatchUsersId] uniqueidentifier NOT NULL,
						[IsActive] bit NOT NULL,
						[CreateDate] datetime NOT NULL,
						[ModifyDate] datetime NOT NULL,
						[CreateUser] nvarchar(50) NOT NULL,
						[ModifyUser] nvarchar(50) NOT NULL,
						[TenantKey] int NULL,
						[DispatchId] uniqueidentifier NOT NULL,
						[Username] nvarchar(256) NOT NULL,
						[IsNonTeamMember] bit NOT NULL,	
						CONSTRAINT [PK_TeamDispatchUsers] PRIMARY KEY ([TeamDispatchUsersId])
					)
				");
				Database.ExecuteNonQuery(@"
					ALTER TABLE [SMS].[TeamDispatchUser] ADD CONSTRAINT [DF_TeamDispatchUsersId] DEFAULT (newsequentialid()) FOR [TeamDispatchUsersId]
				");
				Database.ExecuteNonQuery(@"
					ALTER TABLE [SMS].[TeamDispatchUser] WITH CHECK ADD CONSTRAINT [FK_TeamDispatchUser_Username] FOREIGN KEY([Username])
					REFERENCES [CRM].[User] ([Username])
				");
				Database.ExecuteNonQuery(@"
					ALTER TABLE [SMS].[TeamDispatchUser] WITH CHECK ADD CONSTRAINT [FK_TeamDispatchUser_DispatchId] FOREIGN KEY([DispatchId])
					REFERENCES [SMS].[ServiceOrderDispatch] ([DispatchId])
				");
			}
		}
	}
}
