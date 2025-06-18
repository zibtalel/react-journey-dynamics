namespace Crm.Project.Controllers.RssFeedProvider
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Threading;

	using Crm.Infrastructure;
	using Crm.Library.Globalization.Lookup;
	using Crm.Library.Globalization.Resource;
	using Crm.Library.Model.Site;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Library.Services.Interfaces;
	using Crm.Project.Model;
	using Crm.Project.Model.Lookups;
	using Crm.Project.SearchCriteria;
	using Crm.Results;

	using Microsoft.AspNetCore.Routing;

	public class ProjectRssFeedProvider : RssFeedProvider<Project>
	{
		private readonly ProjectSearchCriteria criteria;
		private readonly IResourceManager resourceManager;
		private readonly ILookupManager lookupManager;

		public ProjectRssFeedProvider(IAbsolutePathHelper absolutePathHelper, IUserService userService, IResourceManager resourceManager, ILookupManager lookupManager, Site site, IPluginProvider pluginProvider)
			: base(absolutePathHelper, userService, site, pluginProvider)
		{
			this.resourceManager = resourceManager;
			this.lookupManager = lookupManager;
			criteria = new ProjectSearchCriteria();
		}
		protected override SyndicationFeedItemMapper<Project> GetFeedMapper(Dictionary<string, object> argDictionary)
		{
			return new SyndicationFeedItemMapper<Project>
				(
				f => f.Name + " - " + (f.Category != null ? f.Category.Value : resourceManager.GetTranslation("NoCatergory")) + " - " + f.Currency,
				f => f.BackgroundInfo,
				"Project",
				"DetailsTemplate",
				f => f.Id.ToString(),
				f => f.CreateDate
				);
		}
		protected override SyndicationFeedOptions GetFeedOptions(Dictionary<string, object> argDictionary)
		{
			var status = String.IsNullOrEmpty(criteria.ProjectStatusKey) ? String.Empty : String.Format("{0} = {1}", GetPhrase("Status", Thread.CurrentThread.CurrentUICulture, "Status"), lookupManager.Get<ProjectStatus>(criteria.ProjectStatusKey));
			var category = String.IsNullOrEmpty(criteria.CategoryKey) ? String.Empty : String.Format("| {0} = {1}", GetPhrase("Category", Thread.CurrentThread.CurrentUICulture, "Category"), lookupManager.Get<ProjectCategory>(criteria.CategoryKey));
			var responsibleUser = String.IsNullOrEmpty(criteria.ResponsibleUser) ? String.Empty : String.Format("| {0} = {1}", GetPhrase("ResponsibleUser", Thread.CurrentThread.CurrentUICulture, "Responsible User"), criteria.ResponsibleUser);
			var timespan = criteria.SelectedTime == null ? String.Empty : String.Format("| {0} = {1}", GetPhrase("Period", Thread.CurrentThread.CurrentUICulture, "Period"), GetPhrase(criteria.SelectedTime.Key, Thread.CurrentThread.CurrentUICulture, criteria.SelectedTime.Key));
			var fromTo = !criteria.FromDate.HasValue || !criteria.ToDate.HasValue ? String.Empty : String.Format("| {0} {1} {2} {3}", GetPhrase("From", Thread.CurrentThread.CurrentUICulture, "From"), criteria.FromDate.Value.ToShortDateString(), GetPhrase("To", Thread.CurrentThread.CurrentUICulture, "to"), criteria.ToDate.Value.ToShortDateString());
			var rating = !criteria.ProjectRating.HasValue ? String.Empty : String.Format("| {0} >{1}", GetPhrase("ProjectRating", Thread.CurrentThread.CurrentUICulture, "Rating"), criteria.ProjectRating);
			var options = new SyndicationFeedOptions
				(
				GetPhrase("ListOfProjects", Thread.CurrentThread.CurrentUICulture, "List of Projects - Crm"),
				String.Format("{0}: {1} {2} {3} {4} {5} {6}", GetPhrase("FilteredBy", Thread.CurrentThread.CurrentUICulture, "Filtered by"), status, category, responsibleUser, timespan, fromTo, rating),
				absolutePathHelper.GetPath("IndexTemplate", "ProjectList", new RouteValueDictionary(new { plugin = "Crm.Project" }))
				);
			return options;
		}

		protected virtual string GetPhrase(string key, CultureInfo cultureInfo, string defaultValue)
		{
			return resourceManager.GetTranslation(key, cultureInfo) ?? defaultValue;
		}
	}
}
