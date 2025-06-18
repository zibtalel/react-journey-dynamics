namespace Crm.Configurator.Rest.Model
{
	using Crm.Article.Rest.Model;
	using Crm.Configurator.Model;
	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;

	[RestTypeFor(DomainType = typeof(Variable))]
	public class VariableRest : BaseArticleRest
	{
		[NotReceived, ExplicitExpand] public ConfigurationBaseRest ConfigurationBase { get; set; }
	}
}
