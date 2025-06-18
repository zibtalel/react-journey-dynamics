using Microsoft.AspNetCore.Mvc;

namespace Crm.PerDiem.Controllers
{
	using System;

	using Crm.Controllers;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization;
	using Crm.Library.Services.Interfaces;
	using Crm.Library.ViewModels;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.Services.Interfaces;
	using Crm.Services;

	using Microsoft.AspNetCore.Authorization;

	public class PerDiemReportController : CrmController
	{
		private readonly IRepositoryWithTypedId<PerDiemReport, Guid> reportRepository;
		private readonly IPerDiemReportService reportService;
		[RequiredPermission(PermissionName.View, Group = PerDiemPlugin.PermissionGroup.PerDiemReport)]
		public virtual ActionResult Details()
		{
			return PartialView();
		}

		[AllowAnonymous]
		[RenderAction("PerDiemReport", Priority = 50)]
		public virtual ActionResult PerDiemReportAttachments()
		{
			return PartialView();
		}

		[AllowAnonymous]
		[RenderAction("PerDiemReportOverviewEntry", Priority = 100)]
		public virtual ActionResult PerDiemReportOverviewEntryUserExpense()
		{
			return PartialView();
		}

		[AllowAnonymous]
		[RenderAction("PerDiemReportOverviewEntry", Priority = 100)]
		public virtual ActionResult PerDiemReportOverviewEntryUserTimeEntry()
		{
			return PartialView();
		}

		[AllowAnonymous]
		[RenderAction("PerDiemReport", Priority = 70)]
		public virtual ActionResult PerDiemReportOverviewTable()
		{
			return PartialView();
		}

		[AllowAnonymous]
		[RenderAction("PerDiemReportOverviewTableHead", Priority = 100)]
		public virtual ActionResult PerDiemReportOverviewTableHead()
		{
			return PartialView();
		}

		[AllowAnonymous]
		[RenderAction("PerDiemReport", Priority = 80)]
		public virtual ActionResult PerDiemReportOverviewTitle()
		{
			return PartialView();
		}

		[AllowAnonymous]
		[RenderAction("PerDiemReportOverviewUser", Priority = 100)]
		public virtual ActionResult PerDiemReportOverviewUser()
		{
			return PartialView();
		}

		[AllowAnonymous]
		[RenderAction("PerDiemReport", Priority = 60)]
		public virtual ActionResult PerDiemReportStatusUsers()
		{
			return PartialView();
		}

		[AllowAnonymous]
		[RenderAction("PerDiemReport", Priority = 100)]
		public virtual ActionResult PerDiemReportTitle()
		{
			return PartialView();
		}

		[AllowAnonymous]
		[RenderAction("PerDiemReport", Priority = 90)]
		public virtual ActionResult PerDiemReportUsers()
		{
			return PartialView();
		}

		public virtual ActionResult Report()
		{
			return View();
		}

		public virtual ActionResult ReportPdf(Guid reportId)
		{
			var report = reportRepository.Get(reportId);
			var pdf = reportService.CreateReportAsPdf(report);
			var name = reportService.GetReportName(report);
			return Pdf(pdf, name);
		}

		public virtual ActionResult ReportPreview(Guid reportId)
		{
			var perDiemReport = reportRepository.Get(reportId);
			var model = new HtmlTemplateViewModel { Id = perDiemReport.Id, ViewModel = "Crm.PerDiem.ViewModels.PerDiemReportDetailsModalViewModel" };
			return View("Report", model);
		}

		[AllowAnonymous]
		[RenderAction("PerDiemReportResource", Priority = 100)]
		public virtual ActionResult ReportResources()
		{
			return PartialView();
		}

		public PerDiemReportController(
			IPdfService pdfService,
			IRenderViewToStringService renderViewToStringService,
			IRepositoryWithTypedId<PerDiemReport, Guid> reportRepository,
			IPerDiemReportService reportService)
			: base(
				pdfService,
				renderViewToStringService)
		{
			this.reportRepository = reportRepository;
			this.reportService = reportService;
		}
	}
}