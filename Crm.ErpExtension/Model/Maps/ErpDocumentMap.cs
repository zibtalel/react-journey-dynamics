namespace Crm.ErpExtension.Model.Maps
{
	using System;

	using Crm.Library.BaseModel.Mappings;

	public class ErpDocumentMap : EntityClassMapping<ErpDocument>
	{
		public ErpDocumentMap()
		{
			Schema("CRM");
			Table("ERPDocument");
			Discriminator(x => x.Column("DocumentType"));

			Id(a => a.Id, m =>
			{
				m.Column("ErpDocumentId");
				m.Generator(LMobile.Unicore.NHibernate.GuidCombGeneratorDef.Instance);
				m.UnsavedValue(Guid.Empty);
			});

			Property(x => x.LegacyId);
			Property(x => x.DocumentType, map =>
			{
				map.Insert(false);
				map.Update(false);
			});
			Property(x => x.Total);
			Property(x => x.TotalWoTaxes, m => m.Column("[Total wo Taxes]"));
			Property(x => x.CurrencyKey, m => m.Column("Currency"));
			Property(x => x.VATLevel);
			Property(x => x.DiscountPercentage);
			Property(x => x.StatusKey);
			Property(x => x.Description);

		}
	}
}
