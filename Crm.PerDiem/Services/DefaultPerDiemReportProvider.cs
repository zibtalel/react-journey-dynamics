namespace Crm.PerDiem.Services
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Net.Mail;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Model;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.Services.Interfaces;

	public class DefaultPerDiemReportProvider : IPerDiemReportProvider
	{
		private readonly IRepositoryWithTypedId<UserExpense, Guid> userExpenseRepository;
		private readonly IRepositoryWithTypedId<PerDiemEntry, Guid> perDiemEntryRepository;
		public DefaultPerDiemReportProvider(IRepositoryWithTypedId<UserExpense, Guid> userExpenseRepository, IRepositoryWithTypedId<PerDiemEntry, Guid> perDiemEntryRepository)
		{
			this.userExpenseRepository = userExpenseRepository;
			this.perDiemEntryRepository = perDiemEntryRepository;
		}
		public virtual FileResource[] GetFileResources(PerDiemReport perDiemReport)
		{
			var expenses = userExpenseRepository.GetAll().Where(x => x.PerDiemReportId == perDiemReport.Id);
			return expenses.Where(x => x.FileResource != null).Select(x => x.FileResource).ToArray();
		}
		public virtual Attachment[] GetAttachments(PerDiemReport perDiemReport)
		{
			return GetFileResources(perDiemReport).Select(x => new Attachment(new MemoryStream(x.Content), x.Filename, x.ContentType)).ToArray();
		}
		public virtual PerDiemEntry[] GetEntries(PerDiemReport perDiemReport)
		{
			return perDiemEntryRepository.GetAll().Where(x => x.PerDiemReportId == perDiemReport.Id).ToArray();
		}
	}
}
