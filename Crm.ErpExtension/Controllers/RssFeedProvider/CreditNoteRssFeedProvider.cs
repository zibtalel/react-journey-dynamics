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

	public class CreditNoteRssFeedProvider : RssFeedProvider<CreditNote>
	{
		private readonly IResourceManager resourceManager;
		protected override SyndicationFeedItemMapper<CreditNote> GetFeedMapper(Dictionary<string, object> argDictionary = null)
		{
			return new SyndicationFeedItemMapper<CreditNote>
				(
					x => x.OrderNo + " - " + x.CompanyName,
					x => String.Empty,
					x => "CreditNote",
					x => "DetailsTemplate",
					x => x.Id.ToString(),
					x => new RouteValueDictionary(new { plugin = "Crm.ErpExtension" }),
					x => x.CreditNoteDate ?? x.CreateDate
				);
		}
		protected override SyndicationFeedOptions GetFeedOptions(Dictionary<string, object> argDictionary = null)
		{
			return new SyndicationFeedOptions
				(
				resourceManager.GetTranslation("CreditNotes"),
				resourceManager.GetTranslation("CreditNotes"),
				absolutePathHelper.GetPath("IndexTemplate", "CreditNoteList", new RouteValueDictionary(new { plugin = "Crm.ErpExtension" }))
				);
		}

		public CreditNoteRssFeedProvider(IAbsolutePathHelper absolutePathHelper, IUserService userService, IResourceManager resourceManager, Site site, IPluginProvider pluginProvider)
			: base(absolutePathHelper, userService, site, pluginProvider)
		{
			this.resourceManager = resourceManager;
		}
	}
}
