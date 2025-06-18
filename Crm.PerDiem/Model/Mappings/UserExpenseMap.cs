namespace Crm.PerDiem.Model.Mappings
{
	using System;

	using Crm.Library.BaseModel.Mappings;

	using NHibernate.Mapping.ByCode;

	using GuidCombGeneratorDef = LMobile.Unicore.NHibernate.GuidCombGeneratorDef;

	public class UserExpenseMap : EntityClassMapping<UserExpense>
	{
		public UserExpenseMap()
		{
			Schema("SMS");
			Table("Expense");

			Id(
				x => x.Id,
				map =>
				{
					map.Column("ExpenseId");
					map.Generator(GuidCombGeneratorDef.Instance);
					map.UnsavedValue(Guid.Empty);
				});

			Property(x => x.Description);
			Property(x => x.Date);
			Property(x => x.ExpenseTypeKey, map => map.Column("ExpenseType"));
			Property(x => x.Amount);
			Property(x => x.ResponsibleUser);
			Property(x => x.CurrencyKey);
			Property(x => x.IsActive);
			Property(x => x.IsClosed, map => map.Column("Closed"));
			Property(x => x.IsActive);
			Property(x => x.FileResourceKey);
			Property(x => x.CostCenterKey, m => m.Column("CostCenter"));
			Property(x => x.PerDiemReportId);

			ManyToOne(
				x => x.FileResource,
				map =>
				{
					map.Column("FileResourceKey");
					map.Fetch(FetchKind.Select);
					map.Lazy(LazyRelation.Proxy);
					map.Cascade(Cascade.Remove);
					map.Insert(false);
					map.Update(false);
				});

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
			ManyToOne(x => x.ResponsibleUserObject,
				m =>
				{
					m.Column("ResponsibleUser");
					m.Fetch(FetchKind.Select);
					m.Insert(false);
					m.Update(false);
				});
		}
	}
}