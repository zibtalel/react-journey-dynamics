namespace Crm.PerDiem.Model.Mappings
{
	using System;

	using Crm.Library.BaseModel.Mappings;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Type;

	using GuidCombGeneratorDef = LMobile.Unicore.NHibernate.GuidCombGeneratorDef;

	public class PerDiemReportMap : EntityClassMapping<PerDiemReport>
	{
		public PerDiemReportMap()
		{
			Schema("CRM");
			Table("PerDiemReport");

			Id(
				x => x.Id,
				map =>
				{
					map.Column("PerDiemReportId");
					map.Generator(GuidCombGeneratorDef.Instance);
					map.UnsavedValue(Guid.Empty);
				});
			
			Property(x => x.IsSent);
			Property(x => x.SendingError, m => m.Length(int.MaxValue));
			Property(x => x.StatusKey, map => map.Column("Status"));
			Property(x => x.From, map =>
			{
				map.Column("`From`");
				map.Type<DateTimeNoMsType>();
			});
			Property(x => x.To, map =>
			{
				map.Column("`To`");
				map.Type<DateTimeNoMsType>();
			});

			Property(x => x.UserName, m => m.Column("ResponsibleUser"));
			ManyToOne(x => x.User,
				m =>
				{
					m.Column("ResponsibleUser");
					m.Fetch(FetchKind.Select);
					m.Insert(false);
					m.Update(false);
				});
			
			Property(x => x.ApprovedDate);
			Property(x => x.ApprovedBy);
		}
	}
}
