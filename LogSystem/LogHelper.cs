using LogSystem.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSystem
{
    public class LogHelper : ILogHelper
    {
        private static IConfiguration _configuration;
        private readonly ILogger<LogHelper> _log;
        private List<LogLevel> levelList { get; set; }

        public LogHelper(IConfiguration configuration, ILogger<LogHelper> log)//
        {
            _log = log;
            _configuration = configuration;
            levelList = StaticSetting.levelList;
        }

        public string SayHi()
        {
            return "Hi hi";
        }

        public async Task DoLog(LogModel model)
        {
            if (levelList.Contains(model.LogLevel))
            {
                string fileloginfo_message = "\n" + "Search Key: " + model.KeyWord + "\n" + "Message: " + model.Message + "\n";
                string fileloginfo_error = "\n" + "Search Key: " + model.KeyWord + "\n" + "Exception Details: " + model.ExceptionDetail + "\n";
                switch (model.LogLevel)
                {

                    case LogLevel.Trace:
                        _log.LogTrace(fileloginfo_message);
                        break;
                    case LogLevel.Debug:
                        _log.LogDebug(fileloginfo_message);
                        break;
                    case LogLevel.Warning:
                        _log.LogWarning(fileloginfo_error);
                        break;
                    case LogLevel.Error:
                    case LogLevel.Critical:
                        _log.LogError(fileloginfo_error);
                        break;
                    case LogLevel.Information:
                    default:
                        _log.LogInformation(fileloginfo_message);
                        break;
                }
            }
            else
            {
                //do not support this type
            }





        }



    }
}
