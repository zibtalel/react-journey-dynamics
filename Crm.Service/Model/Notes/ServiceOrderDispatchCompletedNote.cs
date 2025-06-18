﻿namespace Crm.Service.Model.Notes
{
	using Crm.Model.Notes;

	public class ServiceOrderDispatchCompletedNote : Note
	{
		public override string DisplayText => ResourceManager.Instance.GetTranslation("DispatchCompleted");
		public override string ImageTextKey => "Dispatch";
		public override string PermanentLabelResourceKey => "DispatchCompletedBy";

		public ServiceOrderDispatchCompletedNote()
		{
			Plugin = ServicePlugin.PluginName;
		}
	}
}