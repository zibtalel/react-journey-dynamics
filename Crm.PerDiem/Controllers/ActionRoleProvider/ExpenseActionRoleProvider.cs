namespace Crm.PerDiem.Controllers.ActionRoleProvider
{
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Model.Lookups;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.Model.Lookups;

	public class ExpenseActionRoleProvider : RoleCollectorBase
	{
		public ExpenseActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			var salesRoleNames = new[] { MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales };

			Add(PerDiemPlugin.PermissionGroup.Expense, PermissionName.Create, salesRoleNames);
			AddImport(PerDiemPlugin.PermissionGroup.Expense, PermissionName.Create, PerDiemPlugin.PermissionGroup.Expense, PermissionName.Edit);
			Add(PerDiemPlugin.PermissionGroup.Expense, PermissionName.Edit, salesRoleNames);
			Add(PerDiemPlugin.PermissionGroup.Expense, PermissionName.Delete, salesRoleNames);
			AddImport(PerDiemPlugin.PermissionGroup.Expense, PermissionName.Delete, PerDiemPlugin.PermissionGroup.Expense, PermissionName.Edit);
			Add(PerDiemPlugin.PermissionGroup.Expense, PermissionName.Index, salesRoleNames);
			Add(PerDiemPlugin.PermissionGroup.Expense, PermissionName.View, salesRoleNames);
			Add(PerDiemPlugin.PermissionGroup.Expense, PerDiemPlugin.PermissionName.SeeAllUsersExpenses, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales);
			AddImport(PerDiemPlugin.PermissionGroup.Expense, PerDiemPlugin.PermissionName.SeeAllUsersExpenses, PerDiemPlugin.PermissionGroup.Expense, PermissionName.Index);
			Add(PerDiemPlugin.PermissionGroup.Expense, MainPlugin.PermissionName.DownloadAsPdf, salesRoleNames);
			AddImport(PerDiemPlugin.PermissionGroup.Expense, MainPlugin.PermissionName.DownloadAsPdf, PerDiemPlugin.PermissionGroup.Expense, PermissionName.Index);

			Add(PermissionGroup.WebApi, nameof(UserExpense), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(ExpenseType), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(CostCenter), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(Currency), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
		}
	}
}