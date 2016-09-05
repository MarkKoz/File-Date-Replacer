using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace File_Date_Replacer
{
	public class Program
	{
		public static void WriteDate(string p)
		{
			if (!File.Exists(p))
			{
				Console.WriteLine("The specified file " +
				                  $"'{Path.GetFileName(p)}' was not found.");
			}
			else
			{
				DateTime dateAcc = File.GetLastAccessTime(p);
				File.SetLastWriteTime(p, dateAcc);
				Console.WriteLine("Set field 'Modified' of file " +
									  $"'{Path.GetFileName(p)}' from " +
									  $"{File.GetLastWriteTime(p)}" +
									  $" to {dateAcc}");
			}
		}

		public static void Main(string[] args)
		{
			string dir = Directory.GetCurrentDirectory();

			if (args.Length <= 0)
			{
				Console.WriteLine("No path was specified.");
				Console.ReadLine();
			}
			else
			{
				if (args[0].Contains("*"))
				{
					var files = Directory.EnumerateFiles(dir, args[0]);
					Console.WriteLine($"{files.Count()} " +
					              $"files were found in {dir}\n");

					foreach (string f in files)
					{
						WriteDate(f);
					}

					Console.WriteLine("\nReplacing complete.");
					Console.ReadLine();
				}
				else
				{
					string path = Path.Combine(dir, args[0]);
					Console.WriteLine($"The path is: {path}");

					WriteDate(path);

					Console.WriteLine("\nReplacing complete.");
					Console.ReadLine();
				}
			}
		}
	}
}
