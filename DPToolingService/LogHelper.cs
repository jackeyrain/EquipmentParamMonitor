[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace DPToolingService
{
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 管道类型
    /// </summary>
    public enum PipeType : Byte
    {
        UNKNOW,
        SERVER,
        CLIENT,
    }
    public static class LogHelper
    {
        public enum LogType : Byte
        {
            Unknow = 0,
            Information,
            Warn,
            Error,
            Exception,
        }

        public static class Log
        {
            /// <summary>
            /// 信息到达
            /// </summary>
            public static event EventHandler<string> InfoEventHandler;
            /// <summary>
            /// 错误信息到达
            /// </summary>
            public static event EventHandler<string> ErrorEventHandler;
            /// <summary>
            /// 异常信息到达
            /// </summary>
            public static event EventHandler<string> ExceptionEventHandler;

            private static ILog infoLog;
            private static ILog errorLog;
            private static ILog exceptionLog;
            static Log()
            {
                FileInfo configFile = new FileInfo(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Configs", "log4net.config"));
                log4net.Config.XmlConfigurator.Configure(configFile);
            }
            /// <summary>
            /// 日志记录
            /// </summary>
            /// <param name="context">日志记录内容</param>
            /// <param name="type">日志类型</param>
            public static void LogInfo(object context, LogType type = LogType.Information, bool pushPipe = true)
            {
                switch (type)
                {
                    case LogType.Information:
                    case LogType.Warn:
                        Info(context as string);
                        break;
                    case LogType.Error:
                        Error(context as string);
                        break;
                    case LogType.Exception:
                        Exception((context as Exception) ?? new System.Exception(context as string));
                        break;
                }
            }

            private static void Info(string logContext)
            {
                if (infoLog == null)
                {
                    infoLog = log4net.LogManager.GetLogger("Info");
                }
                infoLog.Info(logContext, null);
                InfoEventHandler?.Invoke(null, logContext);
            }

            private static void Error(string errContext)
            {
                if (errorLog == null)
                {
                    errorLog = log4net.LogManager.GetLogger("Error");
                }
                errorLog.Error(errContext, null);
                ErrorEventHandler?.Invoke(null, errContext);
            }

            private static void Exception(Exception exception)
            {
                if (exceptionLog == null)
                {
                    exceptionLog = log4net.LogManager.GetLogger("Fatal");
                }
                exceptionLog.Fatal(exception.Message);
                exceptionLog.Fatal(exception.ToString());
                ExceptionEventHandler?.Invoke(null, exception.Message);
            }
        }
    }
}
