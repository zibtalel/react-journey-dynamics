namespace Crm.Documentation
{
	using Crm.Library.Modularization;

	[Plugin(Requires = "Main")]
	public class DocumentationPlugin : Plugin
	{
		public class PermissionGroup
		{
			public const string ApplicationHelp = "ApplicationHelp";
			public const string DeveloperHelp = "DeveloperHelp";
		}
	}
}