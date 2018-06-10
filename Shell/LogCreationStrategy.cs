using System;
using Prism.Logging;
using Unity.Builder;
using Unity.Builder.Strategy;
using Unity.Policy;

namespace phirSOFT.Applications.MusicStand
{
    public class LogCreationStrategy : BuilderStrategy
    {

        public bool IsPolicySet { get; private set; }

        public override void PreBuildUp(IBuilderContext context)
        {
            Type typeToBuild = context.BuildKey.Type;
            if (typeof(ILoggerFacade) != typeToBuild) return;

            if (context.Policies.Get<IBuildPlanPolicy>(context.BuildKey) != null) return;

            Type typeForLog = GetLogType(context);
            IBuildPlanPolicy policy = new LogBuildPlanPolicy(typeForLog);
            context.Policies.Set(policy, context.BuildKey);

            IsPolicySet = true;
        }

        public override void PostBuildUp(IBuilderContext context)
        {
            if (IsPolicySet)
            {
                context.Policies.Clear<IBuildPlanPolicy>(context.BuildKey);
                IsPolicySet = false;
            }
        }

        private static Type GetLogType(IBuilderContext context)
        {
            Type logType = null;
            IBuildTrackingPolicy buildTrackingPolicy = BuildTracking.GetPolicy(context);
            if (buildTrackingPolicy == null || buildTrackingPolicy.BuildKeys.Count < 2) return logType;
            object top = buildTrackingPolicy.BuildKeys.Pop();
            logType = ((NamedTypeBuildKey)buildTrackingPolicy.BuildKeys.Peek()).Type;
            buildTrackingPolicy.BuildKeys.Push(top);
            return logType;
        }
    }
}