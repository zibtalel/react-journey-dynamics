namespace Sms.Einsatzplanung.Team
{

	using Crm.Library.Modularization.Registrars;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Routing;

	public class TeamRouteRegistrar : IRouteRegistrar
	{
		public virtual RoutePriority Priority { get; } = RoutePriority.Normal;
		public virtual void RegisterRoutes(IEndpointRouteBuilder endpoints)
		{
			endpoints.MapControllerRoute(
				null,
				"Sms.Einsatzplanung.Team/{controller}/{action}",
				new { plugin = "Sms.Einsatzplanung.Team" },
				new { plugin = "Sms.Einsatzplanung.Team" }
				);
		}
	}
}
