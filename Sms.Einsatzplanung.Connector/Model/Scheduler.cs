namespace Sms.Einsatzplanung.Connector.Model
{
	using System;

	using Crm.Library.BaseModel;
	using Crm.Library.BaseModel.Attributes;
	using Crm.Library.BaseModel.Interfaces;

	public class Scheduler : EntityBase<Guid>, ISoftDelete, INoAuthorisedObject
	{
		public virtual string VersionString { get; set; }
		[Database(Ignore = true)]
		public virtual Version Version
		{
			get => Version.Parse(VersionString);
			set => VersionString = value.ToString(4);
		}
		public virtual Version ManifestVersion => new Version(Version.Major, Version.Minor, Version.Build, ClickOnceVersion);
		public virtual int ClickOnceVersion { get; set; }
		public virtual string Warnings { get; set; }
		public virtual bool IsReleased { get; set; }
		public virtual Guid? IconKey { get; set; }
		public virtual SchedulerIcon Icon { get; set; }
		public virtual Guid? ConfigKey { get; set; }
		public virtual SchedulerConfig Config { get; set; }
	}
}