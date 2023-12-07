using System;
using FileWitcher.Interfaces;

namespace FileWitcher
{
    public class Program
    {
        private const string FolderPath = "../../../../queue/";

        public static void Main()
        {
            using FileSystemWatcher watcher = new(FolderPath, "*.txt")
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName
            };

            watcher.Created += ParseFile;

            watcher.Renamed += LogEvent;
            watcher.Deleted += LogEvent;
            watcher.Changed += LogEvent;

            watcher.Error += LogError;

            watcher.EnableRaisingEvents = true;

            Console.WriteLine("File Witcher is on his watch. Hit ENTER to abort process.");
            Console.ReadLine();
        }

        public static void ParseFile(object sender, FileSystemEventArgs e)
        {
            var asnParser = new AsnParser();
            try
            {
                var boxes = asnParser.ParseAsnFile(e.FullPath);
                Console.WriteLine($"LogLine: File {e.Name} found {boxes} boxes");
            }
            catch(ErrorEventArgs er)
            {
                Console.WriteLine($"ErrorLine: File {e.Name} caused an error");
            }
        }

        public static void LogError(object sender, ErrorEventArgs e) => Console.WriteLine($"ErrorLine: {e.GetException().Message}");

        public static void LogEvent(object sender, FileSystemEventArgs e) => Console.WriteLine($"LogLine: {e.ChangeType} file {e.Name}");
    }
}