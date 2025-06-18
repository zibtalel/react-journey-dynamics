namespace Crm.InforExtension
{
	using System;
	using System.Configuration;

	using Autofac;
	using Autofac.Core;

	using Crm.ErpExtension.Services;
	using Crm.InforExtension.Services;
	using Crm.InforExtension.Services.Interfaces;
	using Crm.Library.AutoFac;
	using Crm.Library.Environment.Logging;
	using Crm.Library.Helper;

	using Microsoft.AspNetCore.Http;

	public class InforExtensionModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register(c =>
			{
				var appSettingsProvider = c.Resolve<IAppSettingsProvider>();
				var inforErpComVersion = appSettingsProvider.GetValue(InforExtensionPlugin.Settings.System.InforErpComVersion);
				var erpConnectionString = appSettingsProvider.GetValue(InforExtensionPlugin.Settings.System.ErpConnectionString);
				if (String.Equals(inforErpComVersion, "7.1"))
				{
					return (IInforAdapter)new Infor71Adapter(erpConnectionString, c.Resolve<ILogManager>().GetLog(typeof(Infor71Adapter)), c.Resolve<IHttpContextAccessor>());
				}
				if (String.Equals(inforErpComVersion, "6.3"))
				{
					return (IInforAdapter)new Infor63Adapter(erpConnectionString, c.Resolve<ILogManager>().GetLog(typeof(Infor63Adapter)));
				}
				throw new ConfigurationErrorsException("Please specify a valid InforVersion in web.config of InforExtension plugin.");
			}).As<IInforAdapter>();
		}
	}
}
