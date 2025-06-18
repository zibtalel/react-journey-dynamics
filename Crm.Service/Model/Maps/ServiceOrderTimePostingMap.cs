﻿namespace Crm.Service.Model.Maps
{
	using System;

	using Crm.Library.BaseModel.Mappings;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Type;

	public class ServiceOrderTimePostingMap : EntityClassMapping<ServiceOrderTimePosting>
	{
		public ServiceOrderTimePostingMap()
		{
			Schema("SMS");
			Table("ServiceOrderTimePostings");

			Id(x => x.Id, map =>
				{
					map.Column("id");
					map.Generator(LMobile.Unicore.NHibernate.GuidCombGeneratorDef.Instance);
					map.UnsavedValue(Guid.Empty);
				});

			Property(x => x.IsExported);
			Property(x => x.IsClosed, map => map.Column("IsClosed"));
			Property(x => x.OrderTimesId);
			ManyToOne(x => x.ServiceOrderTime, map =>
			{
				map.Column("OrderTimesId");
				map.Fetch(FetchKind.Select);
				map.Lazy(LazyRelation.Proxy);
				map.Cascade(Cascade.None);
				map.Insert(false);
				map.Update(false);
			});
			Property(x => x.OrderId);
			Property(x => x.UserId);
			Property(x => x.Date, map =>
			{
				map.Column("[Date]");
				map.Insert(true);
				map.Update(true);
			});
			Property(x => x.From, map =>
			{
				map.Column("[From]");
				map.Insert(true);
				map.Update(true);
			});
			Property(x => x.To, map =>
			{
				map.Column("[To]");
				map.Insert(true);
				map.Update(true);
			});
			Property(x => x.DurationInMinutes);
			Property(x => x.PlannedDurationInMinutes);
			Property(x => x.Price);
			Property(x => x.DiscountPercent);
			Property(x => x.Description, m => m.Length(Int32.MaxValue));
			Property(x => x.InternalRemark, m => m.Length(Int32.MaxValue));
			Property(x => x.UserUsername);
			Property(x => x.ResponsibleUser, m =>
			{
				m.Column("UserUsername");
				m.Insert(false);
				m.Update(false);
			});
			Property(x => x.ItemNo);
			Property(x => x.DispatchId);
			Property(x => x.SignedByCustomer);
			Property(x => x.BreakInMinutes);
			Property(x => x.Kilometers);
			Property(x => x.CostCenterKey, m => m.Column("CostCenter"));
			Property(x => x.PerDiemReportId);

			Property(x => x.ArticleId);
			ManyToOne(x => x.Article, map =>
			{
				map.Column("ArticleId");
				map.Fetch(FetchKind.Select);
				map.Lazy(LazyRelation.Proxy);
				map.Cascade(Cascade.None);
				map.Insert(false);
				map.Update(false);
			});
			ManyToOne(x => x.User,
				m =>
				{
					m.Column("UserUsername");
					m.Fetch(FetchKind.Select);
					m.Insert(false);
					m.Update(false);
				});
			ManyToOne(x => x.ServiceOrderDispatch,
				m =>
				{
					m.Column("DispatchId");
					m.Fetch(FetchKind.Select);
					m.Insert(false);
					m.Update(false);
				});
			ManyToOne(x => x.ServiceOrderHead,
				m =>
				{
					m.Column("OrderId");
					m.Fetch(FetchKind.Select);
					m.Insert(false);
					m.Update(false);
				});
			ManyToOne(x => x.PerDiemReport, map =>
			{
				map.Column("PerDiemReportId");
				map.Fetch(FetchKind.Select);
				map.Lazy(LazyRelation.Proxy);
				map.Cascade(Cascade.None);
				map.Insert(false);
				map.Update(false);
			});
		}
	}
}
