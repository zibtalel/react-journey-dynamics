namespace Crm.Project.Model.Notes
{
	using System;

	using Crm.Library.Extensions;
	using Crm.Model.Notes;
	using Crm.Project.Model.Lookups;

	public class ProjectStatusChangedNote : Note
	{
		public override string DisplayText
		{
			get
			{
				var projectStatusKey = Text;
				var projectStatusValue = projectStatusKey != null ? LookupManager.Get<ProjectStatus>(projectStatusKey)?.Value ?? String.Empty : String.Empty;
				return ResourceManager.Instance.GetTranslation("ProjectStatusSet").WithArgs(projectStatusValue);
			}
		}
		public override string ImageColor
		{
			get
			{
				var projectStatus = Text != null ? LookupManager.Get<ProjectStatus>(Text) : null;
				return projectStatus != null ? projectStatus.Color : "#AAAAAA";
			}
		}
		public override string ImageTextKey
		{
			get { return "Status"; }
		}
		public override string PermanentLabelResourceKey
		{
			get { return "ProjectStatusSetBy"; }
		}

		public ProjectStatusChangedNote()
		{
			Plugin = ProjectPlugin.PluginName;
		}
	}
}