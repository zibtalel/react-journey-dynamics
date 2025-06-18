namespace Sms.Einsatzplanung.Team.Controllers.ActionRoleProvider
{
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Service;

	using Sms.Einsatzplanung.Team.Model;

	public class SchedulerActionRoleProvider : RoleCollectorBase
	{
		public SchedulerActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			Add(PermissionGroup.WebApi, nameof(TeamDispatchUser), ServicePlugin.Roles.HeadOfService, ServicePlugin.Roles.ServiceBackOffice, ServicePlugin.Roles.InternalService, Roles.APIUser);
		}
	}
}