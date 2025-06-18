/****** Object:  LinkedServer  Script Date: 01/07/2011 12:32:15 ******/
DECLARE @ServerName AS NVARCHAR(50)
DECLARE @Instance AS NVARCHAR(50)
SET @ServerName = N'<NAME OF THE LINKED SERVER>'
SET @Instance = N'<INFORINSTANCE FROM TNSNAMES.ORA>'

EXEC master.dbo.sp_addlinkedserver @server = @ServerName, @srvproduct=N'ORACLE', @provider=N'MSDAORA', @datasrc=@Instance
 /* For security reasons the linked server remote logins password is changed with ######## */
EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=@ServerName,@useself=N'False',@locallogin=NULL,@rmtuser=N'infor',@rmtpassword='sysm'

EXEC master.dbo.sp_serveroption @server=@ServerName, @optname=N'collation compatible', @optvalue=N'false'

EXEC master.dbo.sp_serveroption @server=@ServerName, @optname=N'data access', @optvalue=N'true'

EXEC master.dbo.sp_serveroption @server=@ServerName, @optname=N'dist', @optvalue=N'false'

EXEC master.dbo.sp_serveroption @server=@ServerName, @optname=N'pub', @optvalue=N'false'

EXEC master.dbo.sp_serveroption @server=@ServerName, @optname=N'rpc', @optvalue=N'false'

EXEC master.dbo.sp_serveroption @server=@ServerName, @optname=N'rpc out', @optvalue=N'false'

EXEC master.dbo.sp_serveroption @server=@ServerName, @optname=N'sub', @optvalue=N'false'

EXEC master.dbo.sp_serveroption @server=@ServerName, @optname=N'connect timeout', @optvalue=N'0'

EXEC master.dbo.sp_serveroption @server=@ServerName, @optname=N'collation name', @optvalue=null

EXEC master.dbo.sp_serveroption @server=@ServerName, @optname=N'lazy schema validation', @optvalue=N'false'

EXEC master.dbo.sp_serveroption @server=@ServerName, @optname=N'query timeout', @optvalue=N'0'

EXEC master.dbo.sp_serveroption @server=@ServerName, @optname=N'use remote collation', @optvalue=N'true'

EXEC master.dbo.sp_serveroption @server=@ServerName, @optname=N'remote proc transaction promotion', @optvalue=N'true'