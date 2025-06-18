namespace Crm.ErpExtension
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
				"Crm.ErpExtension",
				"Crm.ErpExtension/{controller}/{action}/{id?}",
				new { plugin = "Crm.ErpExtension" },
				new { plugin = "Crm.ErpExtension" });

			endpoints.MapControllerRoute(
				"InforLink",
				"Crm.ErpExtension/{controller}/{action}",
				new { plugin = "Crm.ErpExtension", controller = "ErpCompanyController", action = "ObjectLibraryLink" },
				new { plugin = "Crm.ErpExtension" });

			endpoints.MapControllerRoute(
				"D3Link",
				"Crm.ErpExtension/{controller}/{action}",
				new { plugin = "Crm.ErpExtension", controller = "ErpCompanyController", action = "ObjectD3Link" },
				new { plugin = "Crm.ErpExtension" });

			endpoints.MapControllerRoute(
				null,
				"Crm.ErpExtension/{controller}/{action}/{id?}",
				new { action = "Index", plugin = "Crm.ErpExtension" },
				new { plugin = "Crm.ErpExtension" }
				);
		}
	}
}
