using System;
using System.Diagnostics;
using log4net;

namespace DBDataLibrary.Utils
{
    public sealed class ExecutionTimerLogger : IDisposable
    {
        private readonly Stopwatch _stopwatch;
        private readonly ILog _log;
        private readonly string _message;
        private readonly LogLevel _logLevel;
        private readonly bool _logOnDispose;

        private bool _disposed = false;
        private Exception? _capturedException;
        DateTime dtStart;

        public ExecutionTimerLogger(ILog log, string message, LogLevel logLevel = LogLevel.Info, bool logOnDispose = true)
        {
            _log = log;
            _message = message;
            _logLevel = logLevel;
            _logOnDispose = logOnDispose;

            dtStart = DateTime.Now;
            _stopwatch = Stopwatch.StartNew();
            
            //if(logOnDispose)
            //    Log($"{_message} - started...");
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _stopwatch.Stop();

            string duration = $"{_stopwatch.Elapsed.TotalMilliseconds:N2} ms";

            if (_capturedException != null)
            {
                Log($"{_message}: failed after {duration}", _capturedException);
            }
            else if (_logOnDispose)
            {                
                Log($"{_message}: started at [{dtStart.ToString("u")}] completed in {duration}");
            }

            _disposed = true;
        }

        public void CaptureException(Exception ex)
        {
            _capturedException = ex;
        }

        private void Log(string msg, Exception? ex = null)
        {
            switch (_logLevel)
            {
                case LogLevel.Debug:
                    if (ex != null) _log.Debug(msg, ex);
                    else _log.Debug(msg);
                    break;
                case LogLevel.Warn:
                    if (ex != null) _log.Warn(msg, ex);
                    else _log.Warn(msg);
                    break;
                case LogLevel.Error:
                    if (ex != null) _log.Error(msg, ex);
                    else _log.Error(msg);
                    break;
                default:
                    if (ex != null) _log.Info(msg, ex);
                    else _log.Info(msg);
                    break;
            }
        }
    }

    public enum LogLevel
    {
        Debug,
        Info,
        Warn,
        Error
    }
}
