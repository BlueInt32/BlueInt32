using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using log4net;
using System.Text;

namespace BlueInt32.Core.Logs
{
	public static class Log
	{
		public static LogLevel ConfLevel { get { return LogConfigurationSection.Values.LogLevelEnum; } }
		public static List<LogMethod> LogMethods { get { return LogConfigurationSection.Values.LogMethodsList; } }

		public static void Init()
		{
			if (!LogMethods.Contains(LogMethod.Log4Net))
				return;
			var configResult = log4net.Config.XmlConfigurator.Configure();
			if (configResult.Count > 0)
			{
				StringBuilder sb = new StringBuilder();
				foreach (var item in configResult)
				{
					sb.AppendLine(item.ToString());
				}
				throw new Exception(sb.ToString());
			}
		}

		public static void Debug(string source, string text)
		{
			TryLog(LogLevel.Debug, source, text);
		}

		public static void Info(string source, string text)
		{
			TryLog(LogLevel.Info, source, text);
		}

		public static void Warn(string source, string text)
		{
			TryLog(LogLevel.Warn, source, text);
		}

		public static void Error(string source, string text)
		{
			TryLog(LogLevel.Error, source, text);
		}

		public static void Fatal(string source, string text)
		{
			TryLog(LogLevel.Fatal, source, text);
		}

		public static void DebugFormat(string source, string formatText, params Object[] args)
		{
			TryLog(LogLevel.Debug, source, string.Format(formatText, args));
		}

		public static void InfoFormat(string source, string formatText, params Object[] args)
		{
			TryLog(LogLevel.Info, source, string.Format(formatText, args));
		}

		public static void WarnFormat(string source, string formatText, params Object[] args)
		{
			TryLog(LogLevel.Warn, source, string.Format(formatText, args));
		}

		public static void ErrorFormat(string source, string formatText, params Object[] args)
		{
			TryLog(LogLevel.Error, source, string.Format(formatText, args));
		}

		public static void FatalFormat(string source, string formatText, params Object[] args)
		{
			TryLog(LogLevel.Fatal, source, string.Format(formatText, args));
		}

		#region Privates
		
		private static void TryLog(LogLevel level, string source, string text)
		{
			if(IsLevelHigherThanConfLevel(level))
				EffectiveLog(level, source, text);
		}

		private static bool IsLevelHigherThanConfLevel(LogLevel inputLogLevel)
		{
			return inputLogLevel >= ConfLevel;
		}

		private static void EffectiveLog(LogLevel level, string source, string text)
		{
			string processedText = text.Replace(Environment.NewLine, " ");
			foreach (LogMethod logMethod in LogMethods)
			{
				switch (logMethod)
				{
					case LogMethod.DebugWriteLine:
					System.Diagnostics.Debug.WriteLine(string.Format(" -- {0} -- {1}", DateTime.UtcNow, processedText));
					break;
					case LogMethod.FileAppend:
					File.AppendAllText(ConfigurationManager.AppSettings["logFilePath"], string.Format("{0} - {1}\n\n", DateTime.Now, processedText)); 
					return;
					break;
					case LogMethod.EventViewer:
					try
					{
						string sSource = source;
						string sLog = "Application";
						string sEvent = string.Format("Log Event: {0} - {1}", source, processedText);
						if (!EventLog.SourceExists(sSource))
							EventLog.CreateEventSource(sSource, sLog);
						EventLog.WriteEntry(sSource, sEvent);
					}
					catch
					{
					}
					break;
					case LogMethod.Log4Net:
					ILog log = LogManager.GetLogger(source);
					switch (level)
					{
						case LogLevel.Debug:
						log.Debug(processedText);
						break;
						case LogLevel.Info:
						log.Info(processedText);
						break;
						case LogLevel.Warn:
						log.Warn(processedText);
						break;
						case LogLevel.Error:
						log.Error(processedText);
						break;
						case LogLevel.Fatal:
						log.Fatal(processedText);
						break;
						default:
						throw new ArgumentOutOfRangeException();
					}
					break;
					default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}
		#endregion
	}
}
