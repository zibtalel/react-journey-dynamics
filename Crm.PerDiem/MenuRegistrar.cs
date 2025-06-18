namespace Crm.PerDiem
{
	using System;

	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Menu;

	using PermissionGroup = PerDiemPlugin.PermissionGroup;

	public class MenuRegistrar : IMenuRegistrar<MaterialMainMenu>
	{
		public virtual void Initialize(MenuProvider<MaterialMainMenu> menuProvider)
		{
			menuProvider.Register(null, "TimeTableAndExpenses", url: "~/Crm.PerDiem/TimeEntry/IndexTemplate", iconClass: "zmdi zmdi-time-interval", priority: Int32.MaxValue - 80);
			menuProvider.AddPermission(null, "TimeTableAndExpenses", PermissionGroup.TimeEntry, PermissionName.View);
		}
	}
}
