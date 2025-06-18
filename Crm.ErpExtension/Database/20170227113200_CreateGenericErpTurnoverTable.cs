namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20170227113200)]
	public class CreateGenericErpTurnoverTable : Migration
	{
		public override void Up()
		{
			if (Database.ColumnExists("[CRM].[Turnover]", "EntryKey"))
			{
				if ((int)Database.ExecuteScalar("SELECT COUNT(*) FROM sys.foreign_keys WHERE name = 'FK_Turnover_Contact'") == 1)
				{
					Database.ExecuteNonQuery("ALTER TABLE [CRM].[Turnover] DROP CONSTRAINT FK_Turnover_Contact");
				}
				Database.ExecuteNonQuery("sp_rename 'CRM.Turnover', 'TurnoverOld'");
				Database.ExecuteNonQuery(@"CREATE TABLE [CRM].[Turnover](
	[TurnoverId] [int] IDENTITY(1, 1) NOT NULL,
	[ContactKey] [uniqueidentifier] NULL,
	[LegacyId] [nvarchar](100) NULL,
	[ItemNo] [nvarchar](50) NOT NULL,
	[ItemDescription] [nvarchar](150) NULL,
	[CurrencyKey] [nvarchar](20) NULL,
	[QuantityUnitKey] [nvarchar](20) NULL,
	[ArticleGroup01Key] [nvarchar](20) NULL,
	[ArticleGroup02Key] [nvarchar](20) NULL,
	[ArticleGroup03Key] [nvarchar](20) NULL,
	[ArticleGroup04Key] [nvarchar](20) NULL,
	[ArticleGroup05Key] [nvarchar](20) NULL,
	[IsVolume] [bit] NOT NULL,
	[Month1] [float] NULL,
	[Month2] [float] NULL,
	[Month3] [float] NULL,
	[Month4] [float] NULL,
	[Month5] [float] NULL,
	[Month6] [float] NULL,
	[Month7] [float] NULL,
	[Month8] [float] NULL,
	[Month9] [float] NULL,
	[Month10] [float] NULL,
	[Month11] [float] NULL,
	[Month12] [float] NULL,
	[Total] [float] NULL,
	[Requirement] [float] NULL,
	[Year] [int] NOT NULL,
	[TenantKey] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[ModifyUser] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[TurnoverId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]");
				Database.ExecuteNonQuery("ALTER TABLE [CRM].[Turnover] ADD DEFAULT (getutcdate()) FOR [CreateDate]");
				Database.ExecuteNonQuery("ALTER TABLE [CRM].[Turnover] ADD  DEFAULT(getutcdate()) FOR [ModifyDate]");
				Database.ExecuteNonQuery("ALTER TABLE [CRM].[Turnover] ADD  DEFAULT('Setup') FOR [CreateUser]");
				Database.ExecuteNonQuery("ALTER TABLE [CRM].[Turnover] ADD  DEFAULT('Setup') FOR [ModifyUser]");
				Database.ExecuteNonQuery("ALTER TABLE [CRM].[Turnover] ADD  DEFAULT((1)) FOR [IsActive]");
				Database.ExecuteNonQuery("ALTER TABLE [CRM].[Turnover] WITH CHECK ADD CONSTRAINT [FK_Turnover_Contact] FOREIGN KEY ([ContactKey]) REFERENCES [CRM].[Contact] ([ContactId])");
				var currencyKey = (string)Database.ExecuteScalar("SELECT TOP 1 [Value] FROM [LU].[Currency] WHERE [IsActive] = 1 ORDER BY [Favorite] DESC, [SortOrder] ASC");
				Database.ExecuteNonQuery($@"INSERT INTO [CRM].[Turnover]
([ContactKey],
[LegacyId],
[ItemNo],
[ItemDescription],
[CurrencyKey],
[QuantityUnitKey],
[ArticleGroup01Key],
[ArticleGroup02Key],
[ArticleGroup03Key],
[ArticleGroup04Key],
[ArticleGroup05Key],
[IsVolume],
[Month1],
[Month2],
[Month3],
[Month4],
[Month5],
[Month6],
[Month7],
[Month8],
[Month9],
[Month10],
[Month11],
[Month12],
[Total],
[Requirement],
[Year])
SELECT 
	t.[ContactKey],
	t.[LegacyId],
	t.[Datafield2],
	t.[Datafield4],
	CASE WHEN t.[RecordType] NOT IN ('USABKA', 'USABPA') THEN '{currencyKey}' ELSE NULL END,
	CASE WHEN t.[RecordType] IN ('USABKA', 'USABPA') THEN a.[QuantityUnit] ELSE NULL END,
	a.[ArticleGroup01],
	a.[ArticleGroup02],
	a.[ArticleGroup03],
	a.[ArticleGroup04],
	a.[ArticleGroup05],
	CASE WHEN t.[RecordType] IN ('USABKA', 'USABPA') THEN 1 ELSE 0 END,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	t.[CurrentYear],
	NULL,
	YEAR(GETDATE())
FROM [CRM].[TurnoverOld] t
JOIN [CRM].[Article] a ON a.[ItemNo] = t.[Datafield2]
JOIN [CRM].[Contact] ac ON ac.[ContactId] = a.[ArticleId] AND ac.[IsActive] = 1
");
				Database.ExecuteNonQuery($@"INSERT INTO [CRM].[Turnover]
([ContactKey],
[LegacyId],
[ItemNo],
[ItemDescription],
[CurrencyKey],
[QuantityUnitKey],
[ArticleGroup01Key],
[ArticleGroup02Key],
[ArticleGroup03Key],
[ArticleGroup04Key],
[ArticleGroup05Key],
[IsVolume],
[Month1],
[Month2],
[Month3],
[Month4],
[Month5],
[Month6],
[Month7],
[Month8],
[Month9],
[Month10],
[Month11],
[Month12],
[Total],
[Requirement],
[Year])
SELECT 
	t.[ContactKey],
	t.[LegacyId],
	t.[Datafield2],
	t.[Datafield4],
	CASE WHEN t.[RecordType] NOT IN ('USABKA', 'USABPA') THEN '{currencyKey}' ELSE NULL END,
	CASE WHEN t.[RecordType] IN ('USABKA', 'USABPA') THEN a.[QuantityUnit] ELSE NULL END,
	a.[ArticleGroup01],
	a.[ArticleGroup02],
	a.[ArticleGroup03],
	a.[ArticleGroup04],
	a.[ArticleGroup05],
	CASE WHEN t.[RecordType] IN ('USABKA', 'USABPA') THEN 1 ELSE 0 END,
	t.[Month1],
	t.[Month2],
	t.[Month3],
	t.[Month4],
	t.[Month5],
	t.[Month6],
	t.[Month7],
	t.[Month8],
	t.[Month9],
	t.[Month10],
	t.[Month11],
	t.[Month12],
	t.[PreviousYear],
	NULL,
	YEAR(GETDATE()) - 1
FROM [CRM].[TurnoverOld] t
JOIN [CRM].[Article] a ON a.[ItemNo] = t.[Datafield2]
JOIN [CRM].[Contact] ac ON ac.[ContactId] = a.[ArticleId] AND ac.[IsActive] = 1
WHERE t.[PreviousYear] IS NOT NULL AND t.[PreviousYear] > 0
");
				Database.ExecuteNonQuery($@"INSERT INTO [CRM].[Turnover]
([ContactKey],
[LegacyId],
[ItemNo],
[ItemDescription],
[CurrencyKey],
[QuantityUnitKey],
[ArticleGroup01Key],
[ArticleGroup02Key],
[ArticleGroup03Key],
[ArticleGroup04Key],
[ArticleGroup05Key],
[IsVolume],
[Month1],
[Month2],
[Month3],
[Month4],
[Month5],
[Month6],
[Month7],
[Month8],
[Month9],
[Month10],
[Month11],
[Month12],
[Total],
[Requirement],
[Year])
SELECT 
	t.[ContactKey],
	t.[LegacyId],
	t.[Datafield2],
	t.[Datafield4],
	CASE WHEN t.[RecordType] NOT IN ('USABKA', 'USABPA') THEN '{currencyKey}' ELSE NULL END,
	CASE WHEN t.[RecordType] IN ('USABKA', 'USABPA') THEN a.[QuantityUnit] ELSE NULL END,
	a.[ArticleGroup01],
	a.[ArticleGroup02],
	a.[ArticleGroup03],
	a.[ArticleGroup04],
	a.[ArticleGroup05],
	CASE WHEN t.[RecordType] IN ('USABKA', 'USABPA') THEN 1 ELSE 0 END,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	t.[PrePreviousYear],
	NULL,
	YEAR(GETDATE()) - 2
FROM [CRM].[TurnoverOld] t
JOIN [CRM].[Article] a ON a.[ItemNo] = t.[Datafield2]
JOIN [CRM].[Contact] ac ON ac.[ContactId] = a.[ArticleId] AND ac.[IsActive] = 1
WHERE t.[PrePreviousYear] IS NOT NULL AND t.[PrePreviousYear] > 0
");
				Database.ExecuteNonQuery("DROP TABLE CRM.TurnoverOld");
			}
		}
	}
}