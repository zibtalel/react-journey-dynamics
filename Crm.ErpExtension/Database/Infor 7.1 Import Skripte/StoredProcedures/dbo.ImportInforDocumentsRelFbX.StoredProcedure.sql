/****** Object:  StoredProcedure [dbo].[ImportInforDocumentsRelFbX]    Script Date: 07/28/2011 09:58:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportInforDocumentsRelFbX]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ImportInforDocumentsRelFbX]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ImportInforDocumentsRelFbX]
	-- Add the parameters for the stored procedure here
	@Discrimator [nvarchar](50),
	@InforTable [nvarchar](50),
	@Criteria [nvarchar](400),
	@LinkedServer [nvarchar](500),
	@Locale [nvarchar](4)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @sqlStatement AS nvarchar(4000)
	DECLARE @inforStatement AS nvarchar(4000)
	DECLARE @message AS nvarchar(4000)
	
	SET @inforStatement = 'SELECT a.*, 
							zust2.KTxt as State2, 
							zust4.KTxt as State4, 
							zust5.KTxt as State5, 
							zust6.KTxt as State6'
	IF Lower(@InforTable) = 'relfba'
	BEGIN
		SET @inforStatement = @inforStatement + ',zust1.KTxt as State1'
	END
	SET @inforStatement = @inforStatement +
							'
							FROM ' + @InforTable + ' a'
							
	IF Lower(@InforTable) = 'relfba'
	BEGIN
		SET @inforStatement = @inforStatement +
							'
							LEFT OUTER JOIN RELZTZUSTDART zust1
							ON a.Segm1_ZArt = zust1.ZTKey
							'
	END
	
	SET @inforStatement = @inforStatement +
							'
							LEFT OUTER JOIN RELZTZUSTDART zust2
							ON a.Segm2_ZArt = zust2.ZTKey
							LEFT OUTER JOIN RELZTZUSTDART zust4
							ON a.Segm4_ZArt = zust4.ZTKey
							LEFT OUTER JOIN RELZTZUSTDART zust5
							ON a.Segm5_ZArt = zust5.ZTKey
							LEFT OUTER JOIN RELZTZUSTDART zust6
							ON a.Segm6_ZArt = zust6.ZTKey
							WHERE 1=1  '
							
	IF Lower(@InforTable) = 'relfba'
	BEGIN
		SET @inforStatement = @inforStatement +
							'
							AND zust1.Sprache = ''''' + @Locale + '''''
							'
	END
	
	SET @inforStatement = @inforStatement +
							'
							AND a.ANr is not NULL
							AND zust2.Sprache = ''''' + @Locale + '''''
							AND zust4.Sprache = ''''' + @Locale + '''''
							AND zust5.Sprache = ''''' + @Locale + '''''
							AND zust6.Sprache = ''''' + @Locale + ''''' 
							AND ' + @Criteria
	
	SET @sqlStatement = '
    INSERT INTO [CRM].[InforDocument]
           ([DocumentType]
           ,[OrderNo]
           ,[DocumentState]
           ,[DocumentServiceState]
           ,[DocumentServiceType]
           ,[DocumentDate11]
           ,[CompanyNo]
           ,[RecordId]
           ,[RecordType]
           ,[QuoteNo]
           ,[QuoteDate]
           ,[RequestNo]
           ,[RequestDate]
           ,[OrderConfirmationNo]
           ,[OrderConfirmationDate]
           ,[DeliverNoteNo]
           ,[DeliveryNoteDate]
           ,[InvoiceNo]
           ,[InvoiceDate]
           ,[ItemNo]
           ,[Total]
           ,[Total wo Taxes]
           ,[Currency]
           ,[State]
           ,[Commission]
           ,[DueDate])
	SELECT ''' + @Discrimator + ''' as DocumentType,
			fb.ANr,
			CASE fb.Zust'
			
	IF Lower(@InforTable) = 'relfba'
	BEGIN
		SET @sqlStatement = @sqlStatement +
					'
						WHEN 1 THEN
							State1
					'
	END
		
	SET @sqlStatement = @sqlStatement +
			'
				WHEN 2 THEN
					State2
				WHEN 4 THEN
					State4
				WHEN 5 THEN
					State5
				WHEN 6 THEN
					State6
			END AS DocumentState,
			NULL as [DocumentServiceState],
			NULL as [DocumentServiceType],'
			
	IF Lower(@InforTable) = 'relfba'
	BEGIN
		SET @sqlStatement = @sqlStatement +
					'fb.BelegDat11,'
	END
	ELSE
	BEGIN
		SET @sqlStatement = @sqlStatement +
					'NULL AS DocumentDate11,'
	END
		
	SET @sqlStatement = @sqlStatement +
			'
			fb.UTNr,
			fb.RNr,
			fb.SAInt,
			fb.BelegNrAng,		 
			fb.BelegDatAng,
			fb.BelegNrAnfr,
			fb.BelegDatAnfr,
			fb.BelegNrAuft,
			fb.BelegDatAuft,
			fb.BelegNrLief,
			fb.BelegDatLief,
			fb.BelegNrRech,
			fb.BelegDatRech,
			fb.MNr,
			fb.Brutto,
			fb.Netto2,
			fb.WE,
			fb.Zust,
			fb.KOMM,
			fb.Segm1_Term
	FROM OPENQUERY(' + @LinkedServer + ', ''' + @inforStatement + ''') AS fb'
	
	BEGIN TRY
		--PRINT(@sqlStatement)
		EXEC(@sqlStatement)
		PRINT 'Importing to CRM.InforDocument from ' + @InforTable + ' successful.' + CHAR(10);
	END TRY
	BEGIN CATCH
		SET @message = 'Importing from ' + @InforTable;
		EXECUTE dbo.CrmImportLog 'CRM.InforDocument', @message;   
	END CATCH;
END
GO
