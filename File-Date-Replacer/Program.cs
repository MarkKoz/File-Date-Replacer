using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;

namespace File_Date_Replacer
{
	public class Program
	{
		[SuppressMessage("ReSharper", "PossibleNullReferenceException")]
		[SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
		public static void WriteDate(string p)
		{
			string nameExt = Path.GetFileName(p);
			string name = Path.GetFileNameWithoutExtension(p);
			string dateStr = name.Substring(0, 8);
			const string dateFormat = "yyyyMMdd";
			DateTime dateParsed;

			if (!File.Exists(p))
			{
				Console.WriteLine("[ERROR] The specified file " +
				                  $"'{nameExt}' was not found.");
				return;
			}

			if (!DateTime.TryParseExact(dateStr, dateFormat, null,
				DateTimeStyles.None, out dateParsed))
			{
				Console.WriteLine("[ERROR] Unable to parse date " +
				                  $"'{dateStr}' for file '{nameExt}'.");
				return;
			}

			File.SetLastWriteTime(p, dateParsed);
			File.SetLastAccessTime(p, dateParsed);
			/*Console.WriteLine("[INFO] Set field 'Modified' of file " +
									$"'{Path.GetFileName(p)}' from " +
									$"{File.GetLastWriteTime(p)}" +
									$" to {dateParsed}");*/

			string nameNew = nameExt.Substring(9);
			string pathNew = Path.Combine(Path.GetDirectoryName(p), nameNew);
			File.Move(p, pathNew);
			Console.WriteLine("[INFO] Successfully changed dates of " +
			                  $"file '{nameExt}' to '{dateParsed}'.");
		}

		public static void Main(string[] args)
		{
			string dir = Directory.GetCurrentDirectory();

			if (args.Length <= 0)
			{
				Console.WriteLine("[ERROR] No path was specified.");
				Console.ReadLine();
			}
			else
			{
				/*try
				{
					if (File.GetAttributes(args[0]).HasFlag(FileAttributes.Directory))
					{

					}
				}
				catch (FileNotFoundException)
				{
					Console.WriteLine("[ERROR] The specified file " +
					                  "was not found.");
				}
				catch (DirectoryNotFoundException)
				{
					Console.WriteLine("[ERROR] The specified directory " +
					                  "was not found.");
				}*/

				if (args[0].Contains("*"))
				{
					var files = Directory.GetFiles(dir, args[0]);
					Console.WriteLine($"[INFO] {files.Length} " +
									  $"files matching criteria '{args[0]}' " +
					                  $"were found in '{dir}'.\n");

					foreach (string f in files)
					{
						WriteDate(f);
					}

					Console.WriteLine("\n[INFO] Replacing complete.");
					Console.ReadLine();
				}
				else
				{
					string path = Path.Combine(dir, args[0]);
					Console.WriteLine($"[INFO] The path is: {path}");

					WriteDate(path);

					Console.WriteLine("\n[INFO] Replacing complete.");
					Console.ReadLine();
				}
			}
		}
	}
}
