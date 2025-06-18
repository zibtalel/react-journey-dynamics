namespace Crm.ErpExtension.Controllers.RssFeedProvider
{
	using System;
	using System.Collections.Generic;

	using Crm.ErpExtension.Model;
	using Crm.Infrastructure;
	using Crm.Library.Globalization.Resource;
	using Crm.Library.Model.Site;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Library.Services.Interfaces;
	using Crm.Results;

	using Microsoft.AspNetCore.Routing;

	public class QuoteRssFeedProvider : RssFeedProvider<Quote>
	{
		private readonly IResourceManager resourceManager;
		protected override SyndicationFeedItemMapper<Quote> GetFeedMapper(Dictionary<string, object> argDictionary = null)
		{
			return new SyndicationFeedItemMapper<Quote>
				(
				x => x.OrderNo + " - " + x.CompanyName,
				x => String.Empty,
				x => "Quote",
				x => "DetailsTemplate",
				x => x.Id.ToString(),
				x => new RouteValueDictionary(new { plugin = "Crm.ErpExtension" }),
				x => x.QuoteDate ?? x.CreateDate
				);
		}
		protected override SyndicationFeedOptions GetFeedOptions(Dictionary<string, object> argDictionary = null)
		{
			return new SyndicationFeedOptions
				(
				resourceManager.GetTranslation("Quotes"),
				resourceManager.GetTranslation("Quotes"),
				absolutePathHelper.GetPath("IndexTemplate", "QuoteList", new RouteValueDictionary(new { plugin = "Crm.ErpExtension" }))
				);
		}

		public QuoteRssFeedProvider(IAbsolutePathHelper absolutePathHelper, IUserService userService, IResourceManager resourceManager, Site site, IPluginProvider pluginProvider)
			: base(absolutePathHelper, userService, site, pluginProvider)
		{
			this.resourceManager = resourceManager;
		}
	}
}
