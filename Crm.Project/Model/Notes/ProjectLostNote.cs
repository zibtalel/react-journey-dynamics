namespace Crm.Project.Model.Notes
{
	using Crm.Model.Notes;
	using Crm.Project.Model.Lookups;
	using System;

	public class ProjectLostNote : Note
	{
		public override string DisplayText
		{
			get
			{
				var noteTexts = Text.Split(new string[] { ";|;" }, StringSplitOptions.None);

				if (!Text.Contains(";|;") && noteTexts.Length != 3)
				{
					return Text;
				}
				var displayText = ResourceManager.Instance.GetTranslation("ProjectLost");
				displayText += noteTexts[0] == "" ? "" : '\n' + ResourceManager.Instance.GetTranslation("Category") + ": " + noteTexts[0];
				displayText += noteTexts[1] == "" ? "" : '\n' + noteTexts[1] + ResourceManager.Instance.GetTranslation("Competitor");
				displayText += noteTexts[2] == "" ? "" : '\n' + ResourceManager.Instance.GetTranslation("Remark") + ": " + noteTexts[2];
				return displayText;
			}
		}
		public override string ImageColor
		{
			get { return LookupManager.Get<ProjectStatus>(ProjectStatus.LostKey).Color; }
		}
		public override string ImageTextKey
		{
			get { return "Status"; }
		}
		public override string PermanentLabelResourceKey
		{
			get { return "ProjectStatusSetBy"; }
		}

		public ProjectLostNote()
		{
			Plugin = ProjectPlugin.PluginName;
		}
	}
}