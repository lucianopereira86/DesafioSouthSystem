using System;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DesafioSouthSystem.Shared.Models;
using Microsoft.Extensions.Options;

namespace DesafioSouthSystem.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press 'Q' to quit the program");
            StartWatcher();
        }

        private static void StartWatcher()
        {
            var serviceProvider = DependencyInjection();

            IOptions<AppSettings> optionAppSettings = serviceProvider.GetService<IOptions<AppSettings>>();
            var mediatR = serviceProvider.GetService<IMediator>();
            var homeDrive = optionAppSettings.Value.IsHomePathComplete ? "" : Environment.GetEnvironmentVariable("HOMEDRIVE");
            var homePath = Environment.GetEnvironmentVariable("HOMEPATH");

            var dataPath = string.Format("{0}{1}\\data\\", homeDrive, homePath);
            string inputPath = $"{dataPath}in";
            string outputPath = $"{dataPath}out";


            Console.WriteLine("Application started");
            new Watcher(mediatR).Run(inputPath, outputPath);
        }

        private static IServiceProvider DependencyInjection()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", false, true)
                    .Build();

            IServiceProvider serviceProvider =
                new ServiceCollection()
                .Configure<AppSettings>(configuration)
                .AddMediatR(
                    AppDomain.CurrentDomain.Load("DesafioSouthSystem.WebAPI"),
                    AppDomain.CurrentDomain.Load("DesafioSouthSystem.Domain")
                )
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
