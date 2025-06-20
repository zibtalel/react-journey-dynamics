/****** Object:  StoredProcedure [dbo].[ImportInforEquipmentFile]    Script Date: 07/28/2011 09:58:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportInforEquipmentFile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ImportInforEquipmentFile]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ImportInforEquipmentFile]
	@LinkedServer [nvarchar](500),
	@DeleteBefore [bit]
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sqlStatement AS nvarchar(max)

	IF @DeleteBefore = 1
		BEGIN
			DELETE FROM SMS.[InstallationHead]
		END;

	BEGIN TRY
		SET @sqlStatement = 'DECLARE @outputTable TABLE (
									MachineNo nvarchar(50),
									LocationContactId int,
									Description nvarchar(150),
									LocationAddressKey int,
									ContactKey int
								)

								MERGE CRM.Contact AS targ
											USING(
												SELECT cam.MachineNo, cam.CustomerNr, cam.Description, c.ContactId, a.AddressId
													FROM OPENQUERY(' + @LinkedServer + ', ''SELECT c1.CAMID AS MachineNo, c1.CustomerNr, c1.Description, s.AnschriftNr
																	FROM relCAM c1 
																	INNER JOIN (SELECT 
																					CAMID, 
																					MAX(HistoryId) hid 
																					FROM relCAM GROUP BY CAMID) c2 
																	ON c1.CAMID = c2.CAMID AND c1.Historyid = c2.hid
																	INNER JOIN relCAMPos cp 
																	ON c1.CAMID = cp.CAMID 
																	INNER JOIN relAdresse a
																	ON c1.AddressNr = a.AdresseNr
																	INNER JOIN relAnsch s
																	ON a.AnschriftNr = s.AnschriftNr
																	AND cp.IBStructLevel = 0 
																	AND cp.AbsPosNr = 1 
																	AND cp.RowType0 = 90
																	LEFT OUTER JOIN relZTISvcCAMStatus camstat
																	ON c1.Status = camstat.ZTKey
																	AND camstat.Sprache = ''''us''''
																	WHERE c1.CAMType = 0
																	'') AS cam
													JOIN CRM.Contact c ON cam.CustomerNr collate database_default = c.LegacyId collate database_default 
													JOIN CRM.Address a ON cam.AnschriftNr collate database_default = a.LegacyId collate database_default)		
													AS source ([InstallationNo],[LocationContactId],[Description], [ContactId], [AddressId])
											ON	targ.Name collate database_default = source.InstallationNo collate database_default
											WHEN NOT MATCHED THEN
												INSERT ([ContactType]
														,[Name]
														,[CreateDate]
														,[ModifyDate]
														,[CreateUser]
														,[ModifyUser]
													   )
												VALUES (''Installation''
													   ,source.InstallationNo
													   ,getdate()
													   ,getdate()
													   ,''Import tool''
													   ,''Import tool''
													   )
												OUTPUT source.InstallationNo, inserted.ContactId, source.Description, source.AddressId, source.ContactId INTO @outputTable;

								INSERT INTO SMS.InstallationHead (InstallationNo, LocationContactId, Description, Priority, InstallationType, RemoteControlType, Status, CreateDate, ModifyDate, LocationAddressKey, ContactKey, CreateUser, ModifyUser, Favorite, SortOrder)				
								SELECT MachineNo, ContactKey, COALESCE(Description, ''''), 0, 0, 0, 2, getdate(), getdate(), LocationAddressKey, LocationContactId, ''Import tool'', ''Import tool'', 0, 0 From @outputTable'
		EXEC(@sqlStatement)
		
		PRINT 'Importing to SMS.[InstallationHead] successful' + CHAR(10)
		
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'SMS.[InstallationHead]', 'Importing InforEquipmentFile';   
	END CATCH;
END
GO
