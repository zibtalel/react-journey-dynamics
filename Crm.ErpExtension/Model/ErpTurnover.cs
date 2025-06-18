namespace Crm.ErpExtension.Model
{
	using System;

	using Crm.Library.BaseModel;
	using Crm.Library.BaseModel.Interfaces;

	public class ErpTurnover : EntityBase<Guid>, ISoftDelete
	{
		public virtual string ArticleGroup01Key { get; set; }
		public virtual string ArticleGroup02Key { get; set; }
		public virtual string ArticleGroup03Key { get; set; }
		public virtual string ArticleGroup04Key { get; set; }
		public virtual string ArticleGroup05Key { get; set; }
		public virtual Guid? ContactKey { get; set; }
		public virtual string CurrencyKey { get; set; }
		public virtual bool IsVolume { get; set; }
		public virtual string ItemDescription { get; set; }
		public virtual string ItemNo { get; set; }
		public virtual string LegacyId { get; set; }
		public virtual decimal? Month1 { get; set; }
		public virtual decimal? Month2 { get; set; }
		public virtual decimal? Month3 { get; set; }
		public virtual decimal? Month4 { get; set; }
		public virtual decimal? Month5 { get; set; }
		public virtual decimal? Month6 { get; set; }
		public virtual decimal? Month7 { get; set; }
		public virtual decimal? Month8 { get; set; }
		public virtual decimal? Month9 { get; set; }
		public virtual decimal? Month10 { get; set; }
		public virtual decimal? Month11 { get; set; }
		public virtual decimal? Month12 { get; set; }
		public virtual string QuantityUnitKey { get; set; }
		public virtual decimal? Total { get; set; }
		public virtual int Year { get; set; }
	}
}