namespace Crm.Configurator.Rest.Model
{
	using Crm.Article.Rest.Model;
	using Crm.Configurator.Model;
	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;

	[RestTypeFor(DomainType = typeof(ConfigurationBase))]
	public class ConfigurationBaseRest : BaseArticleRest
	{
		[NotReceived, ExplicitExpand] public ConfigurationRuleRest[] ConfigurationRules { get; set; }
		[NotReceived, ExplicitExpand] public VariableRest[] Variables { get; set; }
	}
}
