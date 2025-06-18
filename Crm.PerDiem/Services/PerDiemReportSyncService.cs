namespace Crm.PerDiem.Services
{
	using System;
	using System.Linq;

	using AutoMapper;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Helper;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization.Interfaces;
	using Crm.Library.Rest;
	using Crm.Library.Services;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.Model.Lookups;

	public class PerDiemReportSyncService : DefaultSyncService<PerDiemReport, Guid>
	{
		private readonly IAuthorizationManager authorizationManager;
		private readonly IAppSettingsProvider appSettingsProvider;

		public PerDiemReportSyncService(IRepositoryWithTypedId<PerDiemReport, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper, IAuthorizationManager authorizationManager, IAppSettingsProvider appSettingsProvider)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
			this.authorizationManager = authorizationManager;
			this.appSettingsProvider = appSettingsProvider;
		}

		public override IQueryable<PerDiemReport> GetAll(User user)
		{
			var closedReportsSincePeriod = appSettingsProvider.GetValue(PerDiemPlugin.Settings.PerDiemReport.ShowClosedReportsSince);
			var closedReportsSince = DateTime.UtcNow.AddDays(-1 * closedReportsSincePeriod);
			var query = repository.GetAll().Where(x => x.StatusKey != PerDiemReportStatus.ClosedKey || x.To >= closedReportsSince);
			if (!authorizationManager.IsAuthorizedForAction(user, PerDiemPlugin.PermissionGroup.PerDiemReport, PerDiemPlugin.PermissionName.SeeAllUsersPerDiemReports))
			{
				query = query.Where(x => x.UserName == user.Id);
			}
			return query;
		}

		public override PerDiemReport Save(PerDiemReport entity)
		{
			repository.SaveOrUpdate(entity);
			return entity;
		}

		public override void Remove(PerDiemReport entity)
		{
			repository.Delete(entity);
		}
	}
}