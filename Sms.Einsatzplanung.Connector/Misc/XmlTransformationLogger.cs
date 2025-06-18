using System;

namespace Sms.Einsatzplanung.Connector.Misc
{
	using log4net;

	using Microsoft.Web.XmlTransform;
	public class XmlTransformationLogger : IXmlTransformationLogger
	{
		private readonly ILog logger;
		public XmlTransformationLogger(ILog logger)
		{
			this.logger = logger;
		}
		public void LogMessage(string message, params object[] messageArgs) => logger.InfoFormat(message, messageArgs);
		public void LogMessage(MessageType type, string message, params object[] messageArgs)
		{
			if (type == MessageType.Verbose)
			{
				logger.DebugFormat(message, messageArgs);
			}
			else
			{
				logger.InfoFormat(message, messageArgs);
			}
		}
		public void LogWarning(string message, params object[] messageArgs) => logger.WarnFormat(message, messageArgs);
		public void LogWarning(string file, string message, params object[] messageArgs) => LogWarning($"{file} - {message}", messageArgs);
		public void LogWarning(string file, int lineNumber, int linePosition, string message, params object[] messageArgs) => LogWarning($"{file} [{lineNumber}:{linePosition}] - {message}", messageArgs);
		public void LogError(string message, params object[] messageArgs) => logger.ErrorFormat(message, messageArgs);
		public void LogError(string file, string message, params object[] messageArgs) => LogError($"{file} - {message}", messageArgs);
		public void LogError(string file, int lineNumber, int linePosition, string message, params object[] messageArgs) => LogError($"{file} [{lineNumber}:{linePosition}] - {message}", messageArgs);
		public void LogErrorFromException(Exception ex) => logger.Error(ex);
		public void LogErrorFromException(Exception ex, string file) => logger.Error(file, ex);
		public void LogErrorFromException(Exception ex, string file, int lineNumber, int linePosition) => LogErrorFromException(ex, $"{file} [{lineNumber}:{linePosition}]");
		public void StartSection(string message, params object[] messageArgs) => LogMessage(message, messageArgs);
		public void StartSection(MessageType type, string message, params object[] messageArgs) => LogMessage(type, message, messageArgs);
		public void EndSection(string message, params object[] messageArgs) => LogMessage(message, messageArgs);
		public void EndSection(MessageType type, string message, params object[] messageArgs) => LogMessage(type, message, messageArgs);
	}
}