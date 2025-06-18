namespace Crm.MarketInsight
{
	using Crm.Library.AutoFac;
	using Crm.Library.Helper;

	public class MarketInsightSettings : ISingletonDependency
	{
		private readonly IAppSettingsProvider appSettingsProvider;
		public MarketInsightSettings(IAppSettingsProvider appSettingsProvider)
		{
			this.appSettingsProvider = appSettingsProvider;
		}
	}
}
