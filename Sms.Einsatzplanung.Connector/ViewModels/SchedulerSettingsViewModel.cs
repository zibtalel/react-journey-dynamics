using System;

namespace Sms.Einsatzplanung.Connector.ViewModels
{
	public class SchedulerSettingsViewModel
	{
		public string Icon { get; set; }
		public Guid? ConfigId { get; set; }
		public string ConfigFileName { get; set; }
		public string Name { get; set; }
		public string Flavor { get; set; }
	}
}