namespace Crm.Project.Model.Lookups
{
	using Crm.Library.Globalization.Lookup;
	using System.Globalization;

	[Lookup("[LU].[ProjectLostReasonCategory]")]
	public class ProjectLostReasonCategory : EntityLookup<string>
	{
		public static readonly ProjectLostReasonCategory Competitor = new() { Key = "100", Value = "Competitor", Language = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName };
	}
}
