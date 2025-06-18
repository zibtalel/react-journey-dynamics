namespace Crm.MarketInsight.Rest.Model.Mappings
{
	using AutoMapper;

	using Crm.Library.AutoMapper;
	using Crm.MarketInsight.Model;
	using Crm.Model;
	using Crm.Rest.Model;

	public class MarketInsightRestMap : IAutoMap
	{
		public virtual void CreateMap(IProfileExpression mapper)
		{
			mapper.CreateMap<MarketInsight, MarketInsightRest>()
				.IncludeBase<Contact, ContactRest>();
			mapper.CreateMap<MarketInsightRest, MarketInsight>()
				.IncludeBase<ContactRest, Contact>();
		}
	}
}
