namespace Crm.Project.Controllers.OData
{
	using System;

	using Crm.Library.Api.Model;
	using Crm.Project.Rest.Model;

	using ODataConventionModelBuilder = Crm.Library.Api.Model.ODataConventionModelBuilder;

	public class ProjectFunctionConfigurator : IModelConfigurator
	{
		public virtual void Configure(ODataConventionModelBuilder builder)
		{
			var projectValuePerCategoryAndYear = builder.EntityType<ProjectRest>()
				.Collection
				.Action(nameof(ProjectODataController.ValuePerCategoryAndYear))
				.ReturnsCollection<ProjectValueChartData>();
			projectValuePerCategoryAndYear.CollectionParameter<Guid>("ProjectIds");
			projectValuePerCategoryAndYear.Title = "gets the value per category and year ready to display in a chart";

			var countOfProjectsByStatus = builder.EntityType<ProjectRest>()
				.Collection
				.Function(nameof(ProjectODataController.CountOfProjectsByStatus))
				.ReturnsCollection<ProjectValuesData>();
			countOfProjectsByStatus.Parameter<Guid>("ProductFamilyId").Required();
			countOfProjectsByStatus.Parameter<Guid>("ParentId").Required();
			countOfProjectsByStatus.Title = "Return the value group by product family and currency";

			var currencySumByStatusAndCurrencyKey = builder.EntityType<ProjectRest>()
				.Collection
				.Function(nameof(ProjectODataController.CurrencySumByStatusAndCurrencyKey))
				.ReturnsCollection<ValueSumByCurrency>();
			currencySumByStatusAndCurrencyKey.Parameter<Guid>("ProductFamilyId").Required();
			currencySumByStatusAndCurrencyKey.Parameter<Guid>("ParentId").Required();
			currencySumByStatusAndCurrencyKey.Title = "Return the value group by product family and currency filter by product family and parentId";

			var currencySumByStatus = builder.EntityType<ProjectRest>()
				.Collection
				.Function(nameof(ProjectODataController.CurrencySumByStatus))
				.ReturnsCollection<ValueSumByCurrency>();
			currencySumByStatus.Parameter<Guid>("ParentId").Required();
			currencySumByStatus.Title = "Return the value group by currency and status filter by parentId";
			
			var allProjectsCurrencySum = builder.EntityType<ProjectRest>()
				.Collection
				.Function(nameof(ProjectODataController.AllProjectsCurrencySum))
				.ReturnsCollection<ValueSumByCurrency>();
			currencySumByStatus.Title = "Return the value group by currency and status";



		}
	}
}

