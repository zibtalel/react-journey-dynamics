namespace Crm.ErpExtension.Model
{
	using System;
	using System.Collections.Generic;

	using Crm.Model;

	using Newtonsoft.Json;

	public abstract class ErpDocumentHead : ErpDocument
	{
		public virtual string CompanyNo { get; set; }
		public virtual string PaymentTerms { get; set; }
		public virtual string PaymentMethod { get; set; }
		public virtual string TermsOfDelivery { get; set; }
		public virtual string DeliveryMethod { get; set; }
		public virtual Guid? ContactKey { get; set; }
		[JsonIgnore]
		public virtual Contact Contact { get; set; }
		public virtual string ContactType => Contact?.ContactType;
		public virtual string OrderNo { get; set; }
		public virtual string OrderType { get; set; }
		public virtual DateTime? OrderDate { get; set; }
		public virtual string Commission { get; set; }
		public virtual string CompanyName => Contact?.LegacyName;
	}
	public abstract class ErpDocumentHead<TPosition> : ErpDocumentHead
		where TPosition : ErpDocumentPosition
	{
		public virtual ICollection<TPosition> Positions { get; set; }
	}
}