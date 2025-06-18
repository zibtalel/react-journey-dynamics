namespace Crm.Project
{
	using System;
	using System.Runtime.CompilerServices;

	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Menu;

	public class ProjectMenuRegistrar : IMenuRegistrar<MaterialMainMenu>
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public virtual void Initialize(MenuProvider<MaterialMainMenu> menuProvider)
		{
			menuProvider.Register(null, "Sales", iconClass: "zmdi zmdi-money-box", priority: Int32.MaxValue - 100);
			menuProvider.Register("Sales", "Projects", url: "~/Crm.Project/ProjectList/IndexTemplate", iconClass: "zmdi zmdi-file", priority: Int32.MaxValue - 500);
			menuProvider.AddPermission("Sales", "Projects", ProjectPlugin.PermissionGroup.Project, PermissionName.View);
			menuProvider.Register("Sales", "Potentials", url: "~/Crm.Project/PotentialList/IndexTemplate", iconClass: "zmdi zmdi-view-quilt", priority: Int32.MaxValue - 480);
			menuProvider.AddPermission("Sales", "Potentials", ProjectPlugin.PermissionGroup.Potential, PermissionName.View);
		}
	}
}
