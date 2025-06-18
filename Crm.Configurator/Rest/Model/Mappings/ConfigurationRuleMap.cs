namespace Crm.Configurator.Rest.Model.Mappings
{
	using System.Linq;

	using AutoMapper;

	using Crm.Article.Services.Interfaces;
	using Crm.Configurator.Model;
	using Crm.Library.AutoMapper;

	public class ConfigurationRuleMap : IAutoMap
	{
		public virtual void CreateMap(IProfileExpression mapper)
		{
			mapper.CreateMap<ConfigurationRule, ConfigurationRuleRest>()
				.ForMember(x => x.AffectedVariableValues, m => m.MapFrom(x => x.AffectedVariableValues.Select(y => y.Id)))
				.ForMember(x => x.VariableValues, m => m.MapFrom(x => x.VariableValues.Select(y => y.Id)));

			mapper.CreateMap<ConfigurationRuleRest, ConfigurationRule>()
				.ForMember(x => x.AffectedVariableValues, m => m.MapFrom((source, destination, member, context) => source.AffectedVariableValues.Select(id => context.GetService<IArticleService>().GetArticle(id))))
				.ForMember(d => d.VariableValues, m => m.MapFrom((source, destination, member, context) => source.VariableValues.Select(id => context.GetService<IArticleService>().GetArticle(id))));
		}
	}
}
