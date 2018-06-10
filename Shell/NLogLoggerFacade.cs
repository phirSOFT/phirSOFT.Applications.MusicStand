using System;
using NLog;
using Prism.Logging;

namespace phirSOFT.Applications.MusicStand
{
    public class NLogLoggerFacade : ILoggerFacade
    {
        private readonly ILogger _logger;

        public NLogLoggerFacade(ILogger logger)
        {
            _logger = logger;
        }


        public void Log(string message, Category category, Priority priority)
        {
            LogLevel level;

            switch (category)
            {
                case Category.Debug:
                    level = priority == Priority.Low ? LogLevel.Trace : LogLevel.Debug;
                    break;
                case Category.Exception:
                    level = priority == Priority.High ? LogLevel.Fatal : LogLevel.Error;
                    break;
                case Category.Info:
                    level = LogLevel.Info;
                    break;
                case Category.Warn:
                    level = LogLevel.Warn;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(category), category, message: null);
            }

            _logger.Log(level, message);
        }
    }
}