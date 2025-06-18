using System;

namespace Crm.Project.Model.Mappings {
	using Crm.Library.BaseModel.Mappings;

	using NHibernate.Mapping.ByCode;

	public class DocumentEntryMap : EntityClassMapping<DocumentEntry> {
		public DocumentEntryMap() {
			Schema("CRM");
			Table("DocumentEntry");

			Id(x => x.Id, m => {
				m.Column("Id");
				m.Generator(LMobile.Unicore.NHibernate.GuidCombGeneratorDef.Instance);
				m.UnsavedValue(Guid.Empty);
			});
			Property(x => x.ContactKey);
			ManyToOne(x => x.Contact, m => {
				m.Column("ContactKey");
				m.Insert(false);
				m.Update(false);
				m.Fetch(FetchKind.Select);
			});
			Property(x => x.PersonKey);
			ManyToOne(x => x.Person, m => {
				m.Column("PersonKey");
				m.Insert(false);
				m.Update(false);
				m.Fetch(FetchKind.Select);
			});
			Property(x => x.FeedbackReceived);
			Property(x => x.SendDate);
			Property(x => x.DocumentKey);
		}
	}
}