namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20200610142900)]
	public class AddPotentialContactRelationshipTypeLookup : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[LU].[PotentialContactRelationshipType]"))
			{
				Database.ExecuteNonQuery(
					@"
					CREATE TABLE [LU].[PotentialContactRelationshipType](
					[PotentialContactRelationshipTypeId] [int] IDENTITY(1,1) NOT NULL,
				  [Value] [nvarchar](50) NOT NULL,
					[Name] [nvarchar](255) NOT NULL,
					[Language] [nvarchar](2) NOT NULL,
					[Favorite] [bit] NOT NULL,
					[SortOrder] [int] NOT NULL,
					[CreateDate] [datetime] NOT NULL,
					[ModifyDate] [datetime] NOT NULL,
          [CreateUser] [nvarchar](256) NOT NULL,
          [ModifyUser] [nvarchar](256) NOT NULL,
					[IsActive] [bit] NOT NULL,
					PRIMARY KEY CLUSTERED 
					(
						[PotentialContactRelationshipTypeId] ASC
					)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
				) ON [PRIMARY]

					ALTER TABLE [LU].[PotentialContactRelationshipType] ADD  CONSTRAINT [DF_LUPotentialContactRelationshipType_CreateDate]  DEFAULT (getutcdate()) FOR [CreateDate]
					ALTER TABLE [LU].[PotentialContactRelationshipType] ADD  CONSTRAINT [DF_LUPotentialContactRelationshipType_ModifyDate]  DEFAULT (getutcdate()) FOR [ModifyDate]
					ALTER TABLE [LU].[PotentialContactRelationshipType] ADD  CONSTRAINT [DF_LUPotentialContactRelationshipType_IsActive]  DEFAULT ((1)) FOR [IsActive]	
			");
			}
		}
	}
}