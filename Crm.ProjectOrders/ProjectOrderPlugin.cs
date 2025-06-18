namespace Crm.ProjectOrders
{
	using Crm.Library.Modularization;

	[Plugin(Requires = "Crm.Project,Crm.Order")]
	public class ProjectOrderPlugin : Plugin
	{
		public static class Permission
		{
			public const string CreateOrder = "CreateOrder"; 
			public const string CreateOffer = "CreateOffer";
		}
	}
}