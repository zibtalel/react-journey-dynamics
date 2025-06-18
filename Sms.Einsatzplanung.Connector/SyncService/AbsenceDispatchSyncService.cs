namespace Sms.Einsatzplanung.Connector.SyncService;

using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Crm.Library.Data.Domain.DataInterfaces;
using Crm.Library.Globalization.Lookup;
using Crm.Library.Model;
using Crm.Library.Rest;
using Crm.Library.Services;
using Crm.PerDiem.Model.Lookups;

using Sms.Einsatzplanung.Connector.Model;

public class AbsenceDispatchSyncService : DefaultSyncService<AbsenceDispatch, Guid>
{
	private readonly IList<string> timeEntryTypeKeysWithShowInMobileClient;
	private readonly ILookupManager lookupManager;
	
	public AbsenceDispatchSyncService(IRepositoryWithTypedId<AbsenceDispatch, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper, ILookupManager lookupManager)
		: base(repository,
			restTypeProvider,
			restSerializer,
			mapper)
	{
		this.lookupManager = lookupManager;
		var timeEntryTypes = new List<TimeEntryType>(lookupManager.List<TimeEntryType>());
		timeEntryTypeKeysWithShowInMobileClient = timeEntryTypes.Where(timeEntryType => timeEntryType.ShowInMobileClient).Select(timeEntryType => timeEntryType.Key).ToList();
	}
	public override IQueryable<AbsenceDispatch> GetAll(User user)
	{
		return repository.GetAll().Where(absence => timeEntryTypeKeysWithShowInMobileClient.Contains(absence.AbsenceTypeKey));
	}
}
