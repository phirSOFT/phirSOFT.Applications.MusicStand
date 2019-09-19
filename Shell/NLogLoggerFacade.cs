using System;
using Common.Logging;
using NLog;
using Prism.Logging;
using LogLevel = Common.Logging.LogLevel;

namespace phirSOFT.Applications.MusicStand
{
    public class NLogLoggerFacade : ILoggerFacade
    {
        private readonly ILog _logger;

        public NLogLoggerFacade(ILog logger)
        {
            _logger = logger;
        }


        public void Log(string message, Category category, Priority priority)
        {
            Action<object> logCallback;

            switch (category)
            {
                case Category.Debug:
                    logCallback = priority == Priority.Low ? (Action<object>) _logger.Trace : _logger.Debug;
                    break;
                case Category.Exception:
                    logCallback = priority == Priority.High ? (Action<object>)_logger.Fatal : _logger.Error;
                    break;
                case Category.Info:
                    logCallback = _logger.Info;
                    break;
                case Category.Warn:
                    logCallback = _logger.Info;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(category), category, message: null);
            }

            logCallback.Invoke(message);
        }
    }
}