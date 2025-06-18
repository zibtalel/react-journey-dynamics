namespace Crm.Project.Model.Mappings.Notes
{
	using Crm.Project.Model.Notes;

	using NHibernate.Mapping.ByCode.Conformist;

	public class ProjectLostNoteMap : SubclassMapping<ProjectLostNote>
	{
		public ProjectLostNoteMap()
		{
			DiscriminatorValue("ProjectLostNote");
		}
	}
}