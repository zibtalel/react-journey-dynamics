namespace Crm.PerDiem.Services.Interfaces
{
	using System.Collections.Generic;

	using Crm.Library.AutoFac;
	using Crm.Library.Services.Interfaces;
	using Crm.PerDiem.Model;

	public interface IPerDiemReportService : IDependency
	{
		byte[] CreateReportAsPdf(PerDiemReport perDiemReport);
		bool SendReportAsPdf(PerDiemReport perDiemReport);
		bool SendReportAsPdf(PerDiemReport perDiemReport, IEnumerable<string> recipientEmails);
		string GetReportName(PerDiemReport perDiemReport);
		string[] GetDefaultReportRecipientEmails();
	}
}
