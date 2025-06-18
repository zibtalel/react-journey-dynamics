namespace Crm.PerDiem.Model.Mappings
{
	using System;

	using Crm.Library.BaseModel.Mappings;

	using NHibernate.Mapping.ByCode;

	using GuidCombGeneratorDef = LMobile.Unicore.NHibernate.GuidCombGeneratorDef;

	public class UserTimeEntryMap : EntityClassMapping<UserTimeEntry>
	{
		public UserTimeEntryMap()
		{
			Schema("SMS");
			Table("TimeEntry");

			Id(
				x => x.Id,
				map =>
				{
					map.Column("TimeEntryId");
					map.Generator(GuidCombGeneratorDef.Instance);
					map.UnsavedValue(Guid.Empty);
				});

			Property(x => x.Description, m => m.Length(Int32.MaxValue));
			Property(
				x => x.Date,
				map =>
				{
					map.Column("`Date`");
					map.Insert(true);
					map.Update(true);
				});

			Property(x => x.TimeEntryTypeKey, map => map.Column("TimeEntryType"));
			Property(x => x.DurationInMinutes);
			Property(
				x => x.ResponsibleUser,
				map =>
				{
					map.Column("ResponsibleUser");
					map.Insert(true);
					map.Update(true);
				});
			Property(x => x.IsClosed, map => map.Column("Closed"));
			Property(x => x.From, map => map.Column("`From`"));
			Property(x => x.To, map => map.Column("`To`"));
			Property(x => x.CostCenterKey, m => m.Column("CostCenter"));
			Property(x => x.PerDiemReportId);

			ManyToOne(
				x => x.PerDiemReport,
				map =>
				{
					map.Column("PerDiemReportId");
					map.Fetch(FetchKind.Select);
					map.Lazy(LazyRelation.Proxy);
					map.Cascade(Cascade.None);
					map.Insert(false);
					map.Update(false);
				});
			ManyToOne(
				x => x.ResponsibleUserObject,
				map =>
				{
					map.Column("ResponsibleUser");
					map.Fetch(FetchKind.Select);
					map.Lazy(LazyRelation.Proxy);
					map.Cascade(Cascade.None);
					map.Insert(false);
					map.Update(false);
				});
		}
	}
}