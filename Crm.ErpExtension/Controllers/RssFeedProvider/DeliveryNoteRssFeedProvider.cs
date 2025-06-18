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

	public class DeliveryNoteRssFeedProvider : RssFeedProvider<DeliveryNote>
	{
		private readonly IResourceManager resourceManager;
		protected override SyndicationFeedItemMapper<DeliveryNote> GetFeedMapper(Dictionary<string, object> argDictionary = null)
		{
			return new SyndicationFeedItemMapper<DeliveryNote>
				(
				x => x.OrderNo + " - " + x.CompanyName,
				x => String.Empty,
				x => "DeliveryNote",
				x => "DetailsTemplate",
				x => x.Id.ToString(),
				x => new RouteValueDictionary(new { plugin = "Crm.ErpExtension" }),
				x => x.DeliveryNoteDate ?? x.CreateDate
				);
		}
		protected override SyndicationFeedOptions GetFeedOptions(Dictionary<string, object> argDictionary = null)
		{
			return new SyndicationFeedOptions
				(
				resourceManager.GetTranslation("DeliveryNotes"),
				resourceManager.GetTranslation("DeliveryNotes"),
				absolutePathHelper.GetPath("IndexTemplate", "DeliveryNoteList", new RouteValueDictionary(new { plugin = "Crm.ErpExtension" }))
				);
		}

		public DeliveryNoteRssFeedProvider(IAbsolutePathHelper absolutePathHelper, IUserService userService, IResourceManager resourceManager, Site site, IPluginProvider pluginProvider)
			: base(absolutePathHelper, userService, site, pluginProvider)
		{
			this.resourceManager = resourceManager;
		}
	}
}
