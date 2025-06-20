/****** Object:  StoredProcedure [dbo].[ImportLuFromInfor]    Script Date: 07/28/2011 09:58:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportLuFromInfor]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ImportLuFromInfor]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ImportLuFromInfor]
	@LinkedServer [nvarchar](500),
	@DeleteBefore [bit]
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sqlStatement AS nvarchar(max)

	IF @DeleteBefore = 1
		BEGIN
			BEGIN TRY
				DELETE FROM LU.AddressType
				DELETE FROM LU.CompanyGroupFlag1
				DELETE FROM LU.CompanyGroupFlag2
				DELETE FROM LU.CompanyGroupFlag3
				DELETE FROM LU.CompanyGroupFlag4
				DELETE FROM LU.CompanyGroupFlag5
				DELETE FROM LU.CompanyType
				DELETE FROM LU.Country
				DELETE FROM LU.Position
				DELETE FROM LU.Region
				--DELETE FROM LU.Salutation
				--DELETE FROM LU.SalutationLetter
				DELETE FROM LU.CommunicationType
				--DELETE FROM LU.Title
				PRINT 'Deleting LU-Tables successful' + CHAR(10)	
			END TRY
			BEGIN CATCH
				EXECUTE dbo.CrmImportLog 'LU Tables', 'Deleting LU-Tables successful';   
			END CATCH;
	END;

	-- Update AddressType
	BEGIN TRY
		SET @sqlStatement = 'MERGE LU.AddressType AS targ
			USING (SELECT ISNULL(KTxt, ''''),
			CASE Sprache
				WHEN ''us'' Then
					''en''
				ELSE
					Sprache
			END AS Sprache, ZTKey FROM OPENQUERY(' + @LinkedServer + ', ''select * from RELZTNUM where TabName=''''ANSCHV1'''' AND ZTKEY IN (1,101,102,103,104)''))
			AS source (Name, Sprache, Value)
			ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.Value = source.Value)
			WHEN MATCHED THEN 
				UPDATE SET targ.Name = source.Name, targ.[Language]=source.Sprache, targ.Value=source.value
			WHEN NOT MATCHED THEN
				INSERT (Name, [Language], Value)
				VALUES (source.Name, source.Sprache, source.Value);'
		EXEC(@sqlStatement)
		PRINT 'Importing to LU.AddressType successful' + CHAR(10)	
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'LU.AddressType', 'Importing LU.AddressType';   
	END CATCH;

	-- Update CompanyGroupFlag1
	BEGIN TRY
		SET @sqlStatement = 'MERGE LU.CompanyGroupFlag1 AS targ
			USING (SELECT KTxt,
			CASE Sprache
				WHEN ''us'' Then
					''en''
				ELSE
					Sprache
			END AS Sprache, ISNULL(ZTKey,0) FROM OPENQUERY(' + @LinkedServer + ', ''select * from relZTGK where TABNAME=''''KGRKNZ1'''''')) AS source (Name, Sprache, Value)
			ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.Value collate SQL_Latin1_General_CP1_CI_AS = source.Value)
			WHEN MATCHED THEN 
				UPDATE SET targ.Name = source.Name, targ.[Language]=source.Sprache, targ.Value=source.value
			WHEN NOT MATCHED THEN	
				INSERT (Name, [Language], Value)
				VALUES (source.Name, source.Sprache, source.Value);'
		EXEC(@sqlStatement)
		PRINT 'Importing to LU.CompanyGroupFlag1 successful' + CHAR(10)	
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'LU.CompanyGroupFlag1', 'Importing LU.CompanyGroupFlag1';   
	END CATCH;
			
	-- Update CompanyGroupFlag2
	BEGIN TRY
		SET @sqlStatement = 'MERGE LU.CompanyGroupFlag2 AS targ
			USING (SELECT KTxt,
			CASE Sprache
				WHEN ''us'' Then
					''en''
				ELSE
					Sprache
			END AS Sprache, ISNULL(ZTKey,0) FROM OPENQUERY(' + @LinkedServer + ', ''select * from relZTGK where TABNAME=''''KGRKNZ2'''''')) AS source (Name, Sprache, Value)
			ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.Value collate SQL_Latin1_General_CP1_CI_AS = source.Value)
			WHEN MATCHED THEN 
				UPDATE SET targ.Name = source.Name, targ.[Language]=source.Sprache, targ.Value=source.value
			WHEN NOT MATCHED THEN	
				INSERT (Name, [Language], Value)
				VALUES (source.Name, source.Sprache, source.Value);'
		EXEC(@sqlStatement)
		PRINT 'Importing to LU.CompanyGroupFlag2 successful' + CHAR(10)	
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'LU.CompanyGroupFlag2', 'Importing LU.CompanyGroupFlag2';   
	END CATCH;
		
	-- Update CompanyGroupFlag3
	BEGIN TRY
		SET @sqlStatement = 'MERGE LU.CompanyGroupFlag3 AS targ
			USING (SELECT KTxt,
			CASE Sprache
				WHEN ''us'' Then
					''en''
				ELSE
					Sprache
			END AS Sprache, ISNULL(ZTKey,0) FROM OPENQUERY(' + @LinkedServer + ', ''select * from relZTGK where TABNAME=''''KGRKNZ3'''''')) AS source (Name, Sprache, Value)
			ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.Value collate SQL_Latin1_General_CP1_CI_AS = source.Value)
			WHEN MATCHED THEN 
				UPDATE SET targ.Name = source.Name, targ.[Language]=source.Sprache, targ.Value=source.value
			WHEN NOT MATCHED THEN	
				INSERT (Name, [Language], Value)
				VALUES (source.Name, source.Sprache, source.Value);'
		EXEC(@sqlStatement)
		PRINT 'Importing to LU.CompanyGroupFlag3 successful' + CHAR(10)	
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'LU.CompanyGroupFlag3', 'Importing LU.CompanyGroupFlag3';   
	END CATCH;

	-- Update CompanyGroupFlag4
	BEGIN TRY
		SET @sqlStatement = 'MERGE LU.CompanyGroupFlag4 AS targ
			USING (SELECT KTxt,
			CASE Sprache
				WHEN ''us'' Then
					''en''
				ELSE
					Sprache
			END AS Sprache, ISNULL(ZTKey,0) FROM OPENQUERY(' + @LinkedServer + ', ''select * from relZTGK where TABNAME=''''KGRKNZ4'''''')) AS source (Name, Sprache, Value)
			ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.Value collate SQL_Latin1_General_CP1_CI_AS = source.Value)
			WHEN MATCHED THEN 
				UPDATE SET targ.Name = source.Name, targ.[Language]=source.Sprache, targ.Value=source.value
			WHEN NOT MATCHED THEN	
				INSERT (Name, [Language], Value)
				VALUES (source.Name, source.Sprache, source.Value);'
		EXEC(@sqlStatement)
		PRINT 'Importing to LU.CompanyGroupFlag4 successful' + CHAR(10)	
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'LU.CompanyGroupFlag4', 'Importing LU.CompanyGroupFlag4';   
	END CATCH;

	---- Update CompanyGroupFlag5
	BEGIN TRY
		SET @sqlStatement = 'MERGE LU.CompanyGroupFlag5 AS targ
			USING (SELECT ISNULL(KTxt, ''''),
			CASE Sprache
				WHEN ''us'' Then
					''en''
				ELSE
					Sprache
			END AS Sprache, ISNULL(ZTKey,0) FROM OPENQUERY(' + @LinkedServer + ', ''select * from relZTGK where TABNAME=''''KGRKNZ5'''''')) AS source (Name, Sprache, Value)
			ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.Value collate SQL_Latin1_General_CP1_CI_AS = source.Value)
			WHEN MATCHED THEN 
				UPDATE SET targ.Name = source.Name, targ.[Language]=source.Sprache, targ.Value=source.value
			WHEN NOT MATCHED THEN	
				INSERT (Name, [Language], Value)
				VALUES (source.Name, source.Sprache, source.Value);'
		EXEC(@sqlStatement)
		PRINT 'Importing to LU.CompanyGroupFlag5 successful' + CHAR(10)	
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'LU.CompanyGroupFlag5', 'Importing LU.CompanyGroupFlag5';   
	END CATCH;
		
	-- Update CompanyType
	BEGIN TRY
		SET @sqlStatement = 'MERGE LU.CompanyType AS targ
			USING (SELECT KTxt,
			CASE Sprache
				WHEN ''us'' Then
					''en''
				ELSE
					Sprache
			END AS Sprache, ZTKey FROM OPENQUERY(' + @LinkedServer + ', ''select * from RELZTNUM where TabName=''''FIRMAV1'''' AND ZTKEY IN (1,7)'')) AS source (Name, Sprache, Value)
			ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.Value collate SQL_Latin1_General_CP1_CI_AS = source.Value)
			WHEN MATCHED THEN 
				UPDATE SET targ.Name = source.Name, targ.[Language]=source.Sprache, targ.Value=source.value
			WHEN NOT MATCHED THEN	
				INSERT (Name, [Language], Value)
				VALUES (source.Name, source.Sprache, source.Value);'
		EXEC(@sqlStatement)
		PRINT 'Importing to LU.CompanyType successful' + CHAR(10)	
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'LU.CompanyType', 'Importing LU.CompanyType';   
	END CATCH;

	---- Update Country
	--MERGE LU.Country AS targ
	--	USING (SELECT ISNULL(ZTKey, 0), ISNULL(KTxt, ''''), Sprache, ISNULL(INTRACODELAND,0) FROM OPENQUERY(INFORDB, 'select * from RELZTLT where TabName=''LANDTAB'''))
	--		AS source (CountryCode, Name, Sprache, Value)
	--	ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.CountryCode collate SQL_Latin1_General_CP1_CI_AS = source.CountryCode)
	--	WHEN MATCHED THEN 
	--		UPDATE SET targ.CountryCode=source.CountryCode, targ.Name = source.Name, targ.[Language]=source.Sprache, targ.Value=source.value
	--	WHEN NOT MATCHED THEN	
	--		INSERT (CountryCode, Name, [Language], Value)
	--		VALUES (source.CountryCode, source.Name, source.Sprache, source.Value);

	--Perhaps this SQL-Statement is needed for Importing the ZTKEY (i.e. 'A' instead of 'AT' for Country Austria)
	-- Update Country
	BEGIN TRY
		SET @sqlStatement = 'MERGE LU.Country AS targ
			USING (SELECT ISNULL(KTxt, ''''), 
			CASE Sprache
				WHEN ''us'' Then
					''en''
				ELSE
					Sprache
			END AS Sprache, ISNULL(ZTKey, 0) FROM OPENQUERY(' + @LinkedServer + ', ''select * from RELZTLT where TabName=''''LANDTAB''''''))
				AS source (Name, Sprache, Value)
			ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.Value collate SQL_Latin1_General_CP1_CI_AS = source.Value)
			WHEN MATCHED THEN 
				UPDATE SET targ.Name = source.Name, targ.[Language]=source.Sprache, targ.Value=source.value
			WHEN NOT MATCHED THEN	
				INSERT (Name, [Language], Value)
				VALUES (source.Name, source.Sprache, source.Value);'
		EXEC(@sqlStatement)
		PRINT 'Importing to LU.Country successful' + CHAR(10)	
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'LU.Country', 'Importing LU.Country';   
	END CATCH;

	-- Update Position
	BEGIN TRY
		SET @sqlStatement = 'MERGE LU.Position AS targ
			USING (SELECT ISNULL(KTxt, ''''), 
			CASE Sprache
				WHEN ''us'' Then
					''en''
				ELSE
					Sprache
			END AS Sprache, ZTKey FROM OPENQUERY(' + @LinkedServer + ', ''select * from RELZTNUM where TabName=''''PERSV1''''''))
				AS source (Name, Sprache, Value)
			ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.Value collate SQL_Latin1_General_CP1_CI_AS = source.Value)
			WHEN MATCHED THEN 
				UPDATE SET targ.Name = source.Name, targ.[Language]=source.Sprache, targ.Value=source.value
			WHEN NOT MATCHED THEN	
				INSERT (Name, [Language], Value)
				VALUES (source.Name, source.Sprache, source.Value);'
		EXEC(@sqlStatement)
		PRINT 'Importing to LU.Position successful' + CHAR(10)	
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'LU.Position', 'Importing LU.Position';   
	END CATCH;
			
	-- Update Region
	BEGIN TRY
		SET @sqlStatement = 'MERGE LU.Region AS targ
			USING (SELECT ISNULL(KTxt, ''''),
			CASE Sprache
				WHEN ''us'' Then
					''en''
				ELSE
					Sprache
			END AS Sprache, ISNULL(ZTKey, 0) FROM OPENQUERY(' + @LinkedServer + ', ''select * from RELZTNUM where TabName=''''BLAND''''''))
				AS source (Name, Sprache, Value)
			ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.Value collate SQL_Latin1_General_CP1_CI_AS = source.Value)
			WHEN MATCHED THEN 
				UPDATE SET targ.Name = source.Name, targ.[Language]=source.Sprache, targ.Value=source.value
			WHEN NOT MATCHED THEN	
				INSERT (Name, [Language], Value)
				VALUES (source.Name, source.Sprache, source.Value);'
		EXEC(@sqlStatement)
		PRINT 'Importing to LU.Region successful' + CHAR(10)	
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'LU.Region', 'Importing LU.Region';   
	END CATCH;

	-- Bei Leiner ist nichts in der Datenbank, deshalb kein Import
	-- Update Salutation
	--MERGE LU.Salutation AS targ
	--	USING (SELECT ISNULL(KTxt,''),Sprache, ISNULL(ZTKey, 0) FROM OPENQUERY(INFORDB, 'select * from RELZTNUM where TabName=''USANREDE'''))
	--		AS source (Name, Sprache, Value)
	--	ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.Value collate SQL_Latin1_General_CP1_CI_AS = source.Value)
	--	WHEN MATCHED THEN 
	--		UPDATE SET targ.Name = source.Name, targ.[Language]=source.Sprache, targ.Value=source.value
	--	WHEN NOT MATCHED THEN	
	--		INSERT (Name, [Language], Value)
	--		VALUES (source.Name, source.Sprache, source.Value);

	-- Bei Leiner ist nichts in der Datenbank, deshalb kein Import
	---- Update SalutationLetter
	--MERGE LU.SalutationLetter AS targ
	--	USING (SELECT ISNULL(KTxt,''),Sprache, ISNULL(ZTKey, 0) FROM OPENQUERY(INFORDB, 'select * from RELZTNUM where TabName=''USBRIEFAN'''))
	--		AS source (Name, Sprache, Value)
	--	ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.Value collate SQL_Latin1_General_CP1_CI_AS = source.Value)
	--	WHEN MATCHED THEN 
	--		UPDATE SET targ.Name = source.Name, targ.[Language]=source.Sprache, targ.Value=source.value
	--	WHEN NOT MATCHED THEN	
	--		INSERT (Name, [Language], Value)
	--		VALUES (source.Name, source.Sprache, source.Value);

	-- vermutlich gar nicht verwendet
	---- Update CommunicationGroup
	--MERGE LU.CommunicationGroup AS targ
	--	USING (SELECT KTxt, Sprache, ZTKey FROM OPENQUERY(INFORDB, 'select * from RELZTNUM where TabName=''USKOMMTYP'' AND Sprache=''en'''))
	--		AS source (Name, Sprache, Value)
	--	ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.Value collate SQL_Latin1_General_CP1_CI_AS = source.Value)
	--	WHEN MATCHED THEN 
	--		UPDATE SET targ.Name = source.Name, targ.[Language]=source.Sprache, targ.Value=source.value
	--	WHEN NOT MATCHED THEN	
	--		INSERT (Name, [Language], Value)
	--		VALUES (source.Name, source.Sprache, source.Value);

	BEGIN TRY
		-- TRUNCATE CommunicationType before INSERT/UPDATE
		SET @sqlStatement = 'TRUNCATE TABLE LU.CommunicationType;'

		-- Update CommunicationType [!!! EXECUTE after LU.CommunicationGroup import !!!]
		SET @sqlStatement = @sqlStatement + 'MERGE LU.CommunicationType AS targ
			USING (
					SELECT KTxt, 
						CASE ZTKey
							WHEN 1 THEN
								''Phone''
							WHEN 2 THEN
								''Fax''
							WHEN 3 THEN
								''Phone''
							WHEN 4 THEN
								''Email''
							WHEN 5 THEN
								''Website''
							WHEN 6 THEN
								''Website''
							WHEN 7 THEN
								''Phone''
							WHEN 8 THEN
								''Phone''
							WHEN 9 THEN
								''Phone''
						END AS CommTypGroupKey,
						CASE Sprache
							WHEN ''us'' Then
								''en''
							ELSE
								Sprache
						END AS Sprache, ZTKey FROM OPENQUERY(' + @LinkedServer + ', ''select * from RELZTNUM where TabName=''''KOMMART'''''')
					)
				AS source (Name, CommTypeGroupKey, Sprache, Value)
			ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.Value collate SQL_Latin1_General_CP1_CI_AS = source.Value)
			WHEN MATCHED THEN 
				UPDATE SET 
					targ.Name = source.Name,
					targ.CommTypeGroupKey = source.CommTypeGroupKey,
					targ.[Language]=source.Sprache,
					targ.Value=source.value
			WHEN NOT MATCHED THEN	
				INSERT (
						Name,
						CommTypeGroupKey,
						[Language],
						Value
						)
				VALUES (
						source.Name,
						source.CommTypeGroupKey,
						source.Sprache,
						source.Value
						);'
		EXEC(@sqlStatement)
		PRINT 'Deleting and importing to LU.CommunicationType successful' + CHAR(10)	
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'LU.CommunicationType', 'Deleting and importing LU.CommunicationType';   
	END CATCH;

		
	-- Bei Leiner ist nichts in der Datenbank, deshalb kein Import
	----Update LU.Title
	--MERGE LU.Title AS targ
	--	USING (select ISNULL(KTxt,''),Sprache, ZTKey from OPENQUERY(INFORDB, 'select * from RELZTNUM where TabName=''USTITEL''')) AS source (Name, Sprache, Value)
	--	ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.Value = source.Value)
	--	WHEN MATCHED THEN 
	--		UPDATE SET targ.Name = source.Name, targ.[Language]=source.Sprache, targ.Value=source.value
	--	WHEN NOT MATCHED THEN
	--		INSERT (Name, [Language], Value)
	--		VALUES (source.Name, source.Sprache, source.Value);
			
	---- INSERT Currency
	--INSERT INTO LU.Currency (Name, [Language], Value)
	--SELECT KTxt,Sprache, WE FROM OPENQUERY(INFORDB, 'select * from RELZTFWEUR where TabName=''FREMDWEUR''')


END
GO
