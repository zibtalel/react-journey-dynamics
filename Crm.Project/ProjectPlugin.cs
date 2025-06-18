namespace Crm.Project
{
	using Crm.Library.Configuration;
	using Crm.Library.Modularization;

	[Plugin(ModuleId = "FLD03060", Requires = "Main, Crm.Article")]
	public class ProjectPlugin : Plugin
	{
		public static readonly string PluginName = typeof(ProjectPlugin).Namespace;

		public static class PermissionGroup
		{
			public const string Project = "Project";
			public const string Potential = "Potential";
			public const string DocumentEntry = "DocumentEntry";
		}

		public static class PermissionName
		{
			public const string ProjectTab = "ProjectTab";
			public const string HeaderStatus = "HeaderStatus";
			public const string ChangeFinalizedProjectStatus = "ChangeFinalizedProjectStatus";
			public const string SelectResponsible = "SelectResponsible";
			public const string EditContactRelationship = "EditContactRelationship";
			public const string RemoveContactRelationship = "RemoveContactRelationship";
			public const string SetStatus = "SetStatus";
			public const string PotentialTab = "PotentialTab";
			public const string ContactHistoryTab = "ContactHistoryTab";
			public const string EditContactPerson = "EditContactPerson";
		}

		public static class Settings
		{
			public static class Bravo
			{
				public static SettingDefinition<bool> BravoEnabled => new SettingDefinition<bool>("Configuration/BravoActiveForProjects", PluginName);
			}
			public static SettingDefinition<bool> ProjectsHaveAddresses => new SettingDefinition<bool>("ProjectsHaveAddresses", PluginName);
		}
	}
}