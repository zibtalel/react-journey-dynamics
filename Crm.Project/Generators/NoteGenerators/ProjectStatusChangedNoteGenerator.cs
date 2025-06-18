namespace Crm.Project.Generators.NoteGenerators
{
	using System;

	using Crm.Generators.NoteGenerators.Infrastructure;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Model.Notes;
	using Crm.Project.EventHandler;
	using Crm.Project.Model.Notes;

	public class ProjectStatusChangedNoteGenerator : NoteGenerator<ProjectStatusChangedEvent>
	{
		private readonly Func<ProjectStatusChangedNote> noteFactory;
		public override Note GenerateNote(ProjectStatusChangedEvent e)
		{
			var project = e.Project;

			var note = noteFactory();
			note.IsActive = true;
			note.ContactId = project.Id;
			note.AuthData = project.AuthData != null ? new LMobile.Unicore.EntityAuthData { DomainId = project.AuthData.DomainId } : null;
			note.Text = project.Status.Key;
			note.Plugin = "Crm.Project";

			return note;
		}
		public ProjectStatusChangedNoteGenerator(IRepositoryWithTypedId<Note, Guid> noteRepository, Func<ProjectStatusChangedNote> noteFactory)
			: base(noteRepository)
		{
			this.noteFactory = noteFactory;
		}
	}
}