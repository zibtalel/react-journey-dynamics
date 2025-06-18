namespace Sms.Einsatzplanung.Connector.Controllers.OData;

using System;
using System.IO;
using Crm.Library.Api.Controller;
using Crm.Library.AutoFac;
using Microsoft.AspNetCore.Http;
using Sms.Einsatzplanung.Connector.Model;
using Sms.Einsatzplanung.Connector.Rest.Model;
using Sms.Einsatzplanung.Connector.Services;
using Sms.Einsatzplanung.Connector.ViewModels;

public class SchedulerConfigWriter : IODataWriteFunction<SchedulerConfig, SchedulerConfigRest, Guid>, IDependency
{
	private readonly ISchedulerService schedulerService;
	public SchedulerConfigWriter(ISchedulerService schedulerService)
	{
		this.schedulerService = schedulerService;
	}
	public virtual void Apply(HttpRequest request, SchedulerConfigRest restEntity, SchedulerConfig entity)
	{
		SchedulerConfigType configType;
		if (!Enum.TryParse(restEntity.Type, out configType))
		{
			configType = SchedulerConfigType.Undefined;
		}

		var config = new SchedulerConfigViewModel { Type = configType };
		var filePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
		config.FileInfo = new FileInfo(filePath);
		File.WriteAllBytes(filePath, restEntity.Config);
		schedulerService.SaveConfig(config.FileInfo, configType);
	}
}
