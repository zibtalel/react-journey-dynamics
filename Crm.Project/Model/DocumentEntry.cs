using System;

namespace Crm.Project.Model {
	using Crm.Library.BaseModel;
	using Crm.Model;

	public class DocumentEntry : EntityBase<Guid> {
		public virtual Guid ContactKey { get; set; }
		public virtual Contact Contact { get; set; }
		public virtual Guid PersonKey { get; set; }
		public virtual Person Person { get; set; }
		public virtual DateTime SendDate { get; set; }
		public virtual DocumentAttribute Document { get; set; }
		public virtual Guid DocumentKey { get; set; }
		public virtual bool FeedbackReceived { get; set; }
	}
}
