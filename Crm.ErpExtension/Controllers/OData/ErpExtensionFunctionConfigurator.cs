namespace Crm.ErpExtension.Controllers.OData
{
	using System;

	using Crm.ErpExtension.Rest.Model;
	using Crm.Library.Api.Model;

	using ODataConventionModelBuilder = Crm.Library.Api.Model.ODataConventionModelBuilder;
	using SalesOrderRest = Crm.ErpExtension.Rest.Model.SalesOrderRest;

	public class ErpExtensionFunctionConfigurator : IModelConfigurator
	{
		public virtual void Configure(ODataConventionModelBuilder builder)
		{
			var turnoverPerArticleGroup01AndYear = builder.EntityType<ErpTurnoverRest>()
				.Collection
				.Function(nameof(ErpTurnoverODataController.TurnoverPerArticleGroup01AndYear))
				.ReturnsCollection<TurnoverChartData>();
			turnoverPerArticleGroup01AndYear.Parameter<Guid>("ContactKey");
			turnoverPerArticleGroup01AndYear.Parameter<bool>("IsVolume");
			turnoverPerArticleGroup01AndYear.Parameter<string>("CurrencyKey");
			turnoverPerArticleGroup01AndYear.Parameter<string>("QuantityUnitKey");
			turnoverPerArticleGroup01AndYear.Title = "gets the total turnover per ArticleGroup01 and year ready to display in a chart";

			var getDistinct = builder.EntityType<ErpTurnoverRest>()
				.Collection
				.Function(nameof(ErpTurnoverODataController.GetDistinctProperty))
				.ReturnsCollection<string>();
			getDistinct.Parameter<Guid>("ContactKey");
			getDistinct.Parameter<string>("PropertyName");
			getDistinct.Title = "return distinct values of a selected string property";

			var getInformation = builder.EntityType<SalesOrderRest>()
				.Collection
				.Function(nameof(SalesOrderODataController.GetInformation))
				.Returns<SalesOrderInformation>();
			getInformation.Parameter<Guid>("ContactKey");
			getInformation.Title = "returns aggregated sales order information";
		}
	}
}
