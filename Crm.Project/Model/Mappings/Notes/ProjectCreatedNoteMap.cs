namespace Crm.Project.Model.Mappings.Notes
{
	using Crm.Project.Model.Notes;

	using NHibernate.Mapping.ByCode.Conformist;

	public class ProjectCreatedNoteMap : SubclassMapping<ProjectCreatedNote>
	{
		public ProjectCreatedNoteMap()
		{
			DiscriminatorValue("ProjectCreatedNote");
		}
	}
}