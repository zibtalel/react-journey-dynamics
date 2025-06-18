namespace Crm.Project
{

	using Crm.Library.Modularization.Registrars;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Routing;

	public class Routes : IRouteRegistrar
	{
		public virtual RoutePriority Priority
		{
			get { return RoutePriority.AboveNormal; }
		}
		public virtual void RegisterRoutes(IEndpointRouteBuilder endpoints)
		{
			endpoints.MapControllerRoute(
				null,
				"Crm.Project/{controller}/{action}/{id?}",
				new { action = "Index", plugin = "Crm.Project" },
				new { plugin = "Crm.Project" }
				);
		}
	}
}
