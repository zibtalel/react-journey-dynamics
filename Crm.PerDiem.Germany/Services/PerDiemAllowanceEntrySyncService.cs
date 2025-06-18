namespace Crm.PerDiem.Germany.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using AutoMapper;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Helper;
	using Crm.Library.Model;
	using Crm.Library.Rest;
	using Crm.Library.Services;
	using Crm.Library.Services.Interfaces;
	using Crm.PerDiem.Germany.Model;
	using Crm.PerDiem.Model;

	public class PerDiemAllowanceEntrySyncService : DefaultSyncService<PerDiemAllowanceEntry, Guid>
	{
		private readonly IAppSettingsProvider appSettingsProvider;
		private readonly ISyncService<PerDiemReport> perDiemReportSyncService;
		public PerDiemAllowanceEntrySyncService(IRepositoryWithTypedId<PerDiemAllowanceEntry, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper, IAppSettingsProvider appSettingsProvider, ISyncService<PerDiemReport> perDiemReportSyncService)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
			this.appSettingsProvider = appSettingsProvider;
			this.perDiemReportSyncService = perDiemReportSyncService;
		}

		public override Type[] SyncDependencies => new[] { typeof(PerDiemReport)};

		public override IQueryable<PerDiemAllowanceEntry> GetAll(User user, IDictionary<string, int?> groups)
		{
			var historySyncPeriod = appSettingsProvider.GetValue(PerDiemPlugin.Settings.Expense.ClosedHistorySyncPeriod);
			var isClosedEntitiesSince = DateTime.UtcNow.AddDays(-1 * historySyncPeriod);
			var perDiemReports = perDiemReportSyncService.GetAll(user, groups);
			var result = repository.GetAll().Where(
				x =>
					x.ResponsibleUser == user.Id && (!x.IsClosed || x.IsClosed && x.Date >= isClosedEntitiesSince) ||
					x.PerDiemReportId.HasValue && perDiemReports.Any(y => y.Id == x.PerDiemReportId.Value));
			return result;
		}
	}
}
