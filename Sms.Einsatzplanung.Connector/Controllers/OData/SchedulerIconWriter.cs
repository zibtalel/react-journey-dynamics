namespace Sms.Einsatzplanung.Connector.Controllers.OData;

using System;
using Crm.Library.Api.Controller;
using Crm.Library.AutoFac;
using Microsoft.AspNetCore.Http;
using Sms.Einsatzplanung.Connector.Model;
using Sms.Einsatzplanung.Connector.Rest.Model;
using Sms.Einsatzplanung.Connector.Services;

public class SchedulerIconWriter : IODataWriteFunction<SchedulerIcon, SchedulerIconRest, Guid>, IDependency
{
	private readonly ISchedulerService schedulerService;
	public SchedulerIconWriter(ISchedulerService schedulerService)
	{
		this.schedulerService = schedulerService;
	}
	public virtual void Apply(HttpRequest request, SchedulerIconRest restEntity, SchedulerIcon entity)
	{
		schedulerService.SaveIcon(entity);
	}
}
