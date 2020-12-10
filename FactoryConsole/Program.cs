using LogSystem;
using LogSystem.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FactoryConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                #region nlog相關設定
                StaticSetting.SetNlogProperties();  //設定 Nlog Property (會抓Program Name)
                List<LogLevel> levels = new List<LogLevel>()
                {
                    LogLevel.Trace,
                    LogLevel.Error,
                    //LogLevel.Critical
                };
                StaticSetting.SetCustomPrintLogLevel(levels);   //要允許印哪些level的log
                StaticSetting.SetNlogConfig();  //設定 Nlog Config
                #endregion

                //setup our DI
                IServiceCollection serviceCollection = new ServiceCollection(); //NUGET:　Microsoft.Extensions.DependencyInjection
                IConfiguration config = ConfigureServices(serviceCollection);
                ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

                ILogHelper _loghelper = serviceProvider.GetService<ILogHelper>();
                
                LogModel model = new LogModel()
                {
                    Name = "測試Name12",
                    //ProgramName = "TestProgramName",
                    ExceptionMessage = "Test ExceptionMessage",
                    //FileName = "",
                    KeyWord = "hi i m key word.12",
                    ExceptionDetail = "test error detail",
                    LogLevel = LogLevel.Error
                };
                await _loghelper.DoLog(model);
                Console.WriteLine("Done");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("Hello World!");
        }

        private static IConfiguration ConfigureServices(IServiceCollection serviceCollection)
        {
            //改成把config拉出來
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location))//Directory.GetCurrentDirectory()    //NUGET: Microsoft.Extensions.Configuration.FileExtensions
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) //NUGET: Microsoft.Extensions.Configuration.Json
                .Build();
            serviceCollection.Add(new ServiceDescriptor(typeof(IConfiguration), provider => config, ServiceLifetime.Singleton));
            
            // add services
            //serviceCollection.AddTransient<ITestService, TestService>();

            //撈config的值
            //bool.TryParse(config["IsFromWebApi"], out bool isFromWebApi);
            
            serviceCollection.AddSingleton<ILogHelper, LogHelper>().AddLogging(loggingBuilder =>
            {
                // configure Logging with NLog
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                loggingBuilder.AddNLog(config);
            });

            return config;
        }
    }
}
