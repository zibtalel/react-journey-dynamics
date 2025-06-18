namespace Crm.ProjectOrders.Controllers.ActionRoleProvider
{
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Order;
	using Crm.Project;

	public class ProjectOrderActionRoleProvider : RoleCollectorBase
	{
		public ProjectOrderActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			Add(ProjectPlugin.PermissionGroup.Project, ProjectOrderPlugin.Permission.CreateOffer, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice, "HeadOfService", "ServiceBackOffice", "InternalService");
			AddImport(ProjectPlugin.PermissionGroup.Project, ProjectOrderPlugin.Permission.CreateOffer, OrderPlugin.PermissionGroup.Offer, PermissionName.Create);
			Add(ProjectPlugin.PermissionGroup.Project, ProjectOrderPlugin.Permission.CreateOrder, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice, "HeadOfService", "ServiceBackOffice", "InternalService");
			AddImport(ProjectPlugin.PermissionGroup.Project, ProjectOrderPlugin.Permission.CreateOrder, OrderPlugin.PermissionGroup.Order, PermissionName.Create);
			Add(ProjectPlugin.PermissionGroup.Project, OrderPlugin.PermissionName.OfferTab, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
			AddImport(ProjectPlugin.PermissionGroup.Project, OrderPlugin.PermissionName.OfferTab, ProjectPlugin.PermissionGroup.Project, PermissionName.Read);
			Add(ProjectPlugin.PermissionGroup.Project, OrderPlugin.PermissionName.OrderTab, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
			AddImport(ProjectPlugin.PermissionGroup.Project, OrderPlugin.PermissionName.OrderTab, ProjectPlugin.PermissionGroup.Project, PermissionName.Read);
		}
	}
}