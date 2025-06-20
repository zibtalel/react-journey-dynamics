/****** Object:  StoredProcedure [dbo].[ImportCrmFromInfor]    Script Date: 07/28/2011 09:58:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportCrmFromInfor]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ImportCrmFromInfor]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ImportCrmFromInfor]
	@LinkedServer [nvarchar](500),
	@DeleteBefore [bit]
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sqlStatement AS nvarchar(max)

	IF @DeleteBefore = 1
		BEGIN
			DELETE FROM CRM.[Contact]
			DELETE FROM CRM.[ContactAddress]
			DELETE FROM CRM.[Address]
			DELETE FROM CRM.[Communication]
			DELETE FROM CRM.[Position]
		END;

	BEGIN TRY
		SET @sqlStatement = 'MERGE CRM.[Address] AS targ
			USING(
				SELECT Name1,Name2,Name3,Strasse,Ort,PLZOrt, LandKng, reg.Value, Verwendung1, ModifyDate, AnschriftNr, Postfach, PlzPostfach, SprachKnz
					FROM OPENQUERY(' + @LinkedServer + ', ''select * from relAnsch'')
					LEFT JOIN LU.Region AS reg 
					ON reg.Name collate SQL_Latin1_General_CP1_CI_AS = Land and reg.[Language] collate SQL_Latin1_General_CP1_CI_AS = ISNULL(SprachKnz,'''')
					where LandKng is not null)
				AS source ([Name1],[Name2],[Name3],[Street],[City],[ZipCode],[CountryKey],[RegionKey], [AddressTypeKey],[ModifyDate],[LegacyId],[POBox],[ZipCodePOBox], SprachKnz)
			ON	targ.LegacyId collate SQL_Latin1_General_CP1_CI_AS = source.LegacyId	
			WHEN MATCHED THEN
				UPDATE SET targ.[Name1] = source.Name1
					   ,targ.[Name2] = source.Name2
					   ,targ.[Name3] = source.Name3
					   ,targ.[Street] = source.Street
					   ,targ.[City] = source.City
					   ,targ.[ZipCode] = source.ZipCode
					   ,targ.[RegionKey] = source.RegionKey
					   ,targ.[CountryKey] = source.CountryKey
					   ,targ.[AddressTypeKey] = source.AddressTypeKey
					   ,targ.[ModifyDate] = ISNULL(source.ModifyDate,targ.[CreateDate])
					   ,targ.[LegacyId] = source.LegacyId
					   ,targ.[POBox] = source.[POBox]
					   ,targ.[ZipCodePOBox] = source.[ZipCodePOBox]
					   ,targ.[LanguageKey] = source.SprachKnz	
			WHEN NOT MATCHED THEN
				INSERT ([Name1]
					   ,[Name2]
					   ,[Name3]
					   ,[Street]
					   ,[City]
					   ,[ZipCode]
					   ,[RegionKey]
					   ,[CountryKey]
					   ,[AddressTypeKey]
					   ,[ModifyDate]
					   ,[LegacyId]
					   ,[IsActive]
					   ,[CreateDate]
					   ,[CreateUser]
					   ,[ModifyUser]
					   ,[POBox]
					   ,[ZipCodePOBox]
					   ,[LanguageKey]
					   )
				VALUES (source.Name1
					   ,source.Name2
					   ,source.Name3
					   ,source.Street
					   ,source.City
					   ,source.ZipCode
					   ,source.RegionKey
					   ,source.CountryKey
					   ,source.AddressTypeKey
					   ,ISNULL(source.ModifyDate,CURRENT_TIMESTAMP)
					   ,source.LegacyId
					   ,1
					   ,CURRENT_TIMESTAMP
					   ,''''
					   ,''''
					   ,source.[POBox]
					   ,source.[ZipCodePOBox]
					   ,source.SprachKnz
					   );'
		EXEC(@sqlStatement)
		
		PRINT 'Importing to CRM.[Address] successful' + CHAR(10)
		
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'CRM.[Address]', 'Importing Addresses';   
	END CATCH;


	--INSERT/UPDATE CRM.Contact (Firma)
	BEGIN TRY
		SET @sqlStatement = 'MERGE CRM.[Contact] AS targ
			USING(
				SELECT FirmaNr,KTxt,Name,Verwendung1,Suchbegriff,SprachKnz,Sachbearbeiter,FGKnz_1,FGKnz_2,FGKnz_3,FGKnz_4,FGKnz_5,ModifyDate
					FROM OPENQUERY(' + @LinkedServer + ', ''select * from relFirma where Verwendung1 IN (1,7)'')
					WHERE KTxt is not Null
					)
				AS source ([LegacyId],[Name],[CompanyShortText],[CompanyType],[CompanySearchText],[ContactLanguage],[ResponsibleUser],[CompanyGroupFlag1Key],[CompanyGroupFlag2Key],[CompanyGroupFlag3Key],[CompanyGroupFlag4Key],[CompanyGroupFlag5Key], [ModifyDate])
			ON	targ.LegacyId collate SQL_Latin1_General_CP1_CI_AS = source.LegacyId	
			WHEN MATCHED THEN
				UPDATE SET targ.[LegacyId] = source.[LegacyId]
				   ,targ.[ContactLanguage] = source.[ContactLanguage]
				   ,targ.[Name] = CASE WHEN ISNULL(source.[LegacyId], '''') != '''' THEN source.[LegacyId] + '' - '' + source.[Name] ELSE source.[Name] END
				   ,targ.[ResponsibleUser] = source.[ResponsibleUser]
				   ,targ.[CompanyShortText] = source.[CompanyShortText]
				   ,targ.[CompanyType] = source.[CompanyType]
				   ,targ.[CompanySearchText] = source.[CompanySearchText]
				   ,targ.[CompanyGroupFlag1Key] = source.[CompanyGroupFlag1Key]
				   ,targ.[CompanyGroupFlag2Key] = source.[CompanyGroupFlag2Key]
				   ,targ.[CompanyGroupFlag3Key] = source.[CompanyGroupFlag3Key]
				   ,targ.[CompanyGroupFlag4Key] = source.[CompanyGroupFlag4Key]
				   ,targ.[CompanyGroupFlag5Key] = source.[CompanyGroupFlag5Key]
				   ,targ.[ModifyDate] = ISNULL(source.ModifyDate,targ.[CreateDate])
				   ,targ.[Visibility] = 2
			WHEN NOT MATCHED THEN
				INSERT ([LegacyId]
				   ,[ContactType]
				   ,[ContactLanguage]
				   ,[Name]
				   ,[ResponsibleUser]
				   ,[CompanyShortText]
				   ,[CompanyType]
				   ,[CompanySearchText]
				   ,[CompanyGroupFlag1Key]
				   ,[CompanyGroupFlag2Key]
				   ,[CompanyGroupFlag3Key]
				   ,[CompanyGroupFlag4Key]
				   ,[CompanyGroupFlag5Key]
				   ,[CreateDate]
				   ,[ModifyDate]
				   ,[CreateUser]
				   ,[ModifyUser]
				   ,[CompanyIsOwnCompany]
				   ,[PersonMima]
				   ,[IsActive]
				   ,[Visibility]
				   )
				VALUES (source.[LegacyId]
				   ,''Company''
				   ,source.[ContactLanguage]
				   ,CASE WHEN ISNULL(source.[LegacyId], '''') != '''' THEN source.[LegacyId] + '' - '' + source.[Name] ELSE source.[Name] END
				   ,source.[ResponsibleUser]
				   ,source.[CompanyShortText]
				   ,source.[CompanyType]
				   ,source.[CompanySearchText]
				   ,source.[CompanyGroupFlag1Key]
				   ,source.[CompanyGroupFlag2Key]
				   ,source.[CompanyGroupFlag3Key]
				   ,source.[CompanyGroupFlag4Key]
				   ,source.[CompanyGroupFlag5Key]
				   ,CURRENT_TIMESTAMP
				   ,ISNULL(source.ModifyDate,CURRENT_TIMESTAMP)
				   ,''''
				   ,''''
				   ,0
				   ,0
				   ,1
				   ,2
				   );'
		EXEC(@sqlStatement)
		
	PRINT 'Importing to CRM.[Contact] successful' + CHAR(10)
		
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'CRM.[Contact]', 'Importing Companies';   
	END CATCH;

	--INSERT/UPDATE CRM.ContactAddress (Firma-Anschrift Zuordnung)
	BEGIN TRY
		SET @sqlStatement = 'MERGE CRM.[ContactAddress] AS targ
			USING(
				SELECT PersonNr,FirmaNr,AnschriftNr, addr.AddressId, contact.ContactId
					FROM OPENQUERY(' + @LinkedServer + ', ''select * from relAdresse where PersonNr is null'')
					LEFT JOIN CRM.[Address] AS addr 
					ON addr.LegacyId collate SQL_Latin1_General_CP1_CI_AS = AnschriftNr
					JOIN CRM.[Contact] AS contact
					ON contact.LegacyId collate SQL_Latin1_General_CP1_CI_AS = FirmaNr
					)
				AS source (PersonNr, FirmaNr, AnschriftNr,[AddressKey],[ContactKey])
			ON	targ.[AddressKey] = source.[AddressKey] 
			AND targ.[ContactKey] = source.[ContactKey]	
			WHEN MATCHED THEN
				UPDATE SET targ.[AddressKey] = source.[AddressKey]
				,targ.[ContactKey] = source.[ContactKey]
			WHEN NOT MATCHED THEN
				INSERT ([AddressKey]
				   ,[ContactKey]
				   )
				VALUES (source.[AddressKey]
				   ,source.[ContactKey]
				   );'
		EXEC(@sqlStatement)
		
		PRINT 'Importing to CRM.[ContactAddress] (Company Address Mapping) successful' + CHAR(10)
		
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'CRM.[ContactAddress]', 'Importing ContactAddresses';   
	END CATCH;
           
         
	--INSERT/UPDATE CRM.Contacts (Persons)
	BEGIN TRY
		SET @sqlStatement = 'MERGE CRM.[Contact] AS targ
			USING(
				SELECT PersonNr,Vorname,p.Name,SprachKnz,t.[Value],s.[Value], sl.[Value], ModifyDate, po.Name
					FROM OPENQUERY(' + @LinkedServer + ', ''select * from relPerson'') As p
					LEFT JOIN LU.Title as t ON t.[Name] collate SQL_Latin1_General_CP1_CI_AS = Titel AND t.[Language] collate SQL_Latin1_General_CP1_CI_AS = SprachKnz
					LEFT JOIN LU.Salutation as s ON s.[Name] collate SQL_Latin1_General_CP1_CI_AS = Anrede AND s.[Language] collate SQL_Latin1_General_CP1_CI_AS = SprachKnz
					LEFT JOIN LU.SalutationLetter as sl ON sl.[Name] collate SQL_Latin1_General_CP1_CI_AS = BriefAnrede AND sl.[Language]collate SQL_Latin1_General_CP1_CI_AS = SprachKnz
					LEFT JOIN LU.Position as po ON po.[Value] = p.Verwendung1 AND po.[Language] collate SQL_Latin1_General_CP1_CI_AS = SprachKnz
					)
				AS source ([LegacyId],[PersonFirstname],[PersonSurname], [ContactLanguage],[PersonTitleKey],[PersonSalutationKey],[PersonLetterSalutationKey],[ModifyDate], [PersonBusinessTitle])
			ON	targ.LegacyId collate SQL_Latin1_General_CP1_CI_AS = source.LegacyId	
			WHEN MATCHED THEN
				UPDATE SET targ.[LegacyId] = source.[LegacyId]
				  ,targ.[ContactLanguage] = source.[ContactLanguage]
				  ,targ.[Name] = ISNULL(source.[PersonSurname] + '', '' + source.[PersonFirstname],ISNULL(source.[PersonSurname],''''))
				  ,targ.[PersonFirstname] = source.[PersonFirstname]
				  ,targ.[PersonSurname] = source.[PersonSurname]
				  ,targ.[PersonTitleKey] = source.[PersonTitleKey]
				  ,targ.[PersonSalutationKey] = source.[PersonSalutationKey]
				  ,targ.[PersonLetterSalutationKey] = source.[PersonLetterSalutationKey]
				  ,targ.[ModifyDate] = ISNULL(source.ModifyDate,targ.[CreateDate])
				  ,targ.[PersonBusinessTitle] = source.[PersonBusinessTitle]
				  ,targ.[Visibility] = 2
			WHEN NOT MATCHED THEN
				INSERT ([LegacyId]
				  ,[ContactType]
				  ,[ContactLanguage]
				  ,[Name]
				  ,[IsActive]
				  ,[PersonFirstname]
				  ,[PersonSurname]
				  ,[PersonMima]
				  ,[PersonTitleKey]
				  ,[PersonSalutationKey]
				  ,[PersonLetterSalutationKey]
				  ,[CreateDate]
				  ,[ModifyDate]
				  ,[CreateUser]
				  ,[ModifyUser]
				  ,[PersonBusinessTitle]
				  ,[Visibility]
				   )
				VALUES (source.[LegacyId]
				  ,''Person''
				  ,source.[ContactLanguage]
				  ,ISNULL(source.[PersonSurname] + '', '' + source.[PersonFirstname],ISNULL(source.[PersonSurname],''''))
				  ,1
				  ,source.[PersonFirstname]
				  ,source.[PersonSurname]
				  ,0
				  ,source.[PersonTitleKey]
				  ,source.[PersonSalutationKey]
				  ,source.[PersonLetterSalutationKey]
				  ,CURRENT_TIMESTAMP
				  ,ISNULL(source.ModifyDate,CURRENT_TIMESTAMP)
				  ,''''
				  ,''''
				  ,source.[PersonBusinessTitle]
				  ,2
				   );'
		EXEC(@sqlStatement)
	
		PRINT 'Importing to CRM.[Contact] (Persons) successful' + CHAR(10)
		
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'CRM.[Contact]', 'Importing Persons';   
	END CATCH;

 
	--INSERT/UPDATE CRM.ContactAddress (Person-Anschrift Zuordnung)
	BEGIN TRY
		SET @sqlStatement = 'MERGE CRM.[ContactAddress] AS targ
			USING(
				SELECT PersonNr,AnschriftNr, addr.AddressId, contact.ContactId
					FROM (	Select PersonNr,AnschriftNr
							From OPENQUERY(' + @LinkedServer + ', ''select * from relAdresse where PersonNr is not null'')
							GROUP BY PersonNr,AnschriftNr
							Having COUNT(PersonNr) = 1) as p(PersonNr, AnschriftNr)
					JOIN CRM.[Address] AS addr 
					ON addr.LegacyId collate SQL_Latin1_General_CP1_CI_AS = AnschriftNr
					JOIN CRM.[Contact] AS contact
					ON contact.LegacyId collate SQL_Latin1_General_CP1_CI_AS = PersonNr
					)
				AS source (PersonNr, AnschriftNr,[AddressKey],[ContactKey])
			ON	targ.[AddressKey] = source.[AddressKey] 
			AND targ.[ContactKey] = source.[ContactKey]	
			WHEN MATCHED THEN
				UPDATE SET targ.[AddressKey] = source.[AddressKey]
				,targ.[ContactKey] = source.[ContactKey]
			WHEN NOT MATCHED THEN
				INSERT ([AddressKey]
				   ,[ContactKey]
				   )
				VALUES (source.[AddressKey]
				   ,source.[ContactKey]
				   );'
		EXEC(@sqlStatement)
		
		PRINT 'Importing to CRM.[ContactAddress] (Person Address Mapping) successful' + CHAR(10)
		
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'CRM.[ContactAddress]', 'Importing ContactAddresses (Person Address Mapping)';   
	END CATCH;
              
	--Don't use it
	--------------------- ==> EXECUTE AFTER INSERTING COMPANIES, PERSONS AND ADDRESSES
	--------------------DELETE CRM.Contact's with no Addresses
	------------------DISABLE TRIGGER T_CONTACT_PARENT ON CRM.Contact
	------------------GO
	------------------MERGE CRM.[Contact] AS targ
	------------------	USING CRM.ContactAddress AS source
	------------------	ON	targ.[ContactId] = source.[ContactKey] 	
	------------------	WHEN NOT MATCHED BY Source THEN
	------------------		DELETE --evtl nicht lÃ¶schen, sondern auf inaktiv setzen
	------------------	OUTPUT $action,
	------------------	 DELETED.ContactId,
	------------------	 ISNULL(NULL,'Contact has no Address');
	------------------GO
	------------------ENABLE TRIGGER T_CONTACT_PARENT ON CRM.Contact
	------------------GO

	--------------------DELETE CRM.Addresse's which are not used
	------------------MERGE CRM.[Address] AS targ
	------------------	USING CRM.ContactAddress AS source
	------------------	ON	targ.[AddressId] = source.[AddressKey] 	
	------------------	WHEN NOT MATCHED BY Source THEN
	------------------		DELETE -- evtl nicht lÃ¶schen, sondern auf inaktiv setzen
	------------------	OUTPUT $action,
	------------------	 DELETED.AddressId,
	------------------	 ISNULL(NULL,'Address is not used');

	-------------------- <==

	----NOT NEEDED FOR LEINER
	--UPDATE CRM.Contact (Konzern-Zuordnung)
	--IF EXISTS(SELECT * FROM OPENQUERY(INFORDB, 'select TOCHTERNR, COUNT(TOCHTERNR) from relKonzern GROUP BY TOCHTERNR Having COUNT(TOCHTERNR) > 1'))
	--	raiserror('<< Eine Firma kann nur einen Mutterkonzern besitzen. >>', 20, -1) WITH LOG
	--GO
	--UPDATE CRM.[Contact]
	--SET  
	--	[ParentKey] = source.[ParentCompanyId]
	--FROM (
	--	  SELECT c.ContactId, c1.ContactId FROM OPENQUERY(INFORDB, 'select * from relKonzern')
	--	  INNER JOIN (SELECT * FROM CRM.Contact WHERE ContactType = 'Company') AS c ON c.LegacyId collate database_default = MutterNr collate database_default
	--	  INNER JOIN (SELECT * FROM CRM.Contact WHERE ContactType = 'Company') AS c1 ON c1.LegacyId collate database_default = TochterNr collate database_default
	--	 )AS source([ParentCompanyId], [CompanyId])
	--WHERE [ContactId] = source.[CompanyId]


	--UPDATE CRM.Contact (Person-Firma Zuordnung)
	BEGIN TRY
		SET @sqlStatement = 'UPDATE  CRM.[Contact]
			SET [ParentKey] = source.[FirmaId]
			FROM (
				   SELECT relAddr.PersonNr, firmen.ContactId 
				   FROM OPENQUERY(' + @LinkedServer + ', ''select * from relAdresse where PersonNr is not null'') AS relAddr
				   JOIN (SELECT ContactId, LegacyId FROM CRM.Contact) AS firmen ON firmen.LegacyId collate database_default = relAddr.FirmaNr collate database_default
				 ) AS source(PersonInforId, FirmaId)
			WHERE [LegacyId]  collate database_default =source.PersonInforId collate database_default'
		EXEC(@sqlStatement)
		
		-- Now Delete all Persons which don't have a ParentCompanyId, because their Companies were not imported
		SET @sqlStatement = 'DELETE FROM CRM.Contact
			WHERE ContactType = ''Person'' AND ParentKey is null'
		EXEC(@sqlStatement)
		
		PRINT 'Setting CRM.[Contact] (Person ParentKey) successful. Deleting Persons without ParentKey successful.' + CHAR(10)
		
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'CRM.[Contact]', 'Setting CRM.[Contact] (Person ParentKey). Deleting Persons without ParentKey.';   
	END CATCH;
	


	-- Now Delete unused Addresse
	BEGIN TRY
		SET @sqlStatement = 'DELETE FROM CRM.[Address]
			WHERE AddressId NOT IN ( 
				SELECT DISTINCT AddressKey 
				FROM Crm.ContactAddress
				)'
		EXEC(@sqlStatement)
		
		PRINT 'Deleting CRM.[Address] successful' + CHAR(10)
		
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'CRM.[Address]', 'Deleting unused Addresses';   
	END CATCH;


	-- Update AddressType
	BEGIN TRY
		SET @sqlStatement = 'MERGE LU.AddressType AS targ
			USING (SELECT ISNULL(KTxt, ''''),Sprache, ZTKey FROM OPENQUERY(' + @LinkedServer + ', ''select * from RELZTNUM where TabName=''''ANSCHV1'''' AND ZTKEY IN (1,101,102,103,104)''))
			AS source (Name, Sprache, Value)
			ON (targ.[Language] collate SQL_Latin1_General_CP1_CI_AS = source.Sprache AND targ.Value = source.Value)
			WHEN MATCHED THEN 
				UPDATE SET targ.Name = source.Name, targ.[Language]=source.Sprache, targ.Value=source.value
			WHEN NOT MATCHED THEN
				INSERT (Name, [Language], Value)
				VALUES (source.Name, source.Sprache, source.Value);'
		EXEC(@sqlStatement)
		
		PRINT 'Updating LU.AddressType successful' + CHAR(10)
		
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'LU.AddressType', 'Updating AddressType';   
	END CATCH;

	-- Import Communication
	BEGIN TRY
		SET @sqlStatement = 'MERGE CRM.[Communication] AS targ
			USING(
				SELECT relKomm.ID, relKomm.Nummer, comtypes.Name, relKomm.Bemerkung, relKomm.ModifyDate, Sub.ContactKey, Sub.ContactAdrId, relKomm.Komart
				FROM OPENQUERY(' + @LinkedServer + ', ''select CAST(IK AS NVARCHAR2(32)) ID,Nummer,Bemerkung,ModifyDate,AdresseNr, Komart  from relKomm'') AS relKomm
				JOIN (
						SELECT relAddr.AdresseNr, ca.ContactAddressId, ca.ContactKey FROM CRM.ContactAddress AS ca
						JOIN CRM.[Address] AS a ON ca.AddressKey=a.AddressId
						JOIN CRM.Contact AS c ON ca.ContactKey=c.ContactId
						JOIN OPENQUERY(' + @LinkedServer + ', ''select * from relAdresse'') relAddr 
						ON c.LegacyId collate database_default = ISNULL(relAddr.PersonNr,relAddr.FirmaNr) collate database_default AND relAddr.AnschriftNr collate database_default = a.LegacyId collate database_default
					 )  
					 AS Sub (AdresseNr, ContactAdrId, ContactKey)
				ON relKomm.AdresseNr=Sub.AdresseNr
				JOIN (
						SELECT DISTINCT CommTypeGroupKey, Value, CommTypeGroupKey FROM LU.CommunicationType
					 )  
					 AS comtypes(CommTypeGroupKey,Value, Name)
				ON comtypes.Value = relKomm.Komart)
				AS source ([LegacyId],[Data],[GroupKey],[Comment],[ModifyDate],[ContactKey],[ContactAddressKey],[TypeKey])
			ON	targ.[LegacyId]  collate database_default = source.[LegacyId]
			WHEN MATCHED THEN
				UPDATE SET
					targ.[LegacyId] = source.[LegacyId]
				   ,targ.[Data] = source.[Data]
				   ,targ.[GroupKey] = source.[GroupKey]
				   ,targ.[TypeKey] = source.[TypeKey]
				   ,targ.[Comment] = source.[Comment]
				   ,targ.[ModifyDate] = ISNULL(source.ModifyDate,targ.[CreateDate])
				   ,targ.[ContactKey] = source.[ContactKey]
				   ,targ.[ContactAddressKey] = source.[ContactAddressKey]
			WHEN NOT MATCHED THEN
				INSERT(
					[LegacyId]
				   ,[Data]
				   ,[GroupKey]
				   ,[TypeKey]
				   ,[Comment]
				   ,[IsActive]
				   ,[CreateDate]
				   ,[ModifyDate]
				   ,[CreateUser]
				   ,[ModifyUser]
				   ,[ContactKey]
				   ,[ContactAddressKey])			
				VALUES (
					source.[LegacyId]
				   ,source.[Data]
				   ,source.[GroupKey]
				   ,source.[TypeKey]
				   ,source.[Comment]
				   ,1
				   ,CURRENT_TIMESTAMP
				   ,ISNULL(source.ModifyDate,CURRENT_TIMESTAMP)
				   ,''''
				   ,''''
				   ,source.[ContactKey]
				   ,source.[ContactAddressKey]);'
		EXEC(@sqlStatement)
		
		PRINT 'Importing to CRM.[Communication] successful' + CHAR(10)
			
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'CRM.[Communication]', 'Importing Communications';   
	END CATCH;

	----NOT NEEDED FOR LEINER
	----INSERT/UPDATE CRM.Positions (Mapping Persons to Positions)
	--MERGE CRM.[Position] AS targ
	--	USING(
	--		SELECT c.ContactId, p.Verwendung1
	--			FROM OPENQUERY(INFORDB, 'select * from relPerson') As p
	--			INNER JOIN CRM.Contact as c ON c.LegacyId collate database_default = p.PersonNr collate database_default
	--			)
	--		AS source (PersonKey, PositionTypeKey)
	--	ON	targ.PersonKey = source.PersonKey AND targ.PositionTypeKey = source.PositionTypeKey	
	--	WHEN MATCHED THEN
	--		UPDATE SET
	--			targ.PersonKey = source.PersonKey,
	--			targ.PositionTypeKey = source.PositionTypeKey
	--	WHEN NOT MATCHED THEN
	--		INSERT (
	--			[PersonKey],
	--			[PositionTypeKey]
	--		   )
	--		VALUES (
	--			source.PersonKey,
	--			source.PositionTypeKey
	--		   );

			   
	  
	--------UPDATE CRM.Contact contact languages  !!! THIS IS NOT A VERY GOOD SOLUTION !!!
	------IF EXISTS (select * from #temp)
	------DROP TABLE #temp 
	------GO
	------SELECT *, (ROW_NUMBER() over (order by TwoLetterLanguage) - rank() over (order by TwoLetterLanguage)) AS row_num INTO #temp
	------FROM [LU].[CultureInfo]
	------GO
	------UPDATE  CRM.[Contact]
	------SET [ContactLanguage] = t.CultureName
	------FROM CRM.Contact AS c
	------JOIN (SELECT * FROM #temp WHERE #temp.row_num=0) AS t
	------ON t.TwoLetterLanguage = c.ContactLanguage
	  
	  
	  
	  
	  
	   






	--------------------------------------------------------------------------------------------------
	-- Insert CRM.Contact / CRM.ContactAddress
	--TRUNCATE TABLE dbo.#test
	--GO
	--TRUNCATE TABLE CRM.ContactAddress

	--DECLARE @tempCA table(
	--	contactId	int not null,
	--	firmaNr		nvarchar(32) not null
	--);

	--INSERT INTO dbo.#test ([Name])
	--OUTPUT INSERTED.TestId, INSERTED.Name
	--INTO @tempCA
	--SELECT relAddr.FirmaNr FROM CRM.[Address] as addr
	--	JOIN OPENQUERY(INFORDB, 'select * from relAdresse where PersonNr is null') as relAddr
	--	ON addr.LegacyId collate SQL_Latin1_General_CP1_CI_AS = relAddr.AnschriftNr
	--	JOIN OPENQUERY(INFORDB, 'select * from relFirma') as firma
	--	ON relAddr.FirmaNr = firma.FirmaNr

	--INSERT INTO CRM.ContactAddress ([AddressKey],[ContactKey])
	--Select ISNULL(addr.AddressId,11111),contactId 
	--	FROM @tempCA
	--	JOIN CRM.[Address] AS addr 
	--	ON addr.LegacyId collate SQL_Latin1_General_CP1_CI_AS = firmaNr


	--CREATE TABLE #test
	--(
	--	[TestId] [int] IDENTITY(1,1) NOT NULL,
	--	[Name] [nvarchar](120) NOT NULL
	--)

	--select * from dbo.#test
	--where TestId=982


	 --DELETE Duplicate Entries (ContactId = UniqueId) Group By Columns = Duplicate Entries
	--DELETE FROM [Crm].[Contact] 
	--WHERE ContactId NOT IN(
	--SELECT MAX(ContactId) 
	--FROM [Crm].[Contact] 
	--GROUP BY ParentKey, LegacyId, Name);

	--TRUNCATE Table CRM.[Communication]
	--GO
	--INSERT/UPDATE CRM.Communication	

END
GO
