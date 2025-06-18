namespace Sms.Einsatzplanung.Connector.ViewModels
{
	using System;
	using System.IO;

	public class SchedulerConfigViewModel : IDisposable
	{
		public FileInfo FileInfo { get; set; }
		public SchedulerConfigType Type { get; set; }
		public void Dispose()
		{
			if (FileInfo.Exists)
			{
				FileInfo.Delete();
			}
		}
	}
}