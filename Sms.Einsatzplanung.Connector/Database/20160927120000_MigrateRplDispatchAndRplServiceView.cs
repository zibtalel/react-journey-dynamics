namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20160927120000)]
	public class MigrateRplDispatchAndRplServiceView : Migration
	{
		public override void Up()
		{
			if (Database.ColumnExists("RPL.Dispatch", "InternalId") == false)
			{
				Database.ExecuteNonQuery(@"
					ALTER TABLE [RPL].[Dispatch]
					ADD InternalId UNIQUEIDENTIFIER NOT NULL DEFAULT(NEWID())
				");
			}

			//Database.ExecuteNonQuery(@"
			//	IF EXISTS(SELECT 1 FROM sys.views WHERE object_id = OBJECT_ID('RPL.Service'))
			//	BEGIN
			//			DROP VIEW [RPL].[Service]
			//	END
			//");

			//Database.ExecuteNonQuery(@"
			//	CREATE VIEW [RPL].[Service]
			//	AS
			//	SELECT DISTINCT sh.ContactKey AS[OrderId],
			//		sh.OrderNo,
			//		c.LegacyId AS[CustomerNo],
			//		c.Name AS[Customer],
			//		com.ShortText AS[CustomerShortText],
			//		sh.[Status] As[StatusKey],
			//		ss.Name AS[Status],
			//		sh.OrderType As[TypeKey],
			//		st.Name As[Type],
			//		sh.ErrorMessage,
			//		c2.BackgroundInfo AS ErrorDescription,
			//		sh.[Priority] As PriorityKey,
			//		p.Name As[Priority],
			//		COALESCE(sh.Street, a.Street) AS Street,
			//		COALESCE(sh.City, a.City) AS City,
			//		COALESCE(sh.ZipCode, a.ZipCode) AS ZipCode,
			//		sh.Latitude,
			//		sh.Longitude,
			//		lc.Name AS Country,
			//		lr.Name AS Region,
			//		ih.InstallationNo,
			//		ih.LegacyInstallationId AS SerialNo,
			//		ih.[Description] AS[InstallationDescription],
			//		ih.InstallationType,
			//		COALESCE(it.[Name], ih.[InstallationType] collate database_default) AS[InstallationTypeName],
			//		it.[GroupKey] AS[InstallationGroup],
			//		ih.[Status] AS[InstallationStatusKey],
			//		i.[Name] AS[InstallationStatus],
			//		ih.[PreferredUser] AS[PreferredUser],
			//		sh.StationKey,
			//		s.Name AS Station,
			//		sh.Reported,
			//		sh.Planned,
			//		sh.PlannedTime,
			//		sh.PlannedDateFix,
			//		(SELECT SUM(EstimatedDuration) FROM SMS.ServiceOrderTimes WHERE OrderNo = sh.OrderNo AND IsActive = 1) AS EstimatedDuration,
			//		sh.Deadline, 
			//		sh.ServiceContractNo, 
			//		c2.CreateUser, 
			//		c2.CreateDate, 
			//		c2.ModifyDate, 
			//		c2.ModifyUser,
			//		NULL AS [InternalInformation]
			//	FROM SMS.[ServiceOrderHead] sh
			//	JOIN CRM.[Contact] c2 ON sh.ContactKey = c2.ContactId AND c2.ContactType IN ('ServiceOrder')
			//	LEFT OUTER JOIN SMS.[InstallationHead] ih ON ih.InstallationNo = sh.InstallationNo
			//	LEFT OUTER JOIN SMS.[ServiceOrderDispatch] d ON sh.ContactKey = d.OrderId
			//	LEFT OUTER JOIN SMS.[InstallationHeadStatus] i ON ih.[Status] = i.Value AND i.[Language] = 'de'
			//	LEFT OUTER JOIN SMS.[InstallationType] it ON ih.InstallationType = it.Value AND it.[Language] = 'de'
			//	LEFT OUTER JOIN SMS.[ServiceOrderStatus] ss ON sh.[Status] = ss.Value AND ss.[Language] = 'de'
			//	LEFT OUTER JOIN SMS.[ServiceOrderType] st ON sh.[OrderType] = st.Value AND st.[Language] = 'de'
			//	LEFT OUTER JOIN SMS.[ServicePriority] p ON sh.[Priority] = p.Value AND p.[Language] = 'de'
			//	LEFT OUTER JOIN CRM.[Contact] c ON ih.LocationContactId = c.ContactId AND c.ContactType IN ('Company')
			//	LEFT OUTER JOIN CRM.[Company] com ON c.ContactId = com.ContactKey
			//	LEFT OUTER JOIN CRM.[Address] a ON ih.LocationAddressKey = a.AddressId AND a.Street IS NOT NULL
			//	LEFT OUTER JOIN CRM.[Station] s ON sh.StationKey = s.StationId
			//	LEFT OUTER JOIN LU.[Country] lc ON COALESCE(sh.CountryKey, a.CountryKey) = lc.Value AND lc.[Language] = 'de' 
			//	LEFT OUTER JOIN LU.[Region] lr ON a.RegionKey = lr.Value AND lr.[Language] = 'de'
			//	WHERE 1=1
			//		AND sh.OrderNo IS NOT NULL
			//");
		}
	}
}