using System;
using System.Security.Permissions;
using System.IO;
using System.Threading;

namespace RepDLL
{
    public class Wattcher
    {
        private string watPath = "";
        public Wattcher(string watPath)
        {
            this.watPath = watPath;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Runner(string put)
        {

            // Create a new FileSystemWatcher and set its properties.
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = put;

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                watcher.NotifyFilter = NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                // Only watch text files.
                watcher.Filter = "*.*";
                watcher.IncludeSubdirectories = true;
                //IncludeSubdirectories
                // Add event handlers.
                //watcher.Changed += OnChanged;
                watcher.Created += OnCreated;
                watcher.Deleted += OnDelete;
                watcher.Renamed += OnRenamed;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                //Console.WriteLine("Press 'q' to quit the sample.");
                //while (Console.Read() != 'q') ;
                //while (true)
                //{

                //}
            }
        }
        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            _RepLog._monitor(String.Format("File created: {0} in folder {1}", e.Name, e.FullPath));
            //Console.WriteLine("File created: {0}", e.Name);
        }


        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            _RepLog._monitor(String.Format("File: {0} {1}", e.FullPath, e.ChangeType));
            //Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");
        }


        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            _RepLog._monitor(String.Format("File: {0} renamed to {1}", e.OldFullPath, e.FullPath));
            //Console.WriteLine($"File: {e.OldFullPath} renamed to {e.FullPath}");
        }


        private static void OnDelete(object sender, FileSystemEventArgs e)
        {
            _RepLog._monitor(String.Format("File deleted: {0} in folder {1}", e.Name, e.FullPath));
            //Console.WriteLine("File deleted: {0}", e.Name);
        }

    }
}
