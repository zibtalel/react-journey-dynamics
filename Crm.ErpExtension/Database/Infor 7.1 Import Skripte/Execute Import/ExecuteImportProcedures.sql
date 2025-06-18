DECLARE @LinkedServer AS NVARCHAR(50)
SET @LinkedServer = '<YOUR LINKED SERVER FROM "linked server.sql"'

/* 1. Import Lookup-Tables */
EXEC ImportLuFromInfor @LinkedServer, '0'

/* 2. Import Crm-Tables  */
EXEC ImportCrmFromInfor @LinkedServer, '0'

/* 3. Import Tags*/
EXEC ImportCrmTags 'de', '1'

/* 4. Import InforDocuments */
DELETE [CRM].[InforDocument]
WHERE 1=1
PRINT 'Deleting CRM.InforDocument successful.'

EXEC ImportInforDocumentsNoServiceRelFb 'SalesOrder', 'a.SAInt = 10 And SoKnz1 = 0', @LinkedServer, 'de'

EXEC ImportInforDocumentsRelFbX 'Quotes', 'relFbA', 'a.SAInt = 10 And a.Segm1_ZArt <> 185', @LinkedServer, 'de'

EXEC ImportInforDocumentsRelFbX 'OrderConfirmation', 'relFbB', 'a.SAInt = 10', @LinkedServer, 'de'

EXEC ImportInforDocumentsRelFbX 'DeliveryNote', 'relFbL', 'a.SAInt = 210', @LinkedServer, 'de'

EXEC ImportInforDocumentsRelFbX 'Invoice', 'relFbR', 'a.SAInt = 210', @LinkedServer, 'de'

EXEC ImportInforEquipmentFileAndMaintenanceContract @LinkedServer, '0'

EXEC ImportArticles @LinkedServer, '0'

EXEC ImportInforEquipmentFile @LinkedServer, '0'