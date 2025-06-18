namespace Crm.Project.Generators.NoteGenerators
{
	using System;

	using Crm.Generators.NoteGenerators.Infrastructure;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Extensions;
	using Crm.Model.Notes;
	using Crm.Project.EventHandler;
	using Crm.Project.Model.Notes;

	public class ProjectLostNoteGenerator : NoteGenerator<ProjectLostEvent>
	{
		private readonly Func<ProjectLostNote> noteFactory;
		public override Note GenerateNote(ProjectLostEvent e)
		{
			var project = e.Project;
			var noteText = string.Format("{0};|;{1};|;{2}",
							project.ProjectLostReasonCategory != null ? project.ProjectLostReasonCategory.Value : string.Empty,
							project.Competitor != null ? project.Competitor.Name : string.Empty,
							project.ProjectLostReason.IsNotNullOrEmpty() ? project.ProjectLostReason : string.Empty);

			var note = noteFactory();
			note.IsActive = true;
			note.ContactId = project.Id;
			note.AuthData = project.AuthData != null ? new LMobile.Unicore.EntityAuthData { DomainId = project.AuthData.DomainId } : null;
			note.Text = noteText;
			note.Plugin = "Crm.Project";
			return note;
		}
		public ProjectLostNoteGenerator(IRepositoryWithTypedId<Note, Guid> noteRepository, Func<ProjectLostNote> noteFactory)
			: base(noteRepository)
		{
			this.noteFactory = noteFactory;
		}
	}
}