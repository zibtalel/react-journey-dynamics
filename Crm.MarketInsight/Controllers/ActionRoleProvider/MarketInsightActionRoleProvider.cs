namespace Crm.MarketInsight.Controllers.ActionRoleProvider
{
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;
	using Crm.MarketInsight.Model;
	using Crm.MarketInsight.Model.Lookups;
	using Crm.MarketInsight.Model.Relationships;

	public class MarketInsightActionRoleProvider : RoleCollectorBase
	{
		public MarketInsightActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			Add(MarketInsightPlugin.PermissionGroup.MarketInsight, PermissionName.Read, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
			Add(MarketInsightPlugin.PermissionGroup.MarketInsight, PermissionName.Create, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
			Add(MarketInsightPlugin.PermissionGroup.MarketInsight, PermissionName.Edit, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
			Add(MarketInsightPlugin.PermissionGroup.MarketInsight, MarketInsightPlugin.PermissionName.SetStatus, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
			Add(MarketInsightPlugin.PermissionGroup.MarketInsight, PermissionName.View, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
			Add(MarketInsightPlugin.PermissionGroup.MarketInsight, MainPlugin.PermissionName.RelationshipsTab, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
			Add(MarketInsightPlugin.PermissionGroup.MarketInsight, MarketInsightPlugin.PermissionName.EditContactRelationship, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
			Add(MarketInsightPlugin.PermissionGroup.MarketInsight, MarketInsightPlugin.PermissionName.RemoveContactRelationship, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);

			Add(PermissionGroup.WebApi, nameof(MarketInsight), MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
			Add(PermissionGroup.WebApi, nameof(MarketInsightReference), MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
			Add(PermissionGroup.WebApi, nameof(MarketInsightStatus), MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
			Add(PermissionGroup.WebApi, nameof(MarketInsightContactRelationshipType), MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
			Add(PermissionGroup.WebApi, nameof(MarketInsightContactRelationship), MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);

			Add(MainPlugin.PermissionGroup.Company, MarketInsightPlugin.PermissionName.MarketInsightTab, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
		}
	}
}