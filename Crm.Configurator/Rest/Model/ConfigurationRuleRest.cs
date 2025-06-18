namespace Crm.Configurator.Rest.Model
{
	using System;

	using Crm.Configurator.Model;
	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;

	[RestTypeFor(DomainType = typeof(ConfigurationRule))]
	public class ConfigurationRuleRest : RestEntityWithExtensionValues
	{
		public Guid ConfigurationBaseId { get; set; }
		public Guid[] VariableValues { get; set; }
		public Guid[] AffectedVariableValues { get; set; }
		public string Validation { get; set; }
		[NotReceived, ExplicitExpand] public ConfigurationBaseRest ConfigurationBase { get; set; }
	}
}
