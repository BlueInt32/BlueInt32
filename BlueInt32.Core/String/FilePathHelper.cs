using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BlueInt32.Core.String
{
	public class FilePathHelper
	{
		#region Properties

		public string InputPath { get; set; }
		public DirectoryInfo DirectoryInfo { get; set; }

		public string Name { get; set; }
		public string FileFull { get; set; }
		public string FileRelative { get; set; }

		public string DirectoryFull { get; set; }
		public string DirectoryRelative { get; set; }
		public string DirectoryStart { get; set; }

		public string AutoRelative { get; set; }

		public string ParentDirectoryFull {get;set;}
		public string ParentDirectoryRelative { get; set; }

		public enum FilePathElementType { File, Directory, Undertermined}
		FilePathElementType ElementType { get; set; }

		#endregion


		#region Constructors

		public FilePathHelper(string rawFullPath)
		{
			ElementType = GetElementType(rawFullPath);
			ProcessAbsolutePaths(rawFullPath);
			DirectoryInfo = new DirectoryInfo(Slashify(rawFullPath));
		}

		public FilePathHelper(string rawFullPath, string startPath)
		{
			if (string.IsNullOrWhiteSpace(rawFullPath))
				throw new InvalidDataException("The rawFullPath must not be empty.");
			if (!Slashify(rawFullPath).Contains(Slashify(startPath)))
				throw new InvalidOperationException("The startPath must be a prefix of the rawFullPath.");
			ElementType = GetElementType(rawFullPath);
			ProcessAbsolutePaths(rawFullPath);
			ProcessRelativePath(startPath);
			DirectoryInfo = new DirectoryInfo(Slashify(rawFullPath));
		}

		#endregion

		public void ProcessAbsolutePaths(string inputPath)
		{
			switch (ElementType)
			{
				case FilePathElementType.File:
					FileInfo fi = new FileInfo(inputPath);
					Name = fi.Name;
					FileFull = fi.FullName.Replace("\\", "/");
					DirectoryFull = FileFull.Replace(Name, "");
				break;
				case FilePathElementType.Directory:
					DirectoryInfo di = new DirectoryInfo(inputPath);
					Name = di.Name;
					DirectoryFull = di.FullName.Replace("\\", "/");
					if (!DirectoryFull.EndsWith("/"))
						DirectoryFull = string.Concat(DirectoryFull, "/");
					ParentDirectoryFull = DirectoryFull.TrimEnd('/').Replace(Name, "");
				break;
				default:
				break;
			}
		}

		public void ProcessRelativePath(string startPath)
		{
			DirectoryInfo di = new DirectoryInfo(startPath);
			DirectoryStart = SlashifyDirectoryPath(di.FullName);
			DirectoryRelative = DirectoryFull.Replace(di.FullName.Replace("\\", "/"), "").TrimStart('/');

			switch (ElementType)
			{
				case FilePathElementType.File:
					FileRelative = FileFull.Replace(di.FullName.Replace("\\", "/"), "").TrimStart('/');
					AutoRelative = FileRelative;
				break;

				case FilePathElementType.Directory:
					if (!DirectoryRelative.EndsWith("/")) DirectoryRelative = string.Concat(DirectoryRelative, "/");
					AutoRelative = DirectoryRelative;
					ParentDirectoryRelative = DirectoryRelative.TrimEnd('/').Replace(Name, "");
				break;
				default:
				break;
			}

		}

		public static string SlashifyDirectoryPath(string directoryPath)
		{
			DirectoryInfo di = new DirectoryInfo(directoryPath);
			string slashedStartFull = di.FullName.Replace("\\", "/");
			return slashedStartFull.EndsWith("/") ? slashedStartFull : string.Concat(slashedStartFull, "/");
		}

		public static string SlashifyFilePath(string filePath)
		{
			FileInfo di = new FileInfo(filePath);
			string slashedStartFull = di.FullName.Replace("\\", "/");
			return slashedStartFull;
		}

		public static string Slashify(string path)
		{
			switch (GetElementType(path))
			{
				case FilePathElementType.File:
				return SlashifyFilePath(path);
				break;
				case FilePathElementType.Directory:
				return SlashifyDirectoryPath(path);
				break;
				case FilePathElementType.Undertermined:
				default:
				return path.Replace("\\", "/");
				break;
			}
		}

		public static FilePathElementType GetElementType(string inputPath)
		{
			try
			{
				FileAttributes attr = File.GetAttributes(inputPath);
				if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
					return FilePathElementType.Directory;
				else
					return FilePathElementType.File;
			}
			catch
			{
				return FilePathElementType.Undertermined;
			}
		}
	}
}
