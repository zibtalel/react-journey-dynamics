namespace Sms.Einsatzplanung.Team
{
	using Crm.Library.Modularization;

	[Plugin(Requires = "Crm.Service,Sms.Einsatzplanung.Connector")]
	public class EinsatzplanungTeamPlugin : Plugin
	{
		public static readonly string PluginName = typeof(EinsatzplanungTeamPlugin).Namespace;
	}
}