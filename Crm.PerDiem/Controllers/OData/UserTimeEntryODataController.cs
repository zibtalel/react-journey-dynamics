namespace Crm.PerDiem.Controllers.OData
{
	using Crm.Library.Api.Attributes;
	using Crm.Library.Api.Mapping;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.Rest.Model;

	using Microsoft.AspNetCore.OData.Query;
	using Microsoft.AspNetCore.Mvc;

	[ControllerName("CrmPerDiem_UserTimeEntry")]
	public class UserTimeEntryODataController : DistinctDateODataController<UserTimeEntry, UserTimeEntryRest>
	{
		public UserTimeEntryODataController(IRepository<UserTimeEntry> repository, IODataMapper mapper)
			: base(repository, mapper)
		{
		}
		[HttpGet]
		public virtual IActionResult GetDistinctUserTimeEntryDates(ODataQueryOptions<UserTimeEntryRest> options) => base.GetDistinctDates(options, x => x.Date);
	}
}
