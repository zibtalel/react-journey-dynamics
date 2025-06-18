namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20200610141000)]
	public class AddPotentialPriorityLookup : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[LU].[PotentialPriority]"))
			{
				Database.ExecuteNonQuery(
					@"
				CREATE TABLE [LU].[PotentialPriority](
				[PotentialPriorityId] [int] IDENTITY(1,1) NOT NULL,
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
					[PotentialPriorityId] ASC
				)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
				) ON [PRIMARY]
	
					ALTER TABLE [LU].[PotentialPriority] ADD  CONSTRAINT [DF__PotentialPriority__Favor]  DEFAULT ((0)) FOR [Favorite]
					ALTER TABLE [LU].[PotentialPriority] ADD  CONSTRAINT [DF__PotentialPriority__Sort]  DEFAULT ((0)) FOR [SortOrder]
					ALTER TABLE [LU].[PotentialPriority] ADD  CONSTRAINT [DF__PotentialPriority__Color]  DEFAULT ('#9E9E9E') FOR [Color]
					ALTER TABLE [LU].[PotentialPriority] ADD  CONSTRAINT [DF_LUPotentialPriority_CreateDate]  DEFAULT (getutcdate()) FOR [CreateDate]
					ALTER TABLE [LU].[PotentialPriority] ADD  CONSTRAINT [DF_LUPotentialPriority_ModifyDate]  DEFAULT (getutcdate()) FOR [ModifyDate]
					ALTER TABLE [LU].[PotentialPriority] ADD  CONSTRAINT [DF_LUPotentialPriority_IsActive]  DEFAULT ((1)) FOR [IsActive]
			");
			}
		}
	}
}