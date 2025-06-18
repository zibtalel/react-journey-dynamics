namespace Crm.Project.ModelBinders
{
	using System;

	using Crm.Library.Extensions;
	using Crm.Library.Globalization.Lookup;
	using Crm.Library.ModelBinder;
	using Crm.ModelBinders;
	using Crm.Project.Model.Lookups;
	using Crm.Project.SearchCriteria;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.ModelBinding;

	[ModelBinderFor(typeof(ProjectSearchCriteria))]
	public class ProjectSearchCriteriaModelBinder : TimeSpanSearchCriteriaModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var model = base.BindModel(controllerContext, bindingContext) as ProjectSearchCriteria;

			if (model == null)
			{
				return null;
			}

			var lookupManager = controllerContext.HttpContext.GetService<ILookupManager>();
			model.ProjectStatusKey = model.ProjectStatusKey != null && model.ProjectStatusKey != lookupManager.GetFavoriteKey<ProjectStatus, string>() ? model.ProjectStatusKey : GetValue<string>(bindingContext, "Status");
			model.CategoryKey = model.CategoryKey != null && model.CategoryKey != lookupManager.GetFavoriteKey<ProjectCategory, string>() ? model.CategoryKey : GetValue<string>(bindingContext, "filter_category");
			model.Category = model.CategoryKey != null ? lookupManager.Get<ProjectCategory>(model.CategoryKey) : null;
			var sortOrder = GetValue<string>(bindingContext, "SortOrder");
			var sortBy = GetValue<string>(bindingContext, "SortBy");

			model.SortOrder = String.IsNullOrWhiteSpace(sortOrder) || sortOrder == "null" ? "descending" : sortOrder;
			model.SortBy = String.IsNullOrWhiteSpace(sortBy) ? "DueDate" : sortBy;
			model.Name = GetValue<string>(bindingContext, "Name");

			var projectCurrency = GetValue<string>(bindingContext, "filter_currency");
			if (projectCurrency.IsNotNullOrEmpty())
			{
				model.CurrencyKey = projectCurrency;
			}

			model.CampaignKey = GetValue<Guid?>(bindingContext, "CampaignKey");

			return model;
		}
	}
}