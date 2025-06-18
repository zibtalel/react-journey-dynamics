﻿namespace Crm.Controllers.OData
{
	using System.Reflection;
	using Crm.Library.Api.Extensions;
	using Crm.Library.Api.Model;
	using Crm.Rest.Model;
	using Microsoft.OData.ModelBuilder;
	using ODataConventionModelBuilder = Crm.Library.Api.Model.ODataConventionModelBuilder;

	public class SiteFunctionConfigurator : IModelConfigurator, IExtensionValueProvider
	{
		private readonly ODataModelBuilderHelper modelHelper;
		private readonly IODataExtensionValueTypeBuilder extensionValueTypeBuilder;
		public SiteFunctionConfigurator(ODataModelBuilderHelper modelHelper, IODataExtensionValueTypeBuilder extensionValueTypeBuilder)
		{
			this.modelHelper = modelHelper;
			this.extensionValueTypeBuilder = extensionValueTypeBuilder;
		}
		public virtual void Configure(ODataConventionModelBuilder builder)
		{
			var getActivePluginNames = builder.EntityType<Site>()
				.Collection
				.Function(nameof(SiteController.GetActivePluginNames))
				.ReturnsCollection<string>();
			getActivePluginNames.Title = "gets the currently active plugins";

			var getCurrentSite = builder.EntityType<Site>()
				.Collection
				.Function(nameof(SiteController.GetCurrentSite))
				.ReturnsFromEntitySet<Site>(modelHelper.GetEntityTypeName(typeof(Library.Model.Site.Site)));
			getCurrentSite.Title = "gets the current site";

			var getMaterialThemes = builder.EntityType<Site>()
				.Collection
				.Function(nameof(SiteController.GetMaterialThemes))
				.ReturnsCollection<Theme>();
			getMaterialThemes.Title = "gets themes for material client";

			var getPlugins = builder.EntityType<Site>()
				.Collection
				.Function(nameof(SiteController.GetPlugins))
				.ReturnsCollection<PluginDescriptor>();
			getPlugins.Title = "gets all known plugins";

			var setActivePluginNames = builder.EntityType<Site>()
				.Collection
				.Action(nameof(SiteController.SetActivePlugins))
				.ReturnsCollection<string>();
			setActivePluginNames.Title = "sets the active plugins of the site";
			setActivePluginNames.CollectionParameter<string>(SiteController.ParameterPluginNames).NotNullable();

			builder.EntitySetTypes.Remove(typeof(Site));
		}

		public virtual PropertyInfo Get(ODataConventionModelBuilder builder, EntityTypeConfiguration configuration)
		{
			if (configuration.ClrType == typeof(Site))
			{
				return extensionValueTypeBuilder.CreateMergedExtensionValuesProperty(modelHelper.GetPluginName(typeof(Site)), typeof(Library.Model.Site.Site), typeof(Site));
			}

			return null;
		}
	}
}
