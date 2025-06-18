namespace Crm.PerDiem.Services.Interfaces
{
	using System.Net.Mail;

	using Crm.Library.AutoFac;
	using Crm.Model;
	using Crm.PerDiem.Model;

	public interface IPerDiemReportProvider : IDependency
	{
		FileResource[] GetFileResources(PerDiemReport perDiemReport);
		Attachment[] GetAttachments(PerDiemReport perDiemReport);
		PerDiemEntry[] GetEntries(PerDiemReport perDiemReport);
	}
}