/****** Object:  StoredProcedure [dbo].[ImportCrmTags]    Script Date: 07/28/2011 09:58:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportCrmTags]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ImportCrmTags]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ImportCrmTags]
	@Locale [nvarchar](4),
	@DeleteBefore [bit]
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sqlStatement AS nvarchar(4000)
	DECLARE @cgfNames TABLE(name nvarchar(150))
	DECLARE @cgfTable AS nvarchar(150)
	DECLARE @completeName AS nvarchar(150)
	
	INSERT INTO @cgfNames values ('CompanyGroupFlag1');
	INSERT INTO @cgfNames values ('CompanyGroupFlag2');
	INSERT INTO @cgfNames values ('CompanyGroupFlag3');
	INSERT INTO @cgfNames values ('CompanyGroupFlag4');
	INSERT INTO @cgfNames values ('CompanyGroupFlag5');
	
	DECLARE cfg_Cursor CURSOR FOR
	SELECT name FROM @cgfNames
	
	IF @DeleteBefore = 1
		BEGIN
			DELETE FROM CRM.[ContactTags]
			PRINT 'Deleting CRM.ContactTags successful.'
		END;
	
	OPEN cfg_Cursor;
	FETCH NEXT FROM cfg_Cursor INTO @cgfTable;
	WHILE @@FETCH_STATUS = 0
	   BEGIN
			BEGIN TRY
				SET @sqlStatement = 'INSERT INTO [CRM].[ContactTags]
					SELECT DISTINCT c.ContactId, cgf.Name
					FROM [LU].[' + @cgfTable + '] cgf
					JOIN [Crm].[Contact] c
					ON cgf.Value = c.' + @cgfTable + 'Key AND cgf.[Language] =''' + @Locale + '''
					WHERE c.' + @cgfTable + 'Key != 0;'
			
				EXEC(@sqlStatement)			
				PRINT 'Importing to CRM.ContactTags from [LU].[' + @cgfTable + '] successful' + CHAR(10)	
			END TRY
			BEGIN CATCH
				SET @completeName = 'Importing to CRM.ContactTags from [LU].[' + @cgfTable + ']';
				EXECUTE dbo.CrmImportLog 'CRM.ContactTags', @completeName;   
			END CATCH;
			FETCH NEXT FROM cfg_Cursor INTO @cgfTable;
	   END;
	CLOSE cfg_Cursor;
	DEALLOCATE cfg_Cursor;
	
END
GO
