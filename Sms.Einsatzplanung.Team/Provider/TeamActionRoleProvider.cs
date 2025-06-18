namespace Sms.Einsatzplanung.Team.Provider
{
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Service;

	using Sms.Einsatzplanung.Team.Model;

	public class TeamActionRoleProvider : RoleCollectorBase
	{
		protected static readonly string[] Roles = { ServicePlugin.Roles.FieldService, ServicePlugin.Roles.ServiceBackOffice };
		public TeamActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			Add(PermissionGroup.WebApi, nameof(TeamDispatchUser), Roles);
		}
	}
}