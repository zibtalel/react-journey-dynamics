namespace Crm.Documentation.Service
{
	using System.Text.RegularExpressions;

	public static class Slugger
	{
		//from https://bitbucket.org/earlz/lucidmvc/src/9cfe0c6753702357b00bf272d10e35284cbf5b4a/LucidMVC/Routing/Routing.cs?at=default
		static public int SlugMaxWords = 7;
		static public int SlugMaxChars = 70;
		/// <summary>
		/// Will match everything that isn't alphanumeric, dash, or space
		/// </summary>
		static readonly Regex NonAlphaNumeric;
		/// <summary>
		/// This is considered a "just-in-case" type thing. After the slug is created, this will run to find
		/// any possibly unexpected output. This is to prevent redirect scams and generally unsafe things (like breaking HTML and other fun stuff)
		/// </summary>
		static readonly Regex SafetyStrip;
		static Slugger()
		{
			NonAlphaNumeric = new Regex(@"[^a-zA-Z0-9]\ ", RegexOptions.Compiled);
			SafetyStrip = new Regex(@"[^a-zA-Z0-9\-.]", RegexOptions.Compiled);
		}
		/// <summary>
		/// Will strip all non-alphanumeric characters and replace all spaces with `-` to make a URL friendly "slug"
		/// </summary>
		static public string Slugify(string text)
		{
			string tmp = NonAlphaNumeric.Replace(text, " ").Replace(" ", "-").ToLower();
			return SafetyStrip.Replace(tmp, "");
		}
	}
}