namespace Crm.Project.Model.Mappings.Notes
{
	using Crm.Project.Model.Notes;

	using NHibernate.Mapping.ByCode.Conformist;

	public class ProjectStatusChangedNoteMap : SubclassMapping<ProjectStatusChangedNote>
	{
		public ProjectStatusChangedNoteMap()
		{
			DiscriminatorValue("ProjectStatusChangedNote");
		}
	}
}