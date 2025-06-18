namespace Crm.ProjectOrders
{

	using Crm.Library.Modularization.Registrars;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Routing;

	public class Routes : IRouteRegistrar
	{
		public virtual RoutePriority Priority
		{
			get { return RoutePriority.Normal; }
		}
		public virtual void RegisterRoutes(IEndpointRouteBuilder endpoints)
		{
			endpoints.MapControllerRoute(
				null,
				"Crm.ProjectOrders/{controller}/{action}/{id?}",
				new { action = "Index", plugin = "Crm.ProjectOrders" },
				new { plugin = "Crm.ProjectOrders" }
				);
		}
	}
}
