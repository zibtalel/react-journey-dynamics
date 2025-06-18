namespace Crm.Project.Controllers.OData
{
	using Crm.Library.Api.Model;

	using System;

	using Crm.Project.Rest.Model;

	public class PotentialFunctionConfiguration : IModelConfigurator
	{
		public virtual void Configure(ODataConventionModelBuilder builder)
		{
			var countOfPotentialsByStatus = builder.EntityType<PotentialRest>()
				.Collection
				.Function(nameof(PotentialODataController.CountOfPotentialsByStatus))
				.ReturnsCollection<PotentialTotalCount>();
			countOfPotentialsByStatus.Parameter<Guid>("ProductFamilyId").Required();
			countOfPotentialsByStatus.Parameter<Guid>("ParentId").Required();
			countOfPotentialsByStatus.Title = "Return the count of potentials group by status";
		}
	}
}
