namespace Crm.PerDiem.Controllers.OData
{
	using System;

	using Crm.Library.AutoFac;
	using Crm.Library.Extensions;
	using Crm.Library.Rest;
	using Microsoft.OData.ModelBuilder;
	using ODataConventionModelBuilder = Crm.Library.Api.Model.ODataConventionModelBuilder;

	public class DistinctDateFunctionConfiguratorHelper : ISingletonDependency
	{
		public virtual FunctionConfiguration Configure<TRest>(ODataConventionModelBuilder builder)
			where TRest : class, IRestEntity
		{
			var function = builder.EntityType<TRest>()
				.Collection
				.Function($"GetDistinct{typeof(TRest).Name.RemoveSuffix("Rest")}Dates")
				.ReturnsCollection<DateTime>();
			function.Title = "returns the distinct dates of the query result";
			return function;
		}
	}
}
