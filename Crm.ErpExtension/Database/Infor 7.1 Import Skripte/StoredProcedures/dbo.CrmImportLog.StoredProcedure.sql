/****** Object:  StoredProcedure [dbo].[CrmImportLog]    Script Date: 07/28/2011 09:58:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CrmImportLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CrmImportLog]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CrmImportLog]
	@TableName [nvarchar](500),
	@ImportInfo [nvarchar](4000)
AS
    PRINT 
		'ERROR: Error when importing into table ''' + @TableName + ''' (Database: ' + db_name() + ' )' + CHAR(10) +
		' > MESSAGE		: "' + CONVERT(VARCHAR(MAX), ERROR_MESSAGE()) + CHAR(10) +
        ' > MORE INFO	: ErrorNo' + CONVERT(VARCHAR(50), ERROR_NUMBER()) +
        ', Severity ' + CONVERT(VARCHAR(5), ERROR_SEVERITY()) +
        ', State ' + CONVERT(VARCHAR(5), ERROR_STATE()) + 
        ', Line ' + CONVERT(VARCHAR(5), ERROR_LINE()) + CHAR(10) +
        ' > IMPORTINFO	: ' + @ImportInfo + CHAR(10) + CHAR(10);
    --Rollback transaction;
GO
