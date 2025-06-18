namespace Sms.Einsatzplanung.Connector
{

	using Crm.Library.Modularization.Registrars;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Routing;

	using Sms.Einsatzplanung.Connector.Controllers;

	public class ServiceRouteRegistrar : IRouteRegistrar
	{
		public virtual RoutePriority Priority
		{
			get { return RoutePriority.AboveNormal; }
		}
		public virtual void RegisterRoutes(IEndpointRouteBuilder endpoints)
		{
			endpoints.MapControllerRoute(
				null,
				"Sms.Einsatzplanung.Connector/{controller}/{action}",
				new { action = "Index", plugin = "Sms.Einsatzplanung.Connector" },
				new { plugin = "Sms.Einsatzplanung.Connector" }
				);
			endpoints.MapControllerRoute(
				null,
				"Sms.Einsatzplanung.Connector/Scheduler/token{token}/{action}",
				new { controller = SchedulerController.Name, plugin = "Sms.Einsatzplanung.Connector" },
				new { plugin = "Sms.Einsatzplanung.Connector" }
				);
			endpoints.MapControllerRoute(
				null,
				"Sms.Einsatzplanung.Connector/Scheduler/token{token}/{schedulerDirectory}/files/{*relativePath}",
				new { controller = SchedulerController.Name, action = nameof(SchedulerController.DownloadApplicationFiles), plugin = "Sms.Einsatzplanung.Connector" },
				new { plugin = "Sms.Einsatzplanung.Connector" }
			);
		}
	}
}
