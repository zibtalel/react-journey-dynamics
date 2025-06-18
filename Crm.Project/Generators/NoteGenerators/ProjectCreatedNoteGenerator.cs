namespace Crm.Project.Generators.NoteGenerators
{
	using System;

	using Crm.Generators.NoteGenerators.Infrastructure;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Model.Notes;
	using Crm.Project.EventHandler;
	using Crm.Project.Model.Lookups;
	using Crm.Project.Model.Notes;

	public class ProjectCreatedNoteGenerator : NoteGenerator<ProjectCreatedEvent>
	{
		private readonly Func<ProjectCreatedNote> noteFactory;
		public override Note GenerateNote(ProjectCreatedEvent e)
		{
			var project = e.Project;

			var note = noteFactory();
			note.IsActive = true;
			note.ContactId = project.Id;
			note.AuthData = project.AuthData != null ? new LMobile.Unicore.EntityAuthData { DomainId = project.AuthData.DomainId } : null;
			note.Text = ProjectStatus.OpenKey;
			note.Plugin = "Crm.Project";
			return note;
		}
		public ProjectCreatedNoteGenerator(IRepositoryWithTypedId<Note, Guid> noteRepository, Func<ProjectCreatedNote> noteFactory)
			: base(noteRepository)
		{
			this.noteFactory = noteFactory;
		}
	}
}