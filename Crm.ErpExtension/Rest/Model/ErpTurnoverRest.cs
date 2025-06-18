namespace Crm.ErpExtension.Rest.Model
{
	using System;

	using Crm.ErpExtension.Model;
	using Crm.Library.BaseModel;
	using Crm.Library.Rest;

	[RestTypeFor(DomainType = typeof(ErpTurnover))]
	public class ErpTurnoverRest : RestEntity
	{
		public Guid Id { get; set; }
		public string ArticleGroup01Key { get; set; }
		public string ArticleGroup02Key { get; set; }
		public string ArticleGroup03Key { get; set; }
		public string ArticleGroup04Key { get; set; }
		public string ArticleGroup05Key { get; set; }
		public Guid? ContactKey { get; set; }
		public string CurrencyKey { get; set; }
		public bool IsVolume { get; set; }
		public string ItemDescription { get; set; }
		public string ItemNo { get; set; }
		public string LegacyId { get; set; }
		public decimal? Month1 { get; set; }
		public decimal? Month2 { get; set; }
		public decimal? Month3 { get; set; }
		public decimal? Month4 { get; set; }
		public decimal? Month5 { get; set; }
		public decimal? Month6 { get; set; }
		public decimal? Month7 { get; set; }
		public decimal? Month8 { get; set; }
		public decimal? Month9 { get; set; }
		public decimal? Month10 { get; set; }
		public decimal? Month11 { get; set; }
		public decimal? Month12 { get; set; }
		public string QuantityUnitKey { get; set; }
		public decimal? Total { get; set; }
		public int Year { get; set; }
		public SerializableDictionary<string, object> ExtensionValues { get; set; }
	}
}
