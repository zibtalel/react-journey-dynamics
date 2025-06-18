namespace Sms.Einsatzplanung.Connector.Controllers.ActionRoleProvider
{
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Service;
	using Sms.Einsatzplanung.Connector.Model;

	public class SchedulerActionRoleProvider : RoleCollectorBase
	{
		public SchedulerActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			Add(PermissionGroup.Login, EinsatzplanungConnectorPlugin.PermissionName.Scheduler, ServicePlugin.Roles.ServicePlanner);
			Add(PermissionGroup.WebApi, nameof(Scheduler));
			Add(PermissionGroup.WebApi, nameof(SchedulerBinary));
			AddImport(PermissionGroup.WebApi, nameof(Scheduler), PermissionGroup.WebApi, nameof(SchedulerBinary));
			Add(PermissionGroup.WebApi, nameof(SchedulerConfig));
			AddImport(PermissionGroup.WebApi, nameof(Scheduler), PermissionGroup.WebApi, nameof(SchedulerConfig));
			Add(PermissionGroup.WebApi, nameof(SchedulerIcon));
			AddImport(PermissionGroup.WebApi, nameof(Scheduler), PermissionGroup.WebApi, nameof(SchedulerIcon));
			
			Add(PermissionGroup.WebApi, nameof(AbsenceDispatch), ServicePlugin.Roles.FieldService, ServicePlugin.Roles.HeadOfService, ServicePlugin.Roles.InternalService, ServicePlugin.Roles.ServiceBackOffice);
			Add(EinsatzplanungConnectorPlugin.PermissionGroup.AbsenceDispatch, PermissionName.Read, ServicePlugin.Roles.FieldService);
		}
	}
}