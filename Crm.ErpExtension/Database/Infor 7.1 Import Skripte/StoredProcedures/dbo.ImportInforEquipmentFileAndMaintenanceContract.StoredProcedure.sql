/****** Object:  StoredProcedure [dbo].[ImportInforEquipmentFileAndMaintenanceContract]    Script Date: 07/28/2011 09:58:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportInforEquipmentFileAndMaintenanceContract]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ImportInforEquipmentFileAndMaintenanceContract]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[ImportInforEquipmentFileAndMaintenanceContract]
	@LinkedServer [nvarchar](500),
	@DeleteBefore [bit]
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sqlStatement AS nvarchar(max)

	IF @DeleteBefore = 1
		BEGIN
			DELETE FROM CRM.[InforEquipmentFile]
			DELETE FROM CRM.[InforMaintenanceContract]		
		END;

	BEGIN TRY
		SET @sqlStatement = 'MERGE CRM.[InforEquipmentFile] AS targ
			USING(
				SELECT *
					FROM OPENQUERY(' + @LinkedServer + ', ''SELECT c1.CAMID as MachineNo, c1.Description, c1.CustomerNr, c1.Commission,
										cp.SerialNo, cp.BuildingDate as AssemblyDate, cp.WarrantyFrom, cp.WarrantyUntil, camstat.ZTKey
									FROM relCAM c1 
									INNER JOIN (SELECT 
													CAMID, 
													MAX(HistoryId) hid 
													FROM relCAM GROUP BY CAMID) c2 
									ON c1.CAMID = c2.CAMID AND c1.Historyid = c2.hid
									INNER JOIN relCAMPos cp 
									ON c1.CAMID = cp.CAMID 
									AND cp.IBStructLevel = 0 
									AND cp.AbsPosNr = 1 
									AND cp.RowType0 = 90
									LEFT OUTER JOIN relZTISvcCAMStatus camstat
									ON c1.Status = camstat.ZTKey
									AND camstat.Sprache = '+'''''us'''''+'
									WHERE c1.CAMType = 0
									''))
					AS source ([MachineNo],[Description],[CustomerNo],[Commission],[SerialNo],[AssemblyDate],[WarrantyFrom],[WarrantyUntil],[State])
			ON	targ.MachineNo collate SQL_Latin1_General_CP1_CI_AS = source.MachineNo	
			WHEN MATCHED THEN
				UPDATE SET targ.[MachineNo] = source.MachineNo
					   ,targ.[Description] = source.Description
					   ,targ.[CustomerNo] = source.CustomerNo
					   ,targ.[Commission] = source.Commission
					   ,targ.[SerialNo] = source.SerialNo
					   ,targ.[AssemblyDate] = source.AssemblyDate
					   ,targ.[WarrantyFrom] = source.WarrantyFrom
					   ,targ.[WarrantyUntil] = source.WarrantyUntil
					   ,targ.[State] = source.State
			WHEN NOT MATCHED THEN
				INSERT ([MachineNo]
					   ,[Description]
					   ,[CustomerNo]
					   ,[Commission]
					   ,[SerialNo]
					   ,[AssemblyDate]
					   ,[WarrantyFrom]
					   ,[WarrantyUntil]
					   ,[State]
					   )
				VALUES (source.MachineNo
					   ,source.Description
					   ,source.CustomerNo
					   ,source.Commission
					   ,source.SerialNo
					   ,source.AssemblyDate
					   ,source.WarrantyFrom
					   ,source.WarrantyUntil
					   ,source.State
					   );'
		EXEC(@sqlStatement)
		
		PRINT 'Importing to CRM.[InforEquipmentFile] successful' + CHAR(10)
		
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'CRM.[InforEquipmentFile]', 'Importing InforEquipmentFile';   
	END CATCH;

	
	
	BEGIN TRY
		SET @sqlStatement = 'MERGE CRM.[InforMaintenanceContract] AS targ
			USING(
				SELECT	*
				FROM OPENQUERY(' + @LinkedServer + ', ''SELECT s.SMANo, f.UTNr, s.CAMID, smastate.KTxt, s.CtrBeginDate, s.CtrExpDate, 
														sa.SLAId, sa.ShortText, p.LastServiceDt, 
														p.NextServiceDt, p.LastSOClosingDt, f.Netto2, f.WE,
														f.BelegDatBest,
														CASE f.Zust
														WHEN 2 THEN
															zust2.KTxt
														WHEN 4 THEN
															zust4.KTxt
														WHEN 5 THEN
															zust5.KTxt
														WHEN 6 THEN
															zust6.KTxt
														END AS DocumentState
											FROM relISvcSMAIB s
													INNER JOIN relFb f 
													ON s.SMANo = f.ANr AND f.SAint = 9
													INNER JOIN relSalesISvc sa 
													ON sa.MakerID = f.RNr
													LEFT OUTER JOIN relISvcASPPlan p 
													ON s.SMANo = p.SMANo
													LEFT OUTER JOIN relZTNum smastate 
													ON s.SMAIBStatus = smastate.ZTKey		
													LEFT OUTER JOIN relZTNum zust2
													ON f.Segm2_ZArt = zust2.ZTKey
													LEFT OUTER JOIN relZTNum zust4
													ON f.Segm4_ZArt = zust4.ZTKey
													LEFT OUTER JOIN relZTNum zust5
													ON f.Segm5_ZArt = zust5.ZTKey
													LEFT OUTER JOIN relZTNum zust6
													ON f.Segm6_ZArt = zust6.ZTKey
													WHERE s.IPos = 1
													AND smastate.TabName = '+'''''ISVCSMAIBST'''''+'
													AND smastate.Sprache = '+'''''us'''''+'
													AND zust2.TABNAME = '+'''''ZUSTDART'''''+' 
													AND zust4.TABNAME = '+'''''ZUSTDART'''''+' 
													AND zust5.TABNAME = '+'''''ZUSTDART'''''+' 
													AND zust6.TABNAME = '+'''''ZUSTDART'''''+' 
													AND zust2.Sprache = '+'''''us'''''+'
													AND zust4.Sprache = '+'''''us'''''+'
													AND zust5.Sprache = '+'''''us'''''+'
													AND zust6.Sprache = '+'''''us'''''+' ''))

					AS source ([ContractNo],[CustomerNo],[MachineNo],[State],[BeginDate],[ExpirationDate],[ServiceLevelAgreementKey],[ServiceLevelDescription],[LastServiceDate],[NextServiceDate],[LatestServiceDate],[Total],[Currency],[Date],[DocumentState])
			ON	targ.ContractNo collate SQL_Latin1_General_CP1_CI_AS = source.ContractNo	
			WHEN MATCHED THEN
				UPDATE SET targ.[ContractNo] = source.ContractNo
					   ,targ.[CustomerNo] = source.CustomerNo
					   ,targ.[MachineNo] = source.MachineNo
					   ,targ.[State] = source.State
					   ,targ.[BeginDate] = source.BeginDate
					   ,targ.[ExpirationDate] = source.ExpirationDate
					   ,targ.[ServiceLevelAgreementKey] = source.ServiceLevelAgreementKey
					   ,targ.[ServiceLevelDescription] = source.ServiceLevelDescription
					   ,targ.[LastServiceDate] = source.LastServiceDate
					   ,targ.[NextServiceDate] = source.NextServiceDate
					   ,targ.[LatestServiceDate] = source.LatestServiceDate
					   ,targ.[Total] = source.Total
					   ,targ.[Currency] = source.Currency
					   ,targ.[Date] = source.Date
					   ,targ.[DocumentState] = source.DocumentState
			WHEN NOT MATCHED THEN
				INSERT ([ContractNo]
					   ,[CustomerNo]
					   ,[MachineNo] 
					   ,[State]
					   ,[BeginDate] 
					   ,[ExpirationDate] 
					   ,[ServiceLevelAgreementKey] 
					   ,[ServiceLevelDescription] 
					   ,[LastServiceDate] 
					   ,[NextServiceDate] 
					   ,[LatestServiceDate] 
					   ,[Total] 
					   ,[Currency] 
					   ,[Date] 
					   ,[DocumentState] 
					   )
				VALUES (source.ContractNo
					   ,source.CustomerNo
					   ,source.MachineNo
					   ,source.State
					   ,source.BeginDate
					   ,source.ExpirationDate
					   ,source.ServiceLevelAgreementKey
					   ,source.ServiceLevelDescription
					   ,source.LastServiceDate
					   ,source.NextServiceDate
					   ,source.LatestServiceDate
					   ,source.Total
					   ,source.Currency
					   ,source.Date
					   ,source.DocumentState
					   );'
		EXEC(@sqlStatement)
		
		PRINT 'Importing to CRM.[InforMaintenanceContract] successful' + CHAR(10)
		
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'CRM.[InforMaintenanceContract]', 'Importing InforMaintenanceContract';   
	END CATCH;

END
GO
