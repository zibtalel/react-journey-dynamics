namespace Crm.MarketInsight.Rest.Model
{
	using System;

	using Crm.Article.Rest.Model;
	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;
	using Crm.MarketInsight.Model;
	using Crm.Rest.Model;

	[RestTypeFor(DomainType = typeof(MarketInsight))]
	public class MarketInsightRest : ContactRest
	{
		public Guid ProductFamilyKey { get; set; }
		public virtual string ReferenceKey { get; set; }
		public virtual string StatusKey { get; set; }
		[NotReceived, ExplicitExpand] public ProductFamilyRest ProductFamily { get; set; }
		[NotReceived, ExplicitExpand] public CompanyRest Company { get; set; }


	}
}
