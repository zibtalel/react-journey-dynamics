namespace Crm.MarketInsight.Rest.Model.Mappings
{
	using AutoMapper;

	using Crm.Library.AutoMapper;
	using Crm.MarketInsight.Model.Relationships;
	using Crm.Model;

	public class MarketInsightContactRelationshipRestMap : IAutoMap
	{
		public virtual void CreateMap(IProfileExpression mapper)
		{
			mapper.CreateMap<MarketInsightContactRelationship, MarketInsightContactRelationshipRest>()
				.ForMember(x => x.MarketInsight, m => m.MapFrom(x => x.Parent))
				.ForMember(x => x.ChildPerson, m => m.MapFromProxy(x => x.Child).As<Person>())
				.ForMember(x => x.ChildCompany, m => m.MapFromProxy(x => x.Child).As<Company>());
		}
	}
}
