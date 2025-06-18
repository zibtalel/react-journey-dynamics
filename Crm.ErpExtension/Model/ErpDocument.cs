namespace Crm.ErpExtension.Model
{
	using System;

	using Crm.ErpExtension.Model.Lookups;
	using Crm.Library.BaseModel;
	using Crm.Library.BaseModel.Interfaces;
	using Crm.Model.Lookups;

	public class ErpDocument : EntityBase<Guid>, ISoftDelete
	{
		public virtual string LegacyId { get; set; }
		public virtual string DocumentType { get; set; }
		public virtual string StatusKey { get; set; }
		public virtual decimal? Total { get; set; }
		public virtual decimal? TotalWoTaxes { get; set; }
		public virtual decimal? VATLevel { get; set; }
		public virtual decimal? DiscountPercentage { get; set; }
		public virtual string CurrencyKey { get; set; }
		public virtual string Description { get; set; }
		public virtual Currency Currency
		{
			get { return CurrencyKey != null ? LookupManager.Get<Currency>(CurrencyKey) : null; }
		}
		public virtual ErpDocumentStatus Status
		{
			get { return StatusKey != null ? LookupManager.Get<ErpDocumentStatus>(Status) : null; }
		}
	}
}
