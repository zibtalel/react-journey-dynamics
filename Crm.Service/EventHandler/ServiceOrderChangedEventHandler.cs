﻿namespace Crm.Service.EventHandler
{
	using System;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Model.Notes;
	using Crm.Service.Model.Notes;

	using Library.Modularization.Events;

	using Model;

	public class ServiceOrderChangedEventHandler : IEventHandler<EntityModifiedEvent<ServiceOrderHead>>
	{
		private readonly IRepositoryWithTypedId<Note, Guid> noteRepository;
		private readonly Func<OrderStatusChangedNote> orderStatusChangedNoteFactory;
		public ServiceOrderChangedEventHandler(IRepositoryWithTypedId<Note, Guid> noteRepository, Func<OrderStatusChangedNote> orderStatusChangedNoteFactory)
		{
			this.noteRepository = noteRepository;
			this.orderStatusChangedNoteFactory = orderStatusChangedNoteFactory;
		}

		public virtual void Handle(EntityModifiedEvent<ServiceOrderHead> e)
		{
			if (e.Entity.StatusKey != e.EntityBeforeChange.StatusKey && !e.Entity.IsTemplate)
			{
				CreateOrderStatusChangedNote(e);
			}
		}

		protected virtual void CreateOrderStatusChangedNote(EntityModifiedEvent<ServiceOrderHead> e)
		{
			var note = orderStatusChangedNoteFactory();
			note.IsActive = true;
			note.ContactId = e.Entity.Id;
			note.AuthData = e.Entity.AuthData != null ? new LMobile.Unicore.EntityAuthData { DomainId = e.Entity.AuthData.DomainId } : null;
			note.Text = e.Entity.StatusKey;
			note.Meta = string.Join(";", e.Entity.InvoiceReasonKey, e.Entity.InvoiceRemark);
			note.Plugin = "Crm.Service";
			noteRepository.SaveOrUpdate(note);
		}
	}
}
