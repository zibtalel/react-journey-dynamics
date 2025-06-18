Infor Import Scripts for 7.1 (with Service)

1. Install Oracle InstantClient and establish a connection to the Oracle server. Detailled Installation instructions can be found here http://blog.l-mobile.com/article/oracle-instant-client
2. Edit the linked server script and replace the @ServerName and @Instance variables to reflect your current environment
3. Run the Scripts in this Order
	- Linked Servers
	- Tables
	- Stored Procedures
	
4. Edit the File "Execute Import\ExecuteImportProcedures.sql" and set the @LinkedServer variable as needed
5. Now you are ready to run the import from Infor:COM with the help of Execute Import\ExecuteImportProcedures.sql

6. You can alter all individual procedures later to make sure to reflect your actual environment and business needs...