namespace Crm.Configurator.Controllers.ActionRoleProvider
{
	using Crm.Article;
	using Crm.Configurator.Model;
	using Crm.Configurator.Model.Lookups;
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;

	public class ConfiguratorActionRoleProvider : RoleCollectorBase
	{
		public ConfiguratorActionRoleProvider(IPluginProvider pluginProvider)
			:
			base(pluginProvider)
		{
			Add(ConfiguratorPlugin.PermissionGroup.ConfigurationRules, PermissionName.Edit, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice);
			AddImport(ConfiguratorPlugin.PermissionGroup.ConfigurationRules, PermissionName.Edit, ArticlePlugin.PermissionGroup.Article, PermissionName.Edit);
			Add(ConfiguratorPlugin.PermissionGroup.ConfigurationRules, PermissionName.Index, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
			Add(ConfiguratorPlugin.PermissionGroup.Configurator, PermissionName.View, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
			AddImport(ConfiguratorPlugin.PermissionGroup.Configurator, PermissionName.View, ArticlePlugin.PermissionGroup.Article, PermissionName.Index);
			Add(ConfiguratorPlugin.PermissionGroup.Configurator, PermissionName.Index, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice);
			AddImport(ConfiguratorPlugin.PermissionGroup.Configurator, PermissionName.Index, ArticlePlugin.PermissionGroup.Article, PermissionName.Index);

			Add(PermissionGroup.WebApi, nameof(ConfigurationBase), MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(ConfigurationRule), MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(ConfigurationRuleType), MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(Variable), MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.SalesBackOffice, Roles.APIUser);
		}
	}
}