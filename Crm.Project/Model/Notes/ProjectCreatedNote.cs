namespace Crm.Project.Model.Notes
{
	using Crm.Model.Notes;
	using Crm.Project.Model.Lookups;

	public class ProjectCreatedNote : Note
	{
		public override string DisplayText
		{
			get { return ResourceManager.Instance.GetTranslation("ProjectCreated"); }
		}
		public override string ImageColor
		{
			get { return LookupManager.Get<ProjectStatus>(ProjectStatus.OpenKey).Color; }
		}
		public override string ImageTextKey
		{
			get { return "New"; }
		}
		public override string PermanentLabelResourceKey
		{
			get { return "ProjectCreatedBy"; }
		}

		public ProjectCreatedNote()
		{
			Plugin = ProjectPlugin.PluginName;
		}
	}
}