using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogSystem
{
    public static class StaticSetting
    {
        internal static string ProgramName { get; set; }

        internal static List<LogLevel> levelList = new List<LogLevel>()
        {
            LogLevel.Trace,
            LogLevel.Debug,
            LogLevel.Warning,
            LogLevel.Error,
            LogLevel.Critical,
            LogLevel.Information
        };

        public static void SetCustomPrintLogLevel(List<LogLevel> setlevelList)
        {
            levelList = setlevelList;
        }

        public static void SetNlogProperties()
        {
            ProgramName = System.Reflection.Assembly.GetCallingAssembly().GetName().Name;
            NLog.GlobalDiagnosticsContext.Set("ProgramName", ProgramName);
        }

        public static void SetNlogConfig(bool isInfoFromConfig = true)
        {
            var config = new NLog.Config.LoggingConfiguration();//NLog.LogManager.Configuration;//

            //======file==================
            // Targets where to log to: File
            var logfileTarget = new NLog.Targets.FileTarget("logfileTarget") { FileName = "C:/Log/${gdc:ProgramName:whenEmpty=Default}/${shortdate}.txt", Layout = "${longdate} ${uppercase:${level}} ${message}" };
            //============================
            #region db
            ////======db====================
            //DatabaseTarget databaseTarget = new DatabaseTarget("databaseTarget");
            //DatabaseParameterInfo param;
            //databaseTarget.ConnectionString = isInfoFromConfig ? "${configsetting:name=ConnectionStrings.LogConnection}" : Config.GetLogDBConnectionConfig();
            ////databaseTarget.DBProvider = "mssql";
            ////databaseTarget.DBHost = ".";
            ////databaseTarget.DBUserName = "nloguser";
            ////databaseTarget.DBPassword = "pass";
            ////databaseTarget.DBDatabase = "databasename";
            //databaseTarget.CommandText = "INSERT INTO [dbo].[Log] ([Id] ,[Name] ,[ProgramName] ,[KeyWord] ,[FileName] ,[LogTime] ,[CreateTime] ,[Message]) VALUES(@Id, @Name, @ProgramName, @KeyWord, @FileName, @LogTime, @CreateTime, @Message); ";

            //param = new DatabaseParameterInfo();
            //param.Name = "@Id";
            //param.Layout = "${event-properties:item=Id}";
            //databaseTarget.Parameters.Add(param);

            //param = new DatabaseParameterInfo();
            //param.Name = "@Name";
            //param.Layout = "${event-properties:item=Name}";
            //databaseTarget.Parameters.Add(param);

            //param = new DatabaseParameterInfo();
            //param.Name = "@ProgramName";
            //param.Layout = "${gdc:ProgramName:whenEmpty=Default}";
            //databaseTarget.Parameters.Add(param);

            //param = new DatabaseParameterInfo();
            //param.Name = "@KeyWord";
            //param.Layout = "${event-properties:item=KeyWord}";
            //databaseTarget.Parameters.Add(param);

            //param = new DatabaseParameterInfo();
            //param.Name = "@FileName";
            //param.Layout = "${shortdate}";
            //databaseTarget.Parameters.Add(param);

            //param = new DatabaseParameterInfo();
            //param.Name = "@LogTime";
            //param.Layout = "${event-properties:item=LogTime}";
            ////param.Layout = "${date:universalTime=true}";
            //databaseTarget.Parameters.Add(param);

            //param = new DatabaseParameterInfo();
            //param.Name = "@CreateTime";
            //param.Layout = "${event-properties:item=CreateTime}";
            ////param.Layout = "${date:universalTime=true}";
            //databaseTarget.Parameters.Add(param);

            //param = new DatabaseParameterInfo();
            //param.Name = "@Message";
            //param.Layout = "${event-properties:item=ExceptionMessage}";
            //databaseTarget.Parameters.Add(param);
            ////============================
            #endregion

            // Rules for mapping loggers to targets (file and db)
            foreach (var level in levelList)
            {
                switch (level)
                {
                    case LogLevel.Trace:
                        config.AddRuleForOneLevel(NLog.LogLevel.Trace, logfileTarget);
                        break;
                    case LogLevel.Debug:
                        config.AddRuleForOneLevel(NLog.LogLevel.Debug, logfileTarget);
                        break;
                    case LogLevel.Warning:
                        config.AddRuleForOneLevel(NLog.LogLevel.Warn, logfileTarget);
                        break;
                    case LogLevel.Error:
                        config.AddRuleForOneLevel(NLog.LogLevel.Error, logfileTarget);
                        //config.AddRuleForOneLevel(NLog.LogLevel.Error, databaseTarget);
                        break;
                    case LogLevel.Critical:
                        config.AddRuleForOneLevel(NLog.LogLevel.Fatal, logfileTarget);
                        //config.AddRuleForOneLevel(NLog.LogLevel.Fatal, databaseTarget);
                        break;
                    case LogLevel.Information:
                        config.AddRuleForOneLevel(NLog.LogLevel.Info, logfileTarget);
                        break;
                }
            }
            //config.AddRule(NLog.LogLevel.Debug, LogLevel.Fatal, logconsole);

            // Apply config           
            NLog.LogManager.Configuration = config;
        }
    }
}
