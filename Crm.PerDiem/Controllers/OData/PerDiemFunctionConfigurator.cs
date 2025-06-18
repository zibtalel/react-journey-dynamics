namespace Crm.PerDiem.Controllers.OData
{
	using Crm.Library.Api.Model;
	using Crm.PerDiem.Rest.Model;

	using ODataConventionModelBuilder = Crm.Library.Api.Model.ODataConventionModelBuilder;

	public class PerDiemFunctionConfigurator : IModelConfigurator
	{
		private readonly DistinctDateFunctionConfiguratorHelper helper;
		public PerDiemFunctionConfigurator(DistinctDateFunctionConfiguratorHelper helper)
		{
			this.helper = helper;
		}
		public virtual void Configure(ODataConventionModelBuilder builder)
		{
			helper.Configure<UserExpenseRest>(builder);
			helper.Configure<UserTimeEntryRest>(builder);
		}
	}
}
