namespace Crm.Project.Rest.Model
{
	using Crm.Library.Rest;
	using Crm.Project.Model.Notes;
	using Crm.Rest.Model;

	[RestTypeFor(DomainType = typeof(ProjectCreatedNote))]
    [RestTypeFor(DomainType = typeof(ProjectStatusChangedNote))]
    [RestTypeFor(DomainType = typeof(ProjectLostNote))]
	public class ProjectNoteRest : NoteRest
	{
	}
}
