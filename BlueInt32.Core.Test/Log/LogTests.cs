using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using BlueInt32.Core.Logs;

namespace BlueInt32.Core.Test
{
	[TestClass]
	public class LogTests
	{
		public const string LogPath =@"..\..\Logs\testLogs.log";

		[TestInitialize]
		public void Init()
		{
			Log.Init();
		}

		[TestCleanup]
		public void CleanUp()
		{
			if (File.Exists(LogPath))
				File.Delete(LogPath);
		}

		[TestMethod]
		public void TestLogInfo()
		{
			Log.Info("TestLog", "~ Test écriture de logs");

			string logFileContent = File.ReadAllText(LogPath);

			Assert.IsTrue(File.Exists(LogPath));
			Assert.IsTrue(logFileContent.Contains(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
			Assert.IsTrue(logFileContent.Contains(" INFO  TestLog - ~ Test écriture de logs"));
		}

		[TestMethod]
		public void TestLogWarning()
		{
			Log.Warn("TestLog", "~ Test écriture de logs");

			string logFileContent = File.ReadAllText(LogPath);

			Assert.IsTrue(File.Exists(LogPath));
			Assert.IsTrue(logFileContent.Contains(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
			Assert.IsTrue(logFileContent.Contains(" WARN  TestLog - ~ Test écriture de logs"));

		}
		[TestMethod]
		public void TestLogError()
		{
			Log.Error("TestLog", "~ Test écriture de logs");

			string logFileContent = File.ReadAllText(LogPath);

			Assert.IsTrue(File.Exists(LogPath));
			Assert.IsTrue(logFileContent.Contains(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
			Assert.IsTrue(logFileContent.Contains(" ERROR TestLog - ~ Test écriture de logs"));
		}
		[TestMethod]
		public void TestLogInfoFormat()
		{
			string variableText = ": check !";
			Log.InfoFormat("TestLog Format", "~ Test écriture de logs {0}", variableText);

			string logFileContent = File.ReadAllText(LogPath);

			Assert.IsTrue(File.Exists(LogPath));
			Assert.IsTrue(logFileContent.Contains(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
			Assert.IsTrue(logFileContent.Contains(" INFO  TestLog Format - ~ Test écriture de logs : check !"));
		}
	}
}
