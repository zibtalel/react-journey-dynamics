/****** Object:  StoredProcedure [dbo].[ImportInforDocumentsNoServiceRelFb]    Script Date: 07/28/2011 09:58:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportInforDocumentsNoServiceRelFb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ImportInforDocumentsNoServiceRelFb]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ImportInforDocumentsNoServiceRelFb]
	@Discrimator [nvarchar](50),
	@Criteria [nvarchar](400),
	@LinkedServer [nvarchar](500),
	@Locale [nvarchar](4)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sqlStatement AS nvarchar(4000)
	DECLARE @inforStatement AS nvarchar(4000)

	SET @inforStatement = 'SELECT a.*, 
							zust2.KTxt as State2, 
							zust4.KTxt as State4, 
							zust5.KTxt as State5, 
							zust6.KTxt as State6
							FROM relFb a
							LEFT OUTER JOIN RELZTZUSTDART zust2
							ON a.Segm2_ZArt = zust2.ZTKey
							LEFT OUTER JOIN RELZTZUSTDART zust4
							ON a.Segm4_ZArt = zust4.ZTKey
							LEFT OUTER JOIN RELZTZUSTDART zust5
							ON a.Segm5_ZArt = zust5.ZTKey
							LEFT OUTER JOIN RELZTZUSTDART zust6
							ON a.Segm6_ZArt = zust6.ZTKey
							WHERE 1=1
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
			CASE fb.Zust
				WHEN 2 THEN
					State2
				WHEN 4 THEN
					State4
				WHEN 5 THEN
					State5
				WHEN 6 THEN
					State6
			END AS DocumentState,
			NULL AS DocumentDate11,
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
		PRINT 'Importing to CRM.InforDocument from relFb successful.' + CHAR(10);
	END TRY
	BEGIN CATCH
		EXECUTE dbo.CrmImportLog 'CRM.InforDocument', 'Importing to CRM.InforDocument from relFb';   
	END CATCH;
END
GO
