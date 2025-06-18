namespace Crm.ErpExtension
{
	using Crm.ErpExtension.Model;
	using Crm.Library.Configuration;
	using Crm.Library.Modularization;

	[Plugin(ModuleId = "FLD03070", Requires = "Main,Crm.Article")]
	public class ErpPlugin : Plugin
	{
		public static readonly string PluginName = typeof(ErpPlugin).Namespace;

		public static class PermissionGroup
		{
			public const string CreditNoteList = "CreditNoteList";
			public const string DeliveryNoteList = "DeliveryNoteList";
			public const string Integration = "Integration";
			public const string InvoiceList = "InvoiceList";
			public const string MasterContractList = "MasterContractList";
			public const string MasterContractPositionList = "MasterContractPositionList";
			public const string QuoteList = "QuoteList";
			public const string QuotePositionList = "QuotePositionList";
			public const string SalesOrderList = "SalesOrderList";
			public const string SalesOrderPositionList = "SalesOrderPositionList";
			public const string Erp = "Erp";
			public const string Project = "Project";
		}

		public static class PermissionName
		{
			public const string ErpDocumentsTab = "ErpDocumentsTab";
			public const string TurnoverTab = "TurnoverTab";
			public const string OpenCompany = "OpenCompany";
			public const string DocumentSummary = "DocumentSummary";
			public const string BackgroundInformationExtension = "BackgroundInformationExtension";
			public const string OpenPerson = "OpenPerson";
			public const string TurnoverTransaction = "TurnoverTransaction";
			public const string TurnoverTransactionDetails = "TurnoverTransactionDetails";
		}

		public static class Settings
		{
			public static class Export
			{
				public static SettingDefinition<bool> EnableCompanyExport => new SettingDefinition<bool>("EnableCompanyExport", PluginName);
				public static SettingDefinition<bool> EnableAddressExport => new SettingDefinition<bool>("EnableAddressExport", PluginName);
				public static SettingDefinition<bool> EnablePersonExport => new SettingDefinition<bool>("EnablePersonExport", PluginName);
				public static SettingDefinition<bool> EnableCommunicationExport => new SettingDefinition<bool>("EnableCommunicationExport", PluginName);
			}

			public static class System
			{
				/// <summary>
				/// SystemID from VPPS.INI
				/// </summary>
				public static SettingDefinition<string> ErpSystemID => new SettingDefinition<string>("ErpSystemID", PluginName);
				/// <summary>
				/// Client/Instance for SAP
				/// </summary>
				public static SettingDefinition<string> ErpSystemName => new SettingDefinition<string>("ErpSystemName", PluginName);
				/// <summary>
				/// InforObjectLink for iol, D3Link for d.velop dms, SapLink for sap
				/// </summary>
				public static SettingDefinition<ObjectLinkIntegration> ObjectLinkIntegration => new SettingDefinition<ObjectLinkIntegration>("ObjectLinkIntegration", PluginName);
				public static SettingDefinition<string> TurnoverCurrencyKey => new SettingDefinition<string>("TurnoverCurrencyKey", PluginName);
			}
		}
	}
}
