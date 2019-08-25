using Serilog;
using System;

namespace PM.Web
{
    internal class SerilogConfig
    {
        internal static void Initialize()
        {
            Log.Logger = new LoggerConfiguration()
                           .ReadFrom.AppSettings()
                           .CreateLogger();

            //Log.Logger = new LoggerConfiguration()
            //   .MinimumLevel.Debug()
            //   .WriteTo.File(@"C:\Users\kuskas\Source\Repos\fse-projectmanager\ProjectManager\Logs\log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit:10)
            //   .CreateLogger();

            Log.Information("logger configured");
        }
    }
}