namespace Crm.Documentation.ViewModel
{
	using System.Collections.Generic;

	public class DocumentationModel
	{
		public string Title { get; set; }
		public string TocCaption { get; set; }
		public string DocumentType { get; set; }
		public List<string> Authors { get; set; }
		public string Version { get; set; }
		public string Body { get; set; }
		public string ResourcePath { get; set; }
	}
}