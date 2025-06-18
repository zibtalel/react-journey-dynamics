﻿namespace Crm.Service.Model.Notes
{
	using Crm.Library.Extensions;
	using Crm.Library.Globalization.Extensions;
	using Crm.Model.Notes;

	public class ServiceCaseStatusChangedNote : Note
	{
		public override string DisplayText
		{
			get
			{
				var serviceCaseStatusValue = Text;
				return ResourceManager.Instance.GetTranslation("ServiceCaseStatusSet", searchStrategy: Search.OnlyPlugin).WithArgs(serviceCaseStatusValue);
			}
		}
		public override string ImageTextKey
		{
			get { return "Status"; }
		}
		public override string PermanentLabelResourceKey
		{
			get { return "NotificationStatusSetToBy"; }
		}

		public ServiceCaseStatusChangedNote()
		{
			Plugin = "Crm.Service";
		}
	}
}