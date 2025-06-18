namespace Sms.Einsatzplanung.Connector.Controllers.OData
{
	using Crm.Library.Api.Extensions;
	using Crm.Library.Api.Model;
	using Sms.Einsatzplanung.Connector.Model;
	using Sms.Einsatzplanung.Connector.Rest.Model;

	public class SchedulerFunctionConfigurator : IModelConfigurator
	{
		private readonly ODataModelBuilderHelper modelBuilderHelper;
		public SchedulerFunctionConfigurator(ODataModelBuilderHelper modelBuilderHelper)
		{
			this.modelBuilderHelper = modelBuilderHelper;
		}
		public virtual void Configure(ODataConventionModelBuilder builder)
		{
			var type = builder.AddEntityType(typeof(SchedulerBinaryRest))
				.HasKey(typeof(SchedulerBinaryRest).GetProperty(nameof(SchedulerBinaryRest.Id)));
			type.Name = modelBuilderHelper.GetEntityTypeName(typeof(SchedulerBinary));
			builder.AddEntitySet(type.Name, type);

			var schedulerCreatePackage = builder
				.EntityType<SchedulerBinaryRest>()
				.Collection
				.Action(nameof(SchedulerBinaryController.CreatePackage))
				.ReturnsCollection<string>();
			schedulerCreatePackage.Title = "creates a package from a binary";
			schedulerCreatePackage.Parameter<string>("FileName").NotNullable().Required();
		}
	}
}
