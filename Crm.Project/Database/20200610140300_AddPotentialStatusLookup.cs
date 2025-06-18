namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20200610140300)]
	public class AddPotentialStatusLookup : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[LU].[PotentialStatus]"))
			{
				Database.ExecuteNonQuery(@"
				CREATE TABLE [LU].[PotentialStatus](
				[PotentialStatusId] [int] IDENTITY(1,1) NOT NULL,
				[Name] [nvarchar](50) NOT NULL,
				[Language] [nvarchar](2) NOT NULL,
				[Value] [nvarchar](20) NOT NULL,
				[Favorite] [bit] NOT NULL,
				[SortOrder] [int] NOT NULL,
				[Color] [nvarchar](20) NOT NULL,
				[CreateDate] [datetime] NOT NULL,
				[ModifyDate] [datetime] NOT NULL,
				[CreateUser] [nvarchar](256) NOT NULL,
				[ModifyUser] [nvarchar](256) NOT NULL,
				[IsActive] [bit] NOT NULL

				PRIMARY KEY CLUSTERED 
				(
					[PotentialStatusId] ASC
				)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
				) ON [PRIMARY]
					ALTER TABLE [LU].[PotentialStatus] ADD  CONSTRAINT [DF__PotentialStatus__Favor]  DEFAULT ((0)) FOR [Favorite]
					ALTER TABLE [LU].[PotentialStatus] ADD  CONSTRAINT [DF__PotentialStatus__Sort]  DEFAULT ((0)) FOR [SortOrder]
					ALTER TABLE [LU].[PotentialStatus] ADD  CONSTRAINT [DF__PotentialStatus__Color]  DEFAULT ('#9E9E9E') FOR [Color]
					ALTER TABLE [LU].[PotentialStatus] ADD  CONSTRAINT [DF_LUPotentialStatus_CreateDate]  DEFAULT (getutcdate()) FOR [CreateDate]
					ALTER TABLE [LU].[PotentialStatus] ADD  CONSTRAINT [DF_LUPotentialStatus_ModifyDate]  DEFAULT (getutcdate()) FOR [ModifyDate]
					ALTER TABLE [LU].[PotentialStatus] ADD  CONSTRAINT [DF_LUPotentialStatus_IsActive]  DEFAULT ((1)) FOR [IsActive]
			");
			}
		}
	}
}
