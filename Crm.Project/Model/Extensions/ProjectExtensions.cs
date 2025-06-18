namespace Crm.Project.Model.Extensions
{
	using System.Linq;
	using Crm.Library.Extensions;
	using Crm.Project.Model.Lookups;
	using Crm.Project.SearchCriteria;

	using StackExchange.Profiling;

	public static class ProjectExtensions
	{
		public static IQueryable<Project> FilterProjectsBySearchCriteria(this IQueryable<Project> projects, ProjectSearchCriteria criteria)
		{
			using (MiniProfiler.Current.Step("ProjectExtensions.FilterProjectsBySearchCriteria"))
			{
				if (criteria == null)
				{
					return projects;
				}

				if (criteria.IsActive.HasValue && criteria.IsActive.Value)
				{
					projects = projects.Where(p => p.IsActive == criteria.IsActive);
				}

				if (criteria.ProjectStatusKey != null && criteria.ProjectStatusKey.NotEquals(""))
				{
					projects = projects.Where(p => p.StatusKey == criteria.ProjectStatusKey);
				}

				if (criteria.ParentId.HasValue)
				{
					projects = projects
						.Where(x => x.ParentId.HasValue)
						.Where(x => x.ParentId.Value == criteria.ParentId.Value);
				}

				if (criteria.ResponsibleUser.IsNotNullOrEmpty())
				{
					projects = projects.Where(x => x.ResponsibleUser == criteria.ResponsibleUser);
				}

				if (criteria.CategoryKey.IsNotNullOrEmpty())
				{
					projects = projects.Where(x => x.CategoryKey == criteria.CategoryKey);
				}

				if (criteria.Name.IsNotNullOrEmpty())
				{
					projects = projects.Where(x => x.Name.Contains(criteria.Name));
				}

				if (criteria.FromDate.HasValue)
				{
					projects = criteria.ProjectStatusKey.HasStatusDate()
						? projects.Where(x => x.StatusDate >= criteria.FromDate)
						: projects.Where(x => x.DueDate >= criteria.FromDate);
				}

				if (criteria.ToDate.HasValue)
				{
					projects = criteria.ProjectStatusKey.HasStatusDate()
						? projects.Where(x => x.StatusDate <= criteria.ToDate.Value.AddDays(1))
						: projects.Where(x => x.DueDate <= criteria.ToDate);
				}

				if (criteria.CurrencyKey.IsNotNullOrEmpty())
				{
					projects = projects.Where(x => x.CurrencyKey == criteria.CurrencyKey);
				}

				if (criteria.ProjectRating.HasValue && criteria.ProjectRating.Value > 0)
				{
					projects = projects.Where(x => x.Rating > criteria.ProjectRating);
				}

				if (criteria.CampaignKey.HasValue)
				{
					projects = projects.Where(x => x.CampaignSource == criteria.CampaignKey);
				}

				return projects;
			}
		}

		public static bool HasStatusDate(this string StatusKey)
		{
			return StatusKey == ProjectStatus.WonKey || StatusKey == ProjectStatus.LostKey;
		}
	}
}