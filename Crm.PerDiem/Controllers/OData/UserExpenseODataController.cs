namespace Crm.PerDiem.Controllers.OData
{
	using Crm.Library.Api.Attributes;
	using Crm.Library.Api.Mapping;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.Rest.Model;

	using Microsoft.AspNetCore.OData.Query;
	using Microsoft.AspNetCore.Mvc;

	[ControllerName("CrmPerDiem_UserExpense")]
	public class UserExpenseODataController : DistinctDateODataController<UserExpense, UserExpenseRest>
	{
		public UserExpenseODataController(IRepository<UserExpense> repository, IODataMapper mapper)
			: base(repository, mapper)
		{
		}
		[HttpGet]
		public virtual IActionResult GetDistinctUserExpenseDates(ODataQueryOptions<UserExpenseRest> options) => base.GetDistinctDates(options, x => x.Date);
	}
}
