namespace Crm.ErpExtension.Controllers.OData
{
	using System;
	using System.Linq;

	using Crm.ErpExtension.Model;
	using Crm.ErpExtension.Rest.Model;
	using Crm.Library.Api;
	using Crm.Library.Api.Controller;
	using Crm.Library.Data.Domain.DataInterfaces;

	using Microsoft.AspNetCore.OData.Query;
	using Microsoft.AspNetCore.Mvc;

	using System.Linq.Dynamic.Core;

	using Crm.Library.Api.Attributes;
	using Crm.Library.Extensions;

	[ControllerName("CrmErpExtension_ErpTurnover")]
	public class ErpTurnoverODataController : ODataControllerEx, IEntityApiController
	{
		private readonly IRepositoryWithTypedId<ErpTurnover, Guid> turnoverRepository;
		public Type EntityType => typeof(ErpTurnover);
		public ErpTurnoverODataController(IRepositoryWithTypedId<ErpTurnover, Guid> turnoverRepository)
		{
			this.turnoverRepository = turnoverRepository;
		}
		[HttpGet]
		public virtual IActionResult TurnoverPerArticleGroup01AndYear(ODataQueryOptions<ErpTurnoverRest> options, Guid ContactKey, bool IsVolume, string CurrencyKey, string QuantityUnitKey)
		{
			if (CurrencyKey == typeof(Microsoft.OData.ODataNullValue).ToString())
			{
				CurrencyKey = null;
			}
			if (QuantityUnitKey == typeof(Microsoft.OData.ODataNullValue).ToString())
			{
				QuantityUnitKey = null;
			}
			var chartData = DynamicQueryableExtensions.AsEnumerable(
					turnoverRepository.GetAll()
						.Where(x => x.ContactKey == ContactKey && x.IsVolume == IsVolume && x.CurrencyKey == CurrencyKey && x.QuantityUnitKey == QuantityUnitKey)
						.GroupBy(k => new { k.ArticleGroup01Key, k.Year }, e => e.Total, (key, elements) => new { key.ArticleGroup01Key, key.Year, Total = elements.Sum() }))
				.Select(x => new TurnoverChartData { d = x.ArticleGroup01Key, x = x.Year, y = x.Total });
			return Ok(chartData);
		}
		[HttpGet]
		public virtual IActionResult GetDistinctProperty(ODataQueryOptions<ErpTurnoverRest> options, Guid ContactKey, string PropertyName)
		{
			var query = (IQueryable<string>)turnoverRepository.GetAll()
				.Where(x => x.ContactKey == ContactKey)
				.Select(PropertyName)
				.Distinct();
			return Ok(query.AsEnumerable());
		}
	}
}
