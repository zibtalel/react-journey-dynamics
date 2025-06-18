namespace Crm.ErpExtension.Controllers.OData
{
	using System;
	using System.Linq;

	using Crm.ErpExtension.Model;
	using Crm.ErpExtension.Rest.Model;
	using Crm.Library.Api;
	using Crm.Library.Api.Attributes;
	using Crm.Library.Api.Controller;
	using Crm.Library.Data.Domain.DataInterfaces;

	using Microsoft.AspNetCore.OData.Query;
	using Microsoft.AspNetCore.Mvc;

	using NHibernate.Criterion;

	[ControllerName("CrmErpExtension_SalesOrder")]
	public class SalesOrderODataController : ODataControllerEx, IEntityApiController
	{
		private readonly IRepositoryWithTypedId<SalesOrder, Guid> salesOrderRepository;
		public Type EntityType => typeof(ErpTurnover);
		public SalesOrderODataController(IRepositoryWithTypedId<SalesOrder, Guid> salesOrderRepository)
		{
			this.salesOrderRepository = salesOrderRepository;
		}
		[HttpGet]
		public virtual IActionResult GetInformation(ODataQueryOptions<SalesOrderRest> options, Guid ContactKey)
		{
			var info = salesOrderRepository.Session.QueryOver<SalesOrder>()
				.Select(Projections.ProjectionList()
					.Add(Projections.Min<SalesOrder>(x => x.OrderConfirmationDate))
					.Add(Projections.Max<SalesOrder>(x => x.OrderConfirmationDate))
					.Add(Projections.Count<SalesOrder>(x => x.Id)))
				.List<object[]>()
				.Single();
			return Ok(new SalesOrderInformation
			{
				FirstOrder = (DateTime)info[0],
				LastOrder = (DateTime)info[1],
				TotalOrders = (int)info[2]
			});
		}
	}
}
