using System;
using NLog;
using Unity.Builder;
using Unity.Policy;

namespace phirSOFT.Applications.MusicStand
{
    public class LogBuildPlanPolicy : IBuildPlanPolicy
    {
        public LogBuildPlanPolicy(Type logType)
        {
            LogType = logType;
        }

        public Type LogType { get; }

        public void BuildUp(IBuilderContext context)
        {
            if (context.Existing != null) return;
            ILogger log = LogManager.GetLogger(LogType.FullName);

            context.Existing = new NLogLoggerFacade(log);
        }
    }
}