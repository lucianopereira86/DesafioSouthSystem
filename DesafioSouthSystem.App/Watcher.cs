using DesafioSouthSystem.Domain.Commands;
using MediatR;
using System;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioSouthSystem.App
{
    public class Watcher
    {
        private static string _outputPath;
        private readonly IMediator _mediator;
        public Watcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void Run(string inputPath, string outputPath)
        {
            // Check directories exist
            if (!Directory.Exists(inputPath))
            {
                // Display the proper way to call the program.
                Console.WriteLine($"Input path not found: {inputPath}");
                return;
            }
            if (!Directory.Exists(outputPath))
            {
                // Display the proper way to call the program.
                Console.WriteLine($"Output path not found: {outputPath}");
                return;
            }

            _outputPath = outputPath;

            // Create a new FileSystemWatcher and set its properties.
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = inputPath;

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                watcher.NotifyFilter = NotifyFilters.LastAccess;

                // Only watch text files.
                watcher.Filter = "*.txt";

                // Add event handlers.
                watcher.Changed += OnChanged;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                while (Console.Read() != 'q') ;
            }
        }

        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Task.Run(() =>
            {
                if (e.ChangeType == WatcherChangeTypes.Changed)
                {
                    Console.WriteLine($"Processing file '{e.Name}'...");

                    var outputPath = string.Format("{0}\\{1}", _outputPath, e.Name);
                    var command = new FileReadingCommand(e.Name, e.FullPath, outputPath);

                    var commandResult = _mediator.Send(command).Result;
                    if (commandResult.Valid)
                    {
                        Console.WriteLine($"File '{e.Name}' processed successfully");
                        Console.WriteLine("OUTPUT:");
                        string[] lines = (string[])commandResult.Data;
                        lines.ToList().ForEach(e => Console.WriteLine(e));
                    }
                    else
                    {
                        Console.WriteLine("ERROR LIST");
                        commandResult.Errors.ToList().ForEach(e => Console.WriteLine(e.Message));
                    }
                }
            });
        }
    }
}