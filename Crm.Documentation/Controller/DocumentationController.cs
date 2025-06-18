using Microsoft.AspNetCore.Mvc;

namespace Crm.Documentation.Controller
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.IO;
	using System.Linq;

	using Crm.Documentation.Service;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Model.Site;
	using Crm.Library.Modularization.Extensions;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Library.Services.Interfaces;
	using Controller = Microsoft.AspNetCore.Mvc.Controller;
	using PermissionGroup = DocumentationPlugin.PermissionGroup;

	public class DocumentationController : Controller
	{
		private readonly Site site;
		private readonly IUserService userService;
		private readonly IPluginProvider pluginProvider;
		private readonly IEnvironment environment;

		[RequiredPermission(PermissionName.View, Group = PermissionGroup.ApplicationHelp)]
		public virtual ActionResult Index()
		{
			var user = userService.CurrentUser;
			var pluginNames = pluginProvider.SortByDependency(pluginProvider.ActivePluginDescriptors).Select(x => x.PluginName);
			var culture = GetCulture(site, user);
			var documentationService = new DocumentationService(DocumentationMode.ServeIntegrated, "img", culture);
			var toc = pluginNames.Select(x => string.Format("{0}/Documentation/{1}/{0}.md", x, culture.TwoLetterISOLanguageName));

			var servingDirectory = environment.MapPath("~/Plugins").FullName;
			var model = documentationService.GenerateDocumentationModel(servingDirectory, toc);

			model.ResourcePath = Url.Content("~/Plugins/Crm.Documentation/Content");

			return View(model);
		}

		[RequiredPermission(PermissionName.View, Group = PermissionGroup.DeveloperHelp)]
		public virtual ActionResult DeveloperIndex()
		{
			var pluginRoot = "~/Plugins";
			var pluginNames = pluginProvider.SortByDependency(pluginProvider.ActivePluginDescriptors).Select(x => x.PluginName);
			var documentationService = new DocumentationService(DocumentationMode.ServeIntegrated, "img", CultureInfo.GetCultureInfo("en"));
			var tocRoots = pluginNames.Select(x => string.Format("{0}/Documentation/dev/", x));
			var servingDirectory = environment.MapPath(pluginRoot).FullName;

			var toc = new List<string>();
			foreach (var tocRoot in tocRoots)
			{
				var tocPath = Path.Combine(environment.MapPath(Path.Combine(pluginRoot, tocRoot)).FullName);
				var tocFile = Path.Combine(tocPath, "toc.txt");
				if (System.IO.File.Exists(tocFile))
				{
					var entries = System.IO.File.ReadAllLines(tocFile);
					foreach (var entry in entries.Where(x => !x.StartsWith("#")))
					{
						foreach (var file in Directory.GetFiles(Path.Combine(tocPath, entry), "*.md"))
						{
							toc.Add(file);
						}
					}
				}
			}

			var model = documentationService.GenerateDocumentationModel(servingDirectory, toc);

			model.ResourcePath = Url.Content("~/Plugins/Crm.Documentation/Content");

			return View("Index", model);
		}

		public static Uri UriAppend(Uri uri, params string[] paths)
		{
			return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => string.Format("{0}/{1}", current.TrimEnd('/'), path.TrimStart('/'))));
		}
		protected virtual CultureInfo GetCulture(Site site, User user)
		{
			var culture = CultureInfo.GetCultureInfo("de");
			if (site != null && !String.IsNullOrWhiteSpace(site.GetExtension<DomainExtension>().DefaultLanguageKey))
			{
				culture = new CultureInfo(site.GetExtension<DomainExtension>().DefaultLanguageKey);
			}
			if (user != null && !String.IsNullOrWhiteSpace(user.DefaultLanguageKey))
			{
				culture = new CultureInfo(user.DefaultLanguageKey);
			}
			return culture;
		}

		public DocumentationController(Site site, IUserService userService, IPluginProvider pluginProvider, IEnvironment environment)
		{
			this.site = site;
			this.userService = userService;
			this.pluginProvider = pluginProvider;
			this.environment = environment;
		}
	}
}
