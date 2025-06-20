/****** Object:  StoredProcedure [dbo].[ImportArticles]    Script Date: 07/28/2011 09:58:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportArticles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ImportArticles]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ImportArticles]
	@LinkedServer [nvarchar](500),
	@DeleteBefore [bit]
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sqlStatement AS nvarchar(max)
	declare @createId as uniqueidentifier 
	
	
	IF @DeleteBefore = 1
		BEGIN
			DELETE FROM SMS.[Articles]
		END;

	BEGIN TRY
		SET @sqlStatement = 'MERGE SMS.[Articles] AS targ
			USING(
				SELECT *
					FROM OPENQUERY(' + @LinkedServer + ', ''SELECT a.MNr AS aMNr,a.KTxt AS aKTxt,f.Periode4 AS fAktPreis,a.SerienKng
									FROM relAc a LEFT OUTER JOIN relFi f ON (a.MNr = f.ArtNr AND f.FiKz = 130) 
									WHERE a.SAint = 90 AND a.RVerwend_2 = 1 AND a.KTXT IS NOT NULL
									''))
					AS source ([ItemNo],[Description],[Price],[Series])
			ON	targ.ItemNo collate SQL_Latin1_General_CP1_CI_AS = source.ItemNo	
			WHEN MATCHED THEN
				UPDATE SET targ.[ItemNo] = source.ItemNo
					   ,targ.[Description] = source.Description
					   ,targ.[Price] = source.Price
					   ,targ.[IsSerial] = source.Series
				WHEN NOT MATCHED THEN
				INSERT ([ItemNo]
					   ,[Description]
					   ,[ArticleType]
					   ,[IsSerial]
					   ,[Price]
					   ,[CreateDate]
					   ,[CreatorId] 
					   ,[ModifyDate] 
					   ,[ModifyId] 
					   ,[IsBatch]
					   ,[DangerousGoodsFlag]
					    )
				VALUES (source.ItemNo
					   ,ISNULL(source.Description,'''')
					   ,'+'''Material'''+'
					   ,source.Series
					   ,source.Price
					   ,CURRENT_TIMESTAMP
					   ,CAST(''afbb1ee2-7cbe-4672-8a9a-0ef2c10e451d'' AS uniqueidentifier)
					   ,CURRENT_TIMESTAMP
					   ,CAST(''afbb1ee2-7cbe-4672-8a9a-0ef2c10e451d'' AS uniqueidentifier)
					   ,0
					   ,0
					   );'
		EXEC(@sqlStatement)
		
		PRINT 'Importing to SMS.[Articles] successful' + CHAR(10)
		
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'SMS.[Articles]', 'Importing Articles';   
	END CATCH;
END
GO
