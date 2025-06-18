namespace Sms.Einsatzplanung.Connector.Model
{
	using System;
	using System.IO;

	public class SchedulerBinary : IDisposable
	{
		public string Filename { get; set; }
		public FileInfo FileInfo { get; set; }

		public void Dispose()
		{
			if (FileInfo.Exists)
			{
				FileInfo.Delete();
			}
		}
	}
}