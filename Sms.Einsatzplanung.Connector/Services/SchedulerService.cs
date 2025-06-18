namespace Sms.Einsatzplanung.Connector.Services
{
	using System;
	using System.Collections.Generic;
	using System.Data.SqlClient;
	using System.Diagnostics;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.IO;
	using System.IO.Compression;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Xml;

	using Crm;
	using Crm.Library.Configuration;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Extensions;
	using Crm.Library.Helper;
	using Crm.Library.Model.Site;
	using Crm.Library.Modularization.Interfaces;

	using log4net;

	using Microsoft.Web.XmlTransform;

	using Sms.Einsatzplanung.Connector.Model;
	using Sms.Einsatzplanung.Connector.ViewModels;

	using XmlTransformationLogger = Sms.Einsatzplanung.Connector.Misc.XmlTransformationLogger;

	public class SchedulerService : ISchedulerService
	{
		private readonly ILog logger;
		private readonly IRepositoryWithTypedId<Scheduler, Guid> schedulerRepository;
		private readonly IRepositoryWithTypedId<SchedulerIcon, Guid> schedulerIconRepository;
		private readonly IRepositoryWithTypedId<SchedulerConfig, Guid> schedulerConfigRepository;
		private readonly IAppSettingsProvider appSettingsProvider;
		private readonly IConnectionStringProvider connectionStringProvider;
		private readonly Site site;
		private readonly IEnvironment environment;
		private readonly Func<SchedulerIcon> schedulerIconFactory;
		private readonly Func<SchedulerConfig> schedulerConfigFactory;
		private readonly Func<Scheduler> schedulerFactory;
		private readonly IRepository<SchedulerClickOnceVersion> clickOnceVersionRepository;
		protected virtual DirectoryInfo ClickOnceTools => new DirectoryInfo(Path.Combine(environment.RootPath.FullName, "App_Data", "ClickOnceTools"));
		protected virtual DirectoryInfo MainSchedulerDirectory
		{
			get
			{
				var scheduler = new DirectoryInfo(Path.Combine(environment.AppDataPath.FullName, "Scheduler"));
				if (scheduler.Exists == false)
				{
					scheduler.Create();
				}
				return scheduler;
			}
		}
		public virtual SchedulerConfig GetCurrentSchedulerConfig() => schedulerConfigRepository.GetAll().OrderByDescending(x => x.CreateDate).FirstOrDefault();
		public virtual SchedulerConfig GetSchedulerConfig(Guid id) => schedulerConfigRepository.Get(id);
		public virtual SchedulerIcon GetCurrentSchedulerIcon() => schedulerIconRepository.GetAll().OrderByDescending(x => x.CreateDate).FirstOrDefault();
		public virtual IEnumerable<Scheduler> GetSchedulers() => schedulerRepository.GetAll();
		public virtual IEnumerable<(FileInfo File, Version Version)> SchedulerBinaries => MainSchedulerDirectory.GetFiles("*.zip", SearchOption.TopDirectoryOnly).Select(x => (x, ReadVersion(x)));
		public SchedulerService(IRepositoryWithTypedId<Scheduler, Guid> schedulerRepository, IRepositoryWithTypedId<SchedulerIcon, Guid> schedulerIconRepository, IRepositoryWithTypedId<SchedulerConfig, Guid> schedulerConfigRepository, Site site, ILog logger, IAppSettingsProvider appSettingsProvider, IConnectionStringProvider connectionStringProvider, IEnvironment environment, Func<SchedulerIcon> schedulerIconFactory, Func<SchedulerConfig> schedulerConfigFactory, Func<Scheduler> schedulerFactory, IRepository<SchedulerClickOnceVersion> clickOnceVersionRepository)
		{
			this.schedulerRepository = schedulerRepository;
			this.schedulerIconRepository = schedulerIconRepository;
			this.schedulerConfigRepository = schedulerConfigRepository;
			this.site = site;
			this.logger = logger;
			this.appSettingsProvider = appSettingsProvider;
			this.connectionStringProvider = connectionStringProvider;
			this.environment = environment;
			this.schedulerIconFactory = schedulerIconFactory;
			this.schedulerConfigFactory = schedulerConfigFactory;
			this.schedulerFactory = schedulerFactory;
			this.clickOnceVersionRepository = clickOnceVersionRepository;
		}
		public virtual Version ReadVersion(FileInfo file)
		{
			if (TryReadVersion(file, out var version))
			{
				return version;
			}
			return null;
		}
		protected virtual bool TryReadVersion(FileInfo file, out Version version)
		{
			version = null;
			var temp = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			try
			{
				using (var zip = ZipFile.OpenRead(file.FullName))
				{
					var exe = zip.Entries.SingleOrDefault(x => x.Name.Equals("Einsatzplanung.exe"));
					if (exe == null)
					{
						return false;
					}

					exe.ExtractToFile(temp, true);
					if (Version.TryParse(FileVersionInfo.GetVersionInfo(temp).FileVersion, out version))
					{
						return true;
					}

					return false;
				}
			}
			catch (Exception e)
			{
				logger.WarnFormat("could not read file {0}: {1}", file.FullName, e);
				return false;
			}
			finally
			{
				if (File.Exists(temp))
				{
					File.Delete(temp);
				}
			}
		}
		protected virtual DirectoryInfo GetSchedulerDirectory(Scheduler scheduler, bool makeEmpty = false)
		{
			var main = MainSchedulerDirectory;
			var directory = new DirectoryInfo(Path.Combine(main.FullName, $"Scheduler_{scheduler.VersionString}_{scheduler.Id}"));
			if (makeEmpty && directory.Exists)
			{
				directory.Delete(true);
				directory.Create();
			}
			return directory;
		}
		protected virtual string RunCli(params string[] commands)
		{
			using (var cmd = new Process())
			{
				cmd.StartInfo.FileName = "cmd.exe";
				cmd.StartInfo.RedirectStandardInput = true;
				cmd.StartInfo.RedirectStandardOutput = true;
				cmd.StartInfo.RedirectStandardError = true;
				cmd.StartInfo.CreateNoWindow = true;
				cmd.StartInfo.UseShellExecute = false;
				cmd.Start();
				foreach (var command in commands)
				{
					logger.DebugFormat("executing command {0}", command);
					cmd.StandardInput.WriteLine(command);
				}
				cmd.StandardInput.Flush();
				cmd.StandardInput.Close();

				var error = cmd.StandardError.ReadToEnd();
				if (string.IsNullOrEmpty(error) == false)
				{
					logger.Error(error);
					throw new Exception(error);
				}

				var output = cmd.StandardOutput.ReadToEnd();
				logger.Debug(output);
				cmd.WaitForExit(5000);
				return output;
			}
		}
		public virtual void AddBinaries(string fileName, FileInfo fileInfo)
		{
			var fileContent = File.ReadAllBytes(fileInfo.FullName);
			AddBinaries(fileName, fileContent);
		}
		public virtual void AddBinaries(string fileName, byte[] fileContent)
		{
			var directory = MainSchedulerDirectory;
			var nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
			var extension = Path.GetExtension(fileName);
			int counter = 0;
			while (directory.GetFiles(fileName).Any())
			{
				counter++;
				fileName = nameWithoutExtension + $"_{counter}{extension}";
			}

			var filePath = Path.Combine(directory.FullName, fileName);
			File.WriteAllBytes(filePath, fileContent);

			try
			{
				using (ZipFile.OpenRead(filePath))
				{
					/* throws exception when file is not a zip */
				}
			}
			catch (Exception e)
			{
				File.Delete(filePath);
				var message = $"file does not seem to be a zip: {fileName}";
				logger.Error(message, e);
				throw new ArgumentException(message, e);
			}
		}
		public virtual void DeleteBinary(string fileName)
		{
			MainSchedulerDirectory.GetFiles(fileName).SingleOrDefault()?.Delete();
		}
		public virtual void DeleteBinary(int key)
		{
			MainSchedulerDirectory.GetFiles().SingleOrDefault(x => x.Name.GetHashCode() == key)?.Delete();
		}
		protected virtual void ApplyConfigTransformations(DirectoryInfo files, SchedulerConfig config)
		{
			var temp = new FileInfo(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));
			try
			{
				File.WriteAllBytes(temp.FullName, config.Config);
				using (var zip = ZipFile.OpenRead(temp.FullName))
				{
					foreach (var entry in zip.Entries)
					{
						var target = files.GetFiles(entry.Name).SingleOrDefault();
						if (target == null)
						{
							continue;
						}
						using (var transformationStream = entry.Open())
						using (var transformation = new XmlTransformation(transformationStream, new XmlTransformationLogger(logger)))
						{
							var targetXml = new XmlDocument();
							targetXml.Load(target.FullName);
							if (transformation.Apply(targetXml) == false)
							{
								throw new Exception($"transformation failed: {entry.Name} -> {target.Name}");
							}
							targetXml.Save(target.FullName);
						}
					}
				}
			}
			catch (Exception e)
			{
				logger.Error("could not apply configs", e);
				throw;
			}
			finally
			{
				if (temp.Exists)
				{
					temp.Delete();
				}
			}
		}
		protected virtual XmlAttribute GetApplicationDatabaseConnectionString(XmlDocument config)
		{
			var applicationDatabase = config.GetElementsByTagName("add").OfType<XmlNode>()
				.Single(x => x.Attributes?["name"]?.Value == "ApplicationDatabase");
			var connectionString = applicationDatabase.Attributes?["connectionString"];
			if (connectionString == null)
			{
				throw new Exception("connection string missing");
			}
			return connectionString;
		}
		protected virtual void CheckConfigForWarnings(DirectoryInfo files, Scheduler scheduler)
		{
			var warnings = new List<string>();
			var mainConfig = files.GetFiles("Einsatzplanung.exe.config").Single();
			var config = new XmlDocument();
			config.Load(mainConfig.FullName);
			var connectionString = GetApplicationDatabaseConnectionString(config);
			var builder = new SqlConnectionStringBuilder(connectionString.Value);
			if (builder.IntegratedSecurity == false)
			{
				warnings.Add("SchedulerConfigNoIntegratedSecurity");
			}
			if (warnings.Any())
			{
				scheduler.Warnings = string.Join(";", warnings);
			}
		}
		protected virtual string[] OverrideConfigEntries(DirectoryInfo files)
		{
			var result = new List<string>();
			var mainConfigFile = files.GetFiles("Einsatzplanung.exe.config").Single();
			var mainConfig = new XmlDocument();
			mainConfig.Load(mainConfigFile.FullName);
			var serviceConfigFile = files.GetFiles("Einsatzplanung.Service.dll.config").Single();
			var serviceConfig = new XmlDocument();
			serviceConfig.Load(serviceConfigFile.FullName);
			if (appSettingsProvider.GetValue(EinsatzplanungConnectorPlugin.Settings.System.OverrideRestEndPoint))
			{
				var restEndPoint = mainConfig.GetElementsByTagName("add")
					.OfType<XmlNode>()
					.Single(x => x.Attributes?["key"]?.Value == "Configuration/RestEndpoint")
					.Attributes?["value"];
				if (restEndPoint == null)
				{
					throw new Exception("restendpoint missing attributes");
				}
				restEndPoint.Value = site.GetExtension<DomainExtension>().Host;
				var message = $"set Configuration/RestEndpoint to {restEndPoint.Value}";
				result.Add(message);
				logger.Info(message);
			}
			if (appSettingsProvider.GetValue(EinsatzplanungConnectorPlugin.Settings.System.OverrideDatabaseHostAndCatalog))
			{
				var currentConnectionString = new SqlConnectionStringBuilder(connectionStringProvider.DbConnectionString);
				var schedulerConnectionStringAttribute = GetApplicationDatabaseConnectionString(mainConfig);
				var builder = new SqlConnectionStringBuilder(schedulerConnectionStringAttribute.Value)
				{
					DataSource = currentConnectionString.DataSource,
					InitialCatalog = currentConnectionString.InitialCatalog
				};
				schedulerConnectionStringAttribute.Value = builder.ToString();
				if (builder.IntegratedSecurity == false)
				{
					builder.Password = "********";
					builder.UserID = "********";
				}
				var message = $"set ApplicationDatabase to {builder}";
				result.Add(message);
				logger.Info(message);
			}
			if (appSettingsProvider.GetValue(EinsatzplanungConnectorPlugin.Settings.System.AppendFlavorToCompanyName))
			{
				var companyName = mainConfig.GetElementsByTagName("add")
					.OfType<XmlNode>()
					.Single(x => x.Attributes?["key"]?.Value == "Configuration/CompanyName")
					.Attributes?["value"];
				if (companyName == null)
				{
					throw new Exception("company name missing attributes");
				}
				var flavor = appSettingsProvider.GetValue(EinsatzplanungConnectorPlugin.Settings.System.SetupFlavor).Trim();
				companyName.Value = companyName.Value + (string.IsNullOrWhiteSpace(flavor) ? string.Empty : $" ({flavor})");
				var message = $"set Configuration/CompanyName to {companyName.Value}";
				result.Add(message);
				logger.Info(message);
			}
			if (appSettingsProvider.GetValue(EinsatzplanungConnectorPlugin.Settings.System.OverrideGoogleMapsApiKey))
			{
				var apiKey = serviceConfig.GetElementsByTagName("add")
					.OfType<XmlNode>()
					.Single(x => x.Attributes?["key"]?.Value == "Configuration/Maps/ApiKey")
					.Attributes?["value"];
				if (apiKey == null)
				{
					throw new Exception("api key missing attributes");
				}
				apiKey.Value = appSettingsProvider.GetValue(MainPlugin.Settings.Geocoder.GoogleMapsApiKey);
				var message = $"set Configuration/Maps/ApiKey to {apiKey.Value}";
				result.Add(message);
				logger.Info(message);
			}
			mainConfig.Save(mainConfigFile.FullName);
			serviceConfig.Save(serviceConfigFile.FullName);
			return result.ToArray();
		}
		protected virtual void EditDeploymentManifest(string path, Scheduler scheduler)
		{
			var manifest = new XmlDocument();
			manifest.Load(path);

			var nodeDeployment = manifest.GetElementsByTagName("deployment")[0];
			var nodeExpiration = manifest.GetElementsByTagName("expiration")[0];
			var nodeUpdate = manifest.GetElementsByTagName("update")[0];
			var nodeBeforeApplicationStartup = manifest.CreateElement("beforeApplicationStartup", nodeUpdate.NamespaceURI);
			nodeUpdate.ReplaceChild(nodeBeforeApplicationStartup, nodeExpiration);

			var attrMapFileExtensions = manifest.CreateAttribute("mapFileExtensions");
			attrMapFileExtensions.Value = "true";
			nodeDeployment.Attributes?.Append(attrMapFileExtensions);

			var attrMinimumRequiredVersion = manifest.CreateAttribute("minimumRequiredVersion");
			attrMinimumRequiredVersion.Value = scheduler.ManifestVersion.ToString();
			nodeDeployment.Attributes?.Append(attrMinimumRequiredVersion);

			var attrCreateDesktopShortcut = manifest.CreateAttribute("createDesktopShortcut", "urn:schemas-microsoft-com:clickonce.v1");
			attrCreateDesktopShortcut.Value = "true";
			nodeDeployment.Attributes?.Append(attrCreateDesktopShortcut);

			var schedulerDirectory = GetSchedulerDirectory(scheduler);
			var nodeDependentAssembly = manifest.GetElementsByTagName("dependentAssembly")[0];
			var attrCodebase = nodeDependentAssembly.Attributes?["codebase"];
			if (attrCodebase == null)
			{
				throw new Exception("dependentAssembly/codebase incorrect");
			}
			attrCodebase.Value = $"{schedulerDirectory.Name}\\{attrCodebase.Value}";

			manifest.Save(path);
		}
		public virtual string[] CreatePackage(string fileName, string baseUrl)
		{
			var result = new List<string>();
			var binaries = MainSchedulerDirectory.GetFiles(fileName).Single();
			var scheduler = schedulerFactory();
			scheduler.Id = Guid.NewGuid();
			scheduler.Version = ReadVersion(binaries);

			var clickOnceVersion = clickOnceVersionRepository.GetAll().Single();
			clickOnceVersion.Version++;
			scheduler.ClickOnceVersion = clickOnceVersion.Version;
			clickOnceVersionRepository.SaveOrUpdate(clickOnceVersion);

			var schedulerDirectory = GetSchedulerDirectory(scheduler, true);
			var filesDirectory = schedulerDirectory.CreateSubdirectory("files");
			ZipFile.ExtractToDirectory(binaries.FullName, filesDirectory.FullName);
			foreach (var pdb in filesDirectory.GetFiles("*.pdb", SearchOption.AllDirectories))
			{
				pdb.Delete();
			}
			var icon = GetCurrentSchedulerIcon();
			const string iconFile = "App.ico";
			if (icon != null)
			{
				File.WriteAllBytes(Path.Combine(filesDirectory.FullName, iconFile), icon.Icon);
				scheduler.IconKey = icon.Id;
				scheduler.Icon = icon;
			}
			var config = GetCurrentSchedulerConfig();
			if (config != null)
			{
				ApplyConfigTransformations(filesDirectory, config);
				scheduler.ConfigKey = config.Id;
				scheduler.Config = config;
			}
			result.AddRange(OverrideConfigEntries(filesDirectory));
			CheckConfigForWarnings(filesDirectory, scheduler);

			var mage = "\"" + Path.Combine(ClickOnceTools.FullName, "mage.exe") + "\"";
			var product = appSettingsProvider.GetValue(EinsatzplanungConnectorPlugin.Settings.System.SetupName).Trim();
			var flavor = appSettingsProvider.GetValue(EinsatzplanungConnectorPlugin.Settings.System.SetupFlavor).Trim();
			var applicationName = product + (string.IsNullOrWhiteSpace(flavor) ? string.Empty : $"({flavor})");
			var manifestIdentity = Regex.Replace($"{product}{flavor}", @"\s+", "");
			var applicationManifestFileName = $"{manifestIdentity}.exe.manifest";
			var applicationManifestPath = Path.Combine(filesDirectory.FullName, applicationManifestFileName);
			var deploymentManifestFileName = $"{manifestIdentity}.application";
			var deploymentManifestPath = Path.Combine(schedulerDirectory.FullName, deploymentManifestFileName);
			var publisher = "L-mobile solutions GmbH & Co. KG";

			var url = new UriBuilder(new Uri(baseUrl));
			url.Query = url.Query.AppendQueryString("id=" + scheduler.Id);

			var applicationManifest = new[]
			{
				mage,
				"-New Application",
				$"-ToFile \"{applicationManifestPath}\"",
				$"-Name \"{manifestIdentity}\"",
				$"-Version \"{scheduler.ManifestVersion}\"",
				$"-FromDirectory \"{filesDirectory.FullName}\"",
				"-Algorithm \"sha256RSA\"",
				"-TrustLevel \"FullTrust\"",
				$"-If {iconFile}"
			};

			var deploymentManifest = new[]
			{
				mage,
				"-New Deployment",
				$"-ToFile \"{deploymentManifestPath}\"",
				$"-Name \"{applicationName}\"",
				$"-Version \"{scheduler.ManifestVersion}\"",
				$"-Publisher \"{publisher}\"",
				$"-AppManifest \"{applicationManifestPath}\"",
				$"-ProviderURL \"{url.Uri}\"",
				"-Algorithm \"sha256RSA\"",
				"-Install \"true\""
			};

			var output = RunCli(string.Join(" ", applicationManifest), string.Join(" ", deploymentManifest));
			if (!File.Exists(deploymentManifestPath) || !File.Exists(applicationManifestPath))
			{
				throw new Exception("could not create manifests: " + output);
			}
			foreach (var fileInfo in filesDirectory.GetFiles("*.*", SearchOption.AllDirectories))
			{
				if (fileInfo.Name == applicationManifestFileName)
				{
					continue;
				}
				fileInfo.MoveTo(fileInfo.FullName + ".deploy");
			}
			EditDeploymentManifest(deploymentManifestPath, scheduler);
			schedulerRepository.SaveOrUpdate(scheduler);
			return result.ToArray();
		}
		public virtual void ReleasePackage(Guid schedulerId)
		{
			var currentRelease = schedulerRepository.GetAll().SingleOrDefault(x => x.IsReleased);
			if (currentRelease != null)
			{
				currentRelease.IsReleased = false;
				schedulerRepository.SaveOrUpdate(currentRelease);
			}
			var newRelease = schedulerRepository.Get(schedulerId);
			newRelease.IsReleased = true;
		}
		public virtual void DeletePackage(Guid schedulerId)
		{
			var scheduler = schedulerRepository.Get(schedulerId);
			var directory = GetSchedulerDirectory(scheduler);
			if (directory.Exists)
			{
				directory.Delete(true);
			}
			schedulerRepository.Delete(scheduler);
		}
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Plantafel")]
		public virtual bool ValidateIcon(byte[] data)
		{
			try
			{
				using (var stream = new MemoryStream(data))
				using (var icon = Image.FromStream(stream))
				{
					if (Equals(icon.RawFormat, ImageFormat.Icon))
					{
						return true;
					}
				}
			}
			catch (Exception e)
			{
				logger.Error("data does not seem to be an image", e);
				throw;
			}

			return false;
		}
		public virtual void DeleteConfig(SchedulerConfig config)
		{
			schedulerConfigRepository.Delete(config);
		}
		public virtual void DeleteIcon(SchedulerIcon icon)
		{
			schedulerIconRepository.Delete(icon);
		}
		public virtual void SaveIcon(SchedulerIcon icon)
		{
			if (ValidateIcon(icon.Icon))
			{
				foreach (var remove in schedulerIconRepository.GetAll().Where(x => !x.Schedulers.Any()))
				{
					if (!icon.Equals(remove))
					{
						schedulerIconRepository.Delete(remove);
					}
				}

				schedulerIconRepository.SaveOrUpdate(icon);
			}
		}
		public virtual void SaveConfig(FileInfo file, SchedulerConfigType type)
		{
			var tempDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), $"Scheduler_{Path.GetRandomFileName()}"));
			var zip = new FileInfo(Path.Combine(Path.GetTempPath(), $"SchedulerConfig_{Path.GetRandomFileName()}"));
			byte[] config;
			try
			{
				tempDir.Create();
				if (ExtractConfig(file, tempDir, type, out var errorMessageKey))
				{
					ZipFile.CreateFromDirectory(tempDir.FullName, zip.FullName);
					if (zip.Length > 1 * 1024 * 1024)
					{
						throw new Exception("created zip is too large");
					}

					config = File.ReadAllBytes(zip.FullName);
				}
				else
				{
					logger.Error($"could not extract config: {errorMessageKey}");
					return;
				}
			}
			catch (Exception e)
			{
				logger.Error("could not extract a proper config", e);
				throw;
			}
			finally
			{
				if (tempDir.Exists)
				{
					tempDir.Delete(true);
				}
				if (zip.Exists)
				{
					zip.Delete();
				}
			}
			foreach (var remove in schedulerConfigRepository.GetAll().Where(x => !x.Schedulers.Any()))
			{
				schedulerConfigRepository.Delete(remove);
			}

			var schedulerConfig = schedulerConfigFactory();
			schedulerConfig.Config = config;
			schedulerConfigRepository.SaveOrUpdate(schedulerConfig);
		}
		public virtual (string Name, byte[] Content) GetDeploymentManifest(Guid schedulerId, string currentToken, string replacementAction = null)
		{
			var scheduler = schedulerRepository.Get(schedulerId);
			var directory = GetSchedulerDirectory(scheduler);
			var manifest = directory.GetFiles("*.application", SearchOption.TopDirectoryOnly).Single();
			var manifestXml = new XmlDocument();
			manifestXml.Load(manifest.FullName);
			var deploymentProvider = manifestXml.GetElementsByTagName("deploymentProvider")[0];
			var codebase = deploymentProvider.Attributes?["codebase"];
			if (codebase == null)
			{
				throw new Exception("deploymentProvider/codebase incorrect");
			}
			var baseUri = new Uri(codebase.Value);
			var action = baseUri.Segments.Last();
			var baseUrlWithoutAction = codebase.Value.Substring(0, codebase.Value.Length - action.Length - baseUri.Query.Length);
			var url = new Uri($"{baseUrlWithoutAction}token{currentToken}/{replacementAction ?? action}{baseUri.Query}");
			codebase.Value = url.ToString();

			byte[] xml;
			using (var stream = new MemoryStream())
			using (var writer = XmlWriter.Create(stream, new XmlWriterSettings { Indent = true }))
			{
				manifestXml.Save(writer);
				xml = stream.ToArray();
			}

			return (manifest.Name, xml);
		}
		public virtual FileInfo GetApplicationFile(string schedulerDirectory, string relativePath)
		{
			var dir = MainSchedulerDirectory.GetDirectories(schedulerDirectory, SearchOption.TopDirectoryOnly).Single();
			var file = dir.GetFiles("files/" + relativePath, SearchOption.TopDirectoryOnly).Single();
			return file;
		}
		public virtual bool ExtractConfig(FileInfo file, DirectoryInfo tempDir, SchedulerConfigType type, out string errorMessageKey)
		{
			const string mainConfigName = "Einsatzplanung.exe.config";
			var mainConfig = Path.Combine(tempDir.FullName, mainConfigName);
			var isZip = false;
			var boolDiscardedFiles = false;
			var validIcon = true;
			var icon = new FileInfo("newIcon");
			try
			{
				ZipFile.ExtractToDirectory(file.FullName, tempDir.FullName);
				isZip = true;
				var configName = $"app.{type.ToString().ToLower()}.config";
				foreach (var extracted in tempDir.GetFiles("*.*", SearchOption.AllDirectories))
				{
                    var testName = extracted.Name;
					var testfull = extracted.FullName;
					string path = null;
					if (validIcon && !icon.Exists  && extracted.Name.Contains(".ico"))
                    {
						icon = extracted;
                    }
					else if(icon.Exists && extracted.Name.Contains(".ico"))
					{
						validIcon = false;
						icon = null;
					}
					else if (new Regex(@"^app\.(debug|release)\.config").IsMatch(extracted.Name))
					{
						if (extracted.Name.ToLower() == configName)
						{
							if (extracted.Directory?.Name.Contains("Einsatzplanung") == true)
							{
								path = Path.Combine(tempDir.FullName, $"{extracted.Directory.Name}.dll.config");
							}
							else
							{
								path = mainConfig;
							}
						}
						else
						{
							logger.Debug($"discarded {extracted.FullName.Replace(tempDir.FullName, string.Empty)}");
							extracted.Delete();
							continue;
						}
					}
					else if (extracted.Directory?.FullName == tempDir.FullName && new Regex(@"^Einsatzplanung\.(.*\.)?(exe|dll)\.config$").IsMatch(extracted.Name))
					{
						if (extracted.Name == mainConfigName)
						{
							path = mainConfig;
						}
						else
						{
							path = Path.Combine(tempDir.FullName, extracted.Name);
						}
					}
					if (path != null)
					{
						var source = extracted.FullName.Replace(tempDir.FullName, string.Empty);
						var target = path.Replace(tempDir.FullName, string.Empty);
						logger.Info($"found {source} as {target}");
						extracted.MoveTo(path);
					}
					else if(!extracted.Name.Contains(".ico") && path==null)
					{
						logger.Warn($"discarded {extracted.FullName.Replace(tempDir.FullName, string.Empty)}");
						extracted.Delete();
						boolDiscardedFiles = true;
					}
				}
                if (validIcon && icon.Exists)
                {

                    var schedulerIcon = schedulerIconFactory();
					schedulerIcon.Icon = File.ReadAllBytes(icon.FullName);
					SaveIcon(schedulerIcon);
                }
                foreach (var sub in tempDir.GetDirectories())
				{
					sub.Delete(true);
				}
			}
			catch { /* ignore */ }
			var isXml = false;
			if (isZip == false)
			{
				try
				{
					var xml = new XmlDocument();
					xml.Load(file.FullName);
					file.CopyTo(mainConfig);
					isXml = true;
				}
				catch { /* ignore */ }
			}
			if (isXml == false && isZip == false)
			{
				logger.Error("file is neither a valid zip nor a valid xml");
				errorMessageKey = "SchedulerConfig";
				return false;
			}
			if (boolDiscardedFiles)
			{
				logger.Error("zip contained unexpected files, maybe you need to set the config type");
				errorMessageKey = "SchedulerConfig";
				return false;
			}
			if (File.Exists(mainConfig) == false)
			{
				logger.Error("file did not contain any configs");
				errorMessageKey = "SchedulerConfig";
				return false;
			}
			if (!validIcon)
			{
				logger.Error("file contains more than one icon");
				errorMessageKey = "excessIcons";
				return false;
			}

			errorMessageKey = null;
			return true;
		}
	}
}
