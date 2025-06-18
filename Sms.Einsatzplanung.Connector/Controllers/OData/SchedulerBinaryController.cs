namespace Sms.Einsatzplanung.Connector.Controllers.OData
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Linq.Dynamic.Core;
	using System.Net;
	using Crm.Library.Api;
	using Crm.Library.Api.Attributes;
	using Crm.Library.Api.Controller;
	using Crm.Library.Api.Extensions;
	using Crm.Library.Extensions;
	using Crm.Library.Globalization.Resource;
	using Crm.Library.Model.Site;
	using Crm.Library.Services.Interfaces;
	using Crm.Services.Interfaces;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.OData.Extensions;
	using Microsoft.AspNetCore.OData.Formatter;
	using Microsoft.AspNetCore.OData.Query;
	using Sms.Einsatzplanung.Connector.Model;
	using Sms.Einsatzplanung.Connector.Rest.Model;
	using Sms.Einsatzplanung.Connector.Services;

	[ControllerName("SmsEinsatzplanungConnector_SchedulerBinary")]
	public class SchedulerBinaryController : ODataControllerEx, IEntityApiController
	{
		private readonly ISchedulerService schedulerService;
		private readonly ISiteService siteService;
		private readonly IRuleValidationService ruleValidationService;
		private readonly IResourceManager resourceManager;

		public Type EntityType => typeof(SchedulerBinary);

		public SchedulerBinaryController(ISchedulerService schedulerService, ISiteService siteService, IRuleValidationService ruleValidationService, IResourceManager resourceManager)
		{
			this.schedulerService = schedulerService;
			this.siteService = siteService;
			this.ruleValidationService = ruleValidationService;
			this.resourceManager = resourceManager;
		}

		[HttpPost, HttpPut]
		public virtual IActionResult CreatePackage(ODataActionParameters parameters)
		{
			var fileName = parameters.GetValue<string>("FileName");
			var hostUri = siteService.CurrentSite.GetExtension<DomainExtension>().HostUri;
			var path = hostUri.PathAndQuery.AppendIfMissing("/") + "Sms.Einsatzplanung.Connector/Scheduler/DownloadApplicationManifest";
			var uriBuilder = hostUri.IsDefaultPort ? new UriBuilder(hostUri.Scheme, hostUri.Host) { Path = path } : new UriBuilder(hostUri.Scheme, hostUri.Host, hostUri.Port, path);
			var result = schedulerService.CreatePackage(fileName, uriBuilder.ToString());
			return Ok(result);
		}

		[HttpDelete]
		public virtual IActionResult Delete(int key)
		{
			schedulerService.DeleteBinary(key);
			return NoContent();
		}

		[HttpGet]
		public virtual IActionResult Get(ODataQueryOptions<SchedulerBinaryRest> options)
		{
			var sourceQuery = schedulerService.SchedulerBinaries.Select(x => new SchedulerBinaryRest { Id = x.File.Name.GetHashCode(), Filename = x.File.Name, LastWriteTimeUtc = x.File.LastWriteTimeUtc, Version = x.Version?.ToString(4) }).AsQueryable();
			var query = options.ApplyTo(sourceQuery);
			if (options.Count != null && Request.IsCountRequest())
			{
				return Ok(query.Count());
			}

			return Ok(query);
		}

		[HttpPost]
		public virtual IActionResult Post([FromBody] SchedulerBinaryRest restEntity)
		{
			var bytes = Convert.FromBase64String(restEntity.Content);

			using var entity = new SchedulerBinary();
			entity.Filename = restEntity.Filename;
			var filePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			entity.FileInfo = new FileInfo(filePath);
			System.IO.File.WriteAllBytes(filePath, bytes);
			if (GetODataError(ruleValidationService.GetRuleViolations(entity), resourceManager) is { } error)
			{
				return BadRequest(error);
			}

			schedulerService.AddBinaries(restEntity.Filename, bytes);
			return StatusCode((int)HttpStatusCode.Created, restEntity);
		}
	}
}
