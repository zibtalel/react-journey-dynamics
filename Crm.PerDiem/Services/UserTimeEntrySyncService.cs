namespace Crm.PerDiem.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using AutoMapper;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Helper;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.Interfaces;
	using Crm.Library.Rest;
	using Crm.Library.Services;
	using Crm.Library.Services.Interfaces;
	using Crm.PerDiem.Model;

	public class UserTimeEntrySyncService : DefaultSyncService<UserTimeEntry, Guid>
	{
		private readonly IAppSettingsProvider appSettingsProvider;
		private readonly ISyncService<PerDiemReport> perDiemReportSyncService;
		private readonly IAuthorizationManager authorizationManager;
		public UserTimeEntrySyncService(IRepositoryWithTypedId<UserTimeEntry, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper, IAppSettingsProvider appSettingsProvider, ISyncService<PerDiemReport> perDiemReportSyncService, IAuthorizationManager authorizationManager)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
			this.appSettingsProvider = appSettingsProvider;
			this.perDiemReportSyncService = perDiemReportSyncService;
			this.authorizationManager = authorizationManager;
		}

		public override Type[] SyncDependencies => new[] { typeof(PerDiemReport) };
		public override IQueryable<UserTimeEntry> GetAll(User user, IDictionary<string, int?> groups)
		{
			var historySyncPeriod = appSettingsProvider.GetValue(PerDiemPlugin.Settings.TimeEntry.ClosedHistorySyncPeriod);
			var isClosedEntitiesSince = DateTime.UtcNow.AddDays(-1 * historySyncPeriod);
			var perDiemReports = perDiemReportSyncService.GetAll(user, groups);
			var seeAllUsersTimeEntries = authorizationManager.IsAuthorizedForAction(user, PerDiemPlugin.PermissionGroup.TimeEntry, PerDiemPlugin.PermissionName.SeeAllUsersTimeEntries);
			return repository.GetAll().Where(
				x =>
					(seeAllUsersTimeEntries || x.ResponsibleUser == user.Id) && (!x.IsClosed || x.IsClosed && x.Date >= isClosedEntitiesSince) ||
					x.PerDiemReportId.HasValue && perDiemReports.Any(y => y.Id == x.PerDiemReportId.Value));
		}

		public override void Remove(UserTimeEntry entity)
		{
			repository.Delete(entity);
		}
	}
}
