namespace Crm.Documentation.Model
{
	using System.Collections.Generic;

	public class Section
	{
		public string Name;
		public List<Section> SubSections = new List<Section>();
		public string Title;
	}
}