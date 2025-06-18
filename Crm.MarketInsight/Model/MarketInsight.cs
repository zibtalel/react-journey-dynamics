using System;
using System.Collections.Generic;

namespace Crm.MarketInsight.Model
{
	using Crm.Article.Model;
	using Crm.MarketInsight.Model.Lookups;
	using Crm.MarketInsight.Model.Relationships;
	using Crm.Model;

	public class MarketInsight : Contact
	{
		public virtual ProductFamily ProductFamily { get; set; }
		public virtual Guid ProductFamilyKey { get; set; }
		public virtual string StatusKey { get; set; }
		public virtual string ReferenceKey { get; set; }
		public virtual ICollection<MarketInsightContactRelationship> ContactRelationships { get; set; }
		
		public MarketInsight()
		{
			StatusKey = MarketInsightStatus.UnqualifiedKey;
			
		}
	}
}
