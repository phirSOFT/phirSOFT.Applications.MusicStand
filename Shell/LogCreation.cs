using NLog;
using Prism.Logging;
using Unity.Builder;
using Unity.Extension;
using Unity.Policy;

namespace phirSOFT.Applications.MusicStand
{
    public class LogCreation : UnityContainerExtension, IBuildPlanPolicy
    {
        protected override void Initialize()
        {
            Context.Policies.Set(typeof(ILoggerFacade), null, typeof(IBuildPlanPolicy), this);
        }


        public void BuildUp(IBuilderContext context)
        {
            if (context.Existing != null) return;
            ILogger log = LogManager.GetLogger(context.ParentContext.BuildKey.Type.FullName);

            context.Existing = new NLogLoggerFacade(log);
        }
    }
}