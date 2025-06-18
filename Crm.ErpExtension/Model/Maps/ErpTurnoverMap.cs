namespace Crm.ErpExtension.Model.Maps
{
	using System;

	using Crm.Library.BaseModel.Mappings;

	public class ErpTurnoverMap : EntityClassMapping<ErpTurnover>
	{
		public ErpTurnoverMap()
		{
			Schema("CRM");
			Table("Turnover");

			Id(a => a.Id, m =>
			{
				m.Column("TurnoverId");
				m.Generator(LMobile.Unicore.NHibernate.GuidCombGeneratorDef.Instance);
				m.UnsavedValue(Guid.Empty);
			});

			Property(x => x.ArticleGroup01Key);
			Property(x => x.ArticleGroup02Key);
			Property(x => x.ArticleGroup03Key);
			Property(x => x.ArticleGroup04Key);
			Property(x => x.ArticleGroup05Key);
			Property(x => x.ContactKey);
			Property(x => x.CurrencyKey);
			Property(x => x.IsVolume);
			Property(x => x.ItemDescription);
			Property(x => x.ItemNo);
			Property(x => x.LegacyId);
			Property(x => x.Month1);
			Property(x => x.Month2);
			Property(x => x.Month3);
			Property(x => x.Month4);
			Property(x => x.Month5);
			Property(x => x.Month6);
			Property(x => x.Month7);
			Property(x => x.Month8);
			Property(x => x.Month9);
			Property(x => x.Month10);
			Property(x => x.Month11);
			Property(x => x.Month12);
			Property(x => x.QuantityUnitKey);
			Property(x => x.Total);
			Property(x => x.Year);
		}
	}
}