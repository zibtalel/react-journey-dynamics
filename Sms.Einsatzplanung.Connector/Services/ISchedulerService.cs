namespace Sms.Einsatzplanung.Connector.Services
{
	using System;
	using System.Collections.Generic;
	using System.IO;

	using Crm.Library.AutoFac;

	using Sms.Einsatzplanung.Connector.Model;
	using Sms.Einsatzplanung.Connector.ViewModels;

	public interface ISchedulerService : IDependency
	{
		SchedulerConfig GetCurrentSchedulerConfig();
		SchedulerConfig GetSchedulerConfig(Guid id);
		SchedulerIcon GetCurrentSchedulerIcon();
		IEnumerable<Scheduler> GetSchedulers();
		IEnumerable<(FileInfo File, Version Version)> SchedulerBinaries { get; }
		void AddBinaries(string fileName, FileInfo fileInfo);
		void AddBinaries(string fileName, byte[] fileContent);
		void DeleteBinary(string fileName);
		void DeleteBinary(int key);
		string[] CreatePackage(string fileName, string baseUrl);
		void ReleasePackage(Guid schedulerId);
		void DeletePackage(Guid schedulerId);
		Version ReadVersion(FileInfo file);
		void SaveIcon(SchedulerIcon icon);
		void SaveConfig(FileInfo file, SchedulerConfigType type);
		(string Name, byte[] Content) GetDeploymentManifest(Guid schedulerId, string currentToken, string replacementAction = null);
		FileInfo GetApplicationFile(string schedulerDirectory, string relativePath);
		bool ValidateIcon(byte[] data);
		void DeleteConfig(SchedulerConfig config);
		void DeleteIcon(SchedulerIcon icon);
		bool ExtractConfig(FileInfo file, DirectoryInfo tempDir, SchedulerConfigType type, out string errorMessageKey);
	}
}
