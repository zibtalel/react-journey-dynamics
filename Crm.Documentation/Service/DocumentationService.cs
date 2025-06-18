namespace Crm.Documentation.Service
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using System.Text.RegularExpressions;

	using Model;
	using ViewModel;

	using HtmlAgilityPack;

	using log4net;

	using MarkdownDeep;

	public class DocumentationService
	{
		private readonly DocumentationMode mode;
		private readonly string imageFolder;
		private readonly CultureInfo culture;
		private static readonly ILog Logger = LogManager.GetLogger(typeof(DocumentationService));
		protected List<Section> TableOfContents = new List<Section>();
		protected List<string> Authors = new List<string>();

		protected string Title;
		protected string TocCaption;
		protected string DocumentType;
		protected string Version;

		protected static readonly Regex FileRef = new Regex(@"
												(								# wrap whole match in $1
												{%
													[ ]?					# one optional space
													include[ ]		# the term 'include' followed by space
													(['""])				# quote char = $2
														(.*?)				# file ref = $3
													\2						# matching quote
													[ ]?					# one optional space
												%}

												(?:\n[ ]*)?			# one optional newline followed by spaces
												)", RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled);

		public ICollection<string> ReadToc(string tocFiles, string tocPath)
		{
			// Reads the toc file containing the chapter list
			var lines = new List<string>();

			if (!string.IsNullOrWhiteSpace(tocFiles))
			{
				lines = new List<string>(tocFiles.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
			}
			else
			{
				try
				{
					var content = File.ReadAllText(tocPath);
					lines = new List<string>(content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
					lines = lines.Where(x => !x.StartsWith("#")).ToList();
				}
				catch (Exception)
				{
					Logger.ErrorFormat("Can't read Toc File {0}", tocPath);
					throw;
				}
			}

			return lines;
		}
		public IEnumerable<string> GenerateFileList(IEnumerable<string> toc, string inputExtension = ".md")
		{
			// Uses the toc to generate chapter relative paths to the input files
			return toc.Select(entry =>
			{
				var path = entry.EndsWith(inputExtension) ? string.Empty : entry;
				var filename = entry.EndsWith(inputExtension) ? entry : $"{entry}{inputExtension}";
				var tokens = entry.Split('/', '\\');
				if (tokens.Length > 1 && tokens.Any(x => x.Contains(inputExtension)))
				{
					path = Path.Combine(tokens.Where(x => !x.Contains(inputExtension)).ToArray());
					filename = tokens.Last(x => x.Contains(inputExtension));
				}
				else if (tokens.Length > 1)
				{
					path = Path.Combine(tokens);
					filename = tokens.First() + inputExtension;
				}

				return Path.Combine(path, filename);
			}).ToList();
		}
		private string SanitizeImages(string convertedHtml, string rootDirectory)
		{
			var writer = new StringWriter();
			var doc = new HtmlDocument();
			doc.LoadHtml(convertedHtml);

			foreach (HtmlNode img in doc.DocumentNode.Descendants("img").Where(x => !x.GetAttributeValue("src", String.Empty).Equals(String.Empty)))
			{
				var tokens = img
					.Attributes["src"].Value.Split('/', '\\')
					.Where(x => !String.Equals(imageFolder, x, StringComparison.InvariantCultureIgnoreCase))
					.ToArray();

				var newTokens = new List<string>();
				var servingDirectoryTokens = rootDirectory.Split('\\');
				if (mode == DocumentationMode.ServeIntegrated)
				{
					newTokens.Add("..");
					newTokens.Add("..");
					newTokens.Add("Plugins");
					newTokens.AddRange(servingDirectoryTokens);
					newTokens.Add(imageFolder);
					newTokens.AddRange(tokens);

					var src = String.Join("/", newTokens.Where(x => !string.IsNullOrWhiteSpace(x)));
					img.Attributes["src"].Value = src;
				}
				else if (mode == DocumentationMode.ServeStandalone)
				{
					newTokens.AddRange(servingDirectoryTokens);
					newTokens.Add(imageFolder);
					newTokens.AddRange(tokens);

					img.Attributes["src"].Value = String.Join("/", newTokens); // Serving in standalone context
				}
				else
				{
					newTokens.Add(imageFolder);
					newTokens.AddRange(servingDirectoryTokens);
					newTokens.AddRange(tokens);

					img.Attributes["src"].Value = String.Join("/", newTokens); // Generating output document
				}
			}
			doc.Save(writer);

			return writer.ToString();
		}
		private string Preprocess(string file)
		{
			var sb = new StringBuilder();
			PreprocessFileContent(file, sb);

			return GetMarkdown().Transform(sb.ToString());
		}
		private void PreprocessFileContent(string file, StringBuilder sb)
		{
			using (var reader = new StreamReader(file))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					if (line.StartsWith("%title:", StringComparison.InvariantCultureIgnoreCase))
					{
						Title = string.IsNullOrWhiteSpace(Title) ? line.Substring("%title:".Length) : Title;
					}
					else if (line.StartsWith("%toccaption:", StringComparison.InvariantCultureIgnoreCase))
					{
						TocCaption = string.IsNullOrWhiteSpace(TocCaption) ? line.Substring("%toccaption:".Length) : TocCaption;
					}
					else if (line.StartsWith("%documenttype:", StringComparison.InvariantCultureIgnoreCase))
					{
						DocumentType = string.IsNullOrWhiteSpace(DocumentType) ? line.Substring("%documenttype:".Length) : DocumentType;
					}
					else if (line.StartsWith("%version:", StringComparison.InvariantCultureIgnoreCase) && line.Contains("%auto%"))
					{
						var assembly = Assembly.GetAssembly(typeof(DocumentationService));
						var version = assembly.GetName().Version.ToString();
						var dateTimeString = assembly.Location != null ? $"({File.GetLastWriteTime(assembly.Location).ToString("d", culture)})" : string.Empty;

						Version = string.IsNullOrWhiteSpace(Version) ? $"V{version} {dateTimeString}" : Version;
					}
					else if (line.StartsWith("%version:", StringComparison.InvariantCultureIgnoreCase))
					{
						Version = string.IsNullOrWhiteSpace(Version) ? line.Substring("%version:".Length) : Version;
					}
					else if (line.StartsWith("%author:", StringComparison.InvariantCultureIgnoreCase))
					{
						var author = line.Substring("%author:".Length);
						if (!Authors.Contains(author))
						{
							Authors.Add(author);
						}
					}
					else if (line.StartsWith("#"))
					{
						sb.AppendLine(ParseSection(TableOfContents, line));
					}
					else if (FileRef.IsMatch(line))
					{
						var match = FileRef.Match(line);
						var includeFile = Path.Combine(new FileInfo(file).DirectoryName, match.Groups[3].Value);
						if (File.Exists(includeFile))
						{
							PreprocessFileContent(includeFile, sb);
						}
					}
					else
					{
						sb.AppendLine(line);
					}
				}
			}
		}
		private string ParseSection(List<Section> sections, string line)
		{
			var section = new Section();
			if (line.StartsWith("###"))
			{
				sections[sections.Count - 1].SubSections[sections[sections.Count - 1].SubSections.Count - 1].SubSections.Add(section);
			}
			else if (line.StartsWith("##"))
			{
				sections[sections.Count - 1].SubSections.Add(section);
			}
			else
			{
				sections.Add(section);
			}

			string chapterName;
			string name;

			if (line.Contains(" {#"))
			{
				chapterName = line.Substring(0, line.IndexOf(" {#", StringComparison.InvariantCultureIgnoreCase)).Replace("#", String.Empty).Trim();
				name = line.Substring(line.IndexOf(" {#", StringComparison.InvariantCultureIgnoreCase) + 3, line.LastIndexOf("}", StringComparison.InvariantCultureIgnoreCase) - (line.IndexOf(" {#", StringComparison.InvariantCultureIgnoreCase) + 3)).Trim();
			}
			else
			{
				chapterName = line.Replace("#", "").Trim();
				name = Slugger.Slugify(chapterName);
			}

			section.Title = chapterName;
			section.Name = name;
			return line;
		}
		private Markdown GetMarkdown()
		{
			return new Markdown { AutoHeadingIDs = true, ExtraMode = true, HtmlClassTitledImages = "figure" };
		}
		public DocumentationModel GenerateDocumentationModel(string servingDirectory, IEnumerable<string> files)
		{
			TableOfContents = new List<Section>();
			Authors = new List<string>();

			var rootedFiles = files.Select(x => Path.Combine(servingDirectory, x));

			var contentBuilder = new StringBuilder();
			foreach (string file in rootedFiles.Where(File.Exists))
			{
				var processedMarkdown = Preprocess(file);
				var markdownFile = new FileInfo(file);
				var directory = new DirectoryInfo(servingDirectory);
				var rootDirectory = markdownFile.DirectoryName.Replace(directory.FullName, String.Empty);

				processedMarkdown = SanitizeImages(processedMarkdown, rootDirectory);
				contentBuilder.Append(processedMarkdown);
			}

			return new DocumentationModel
			{
				Title = Title ?? (contentBuilder.Length > 0 ? "%title: missing in markdown" : "No content"),
				TocCaption = TocCaption ?? (contentBuilder.Length > 0 ? "%toccaption: missing in markdown" : "No content"),
				DocumentType = DocumentType ?? (contentBuilder.Length > 0 ? "%documenttype: missing in markdown" : "No content"),
				Authors = Authors,
				Version = Version,
				Body = contentBuilder.Length > 0
					? contentBuilder.ToString().Replace("[title]", Title).Replace("[documenttype]", DocumentType).Replace("[author]", String.Join(", ", Authors))
					: GetMarkdown().Transform("# No content\nWe are sorry, but there is no content where you are looking for it.")
			};
		}

		public DocumentationService(DocumentationMode mode, string imageFolder, CultureInfo culture)
		{
			this.mode = mode;
			this.imageFolder = imageFolder;
			this.culture = culture;
		}
	}
}