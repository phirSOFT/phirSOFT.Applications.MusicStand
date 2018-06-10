using Unity.Builder;
using Unity.Builder.Strategy;

namespace phirSOFT.Applications.MusicStand
{
    public class BuildTrackingStrategy : BuilderStrategy
    {
        public override void PreBuildUp(IBuilderContext context)
        {
            IBuildTrackingPolicy policy = BuildTracking.GetPolicy(context)
                                          ?? BuildTracking.SetPolicy(context);

            policy.BuildKeys.Push(context.BuildKey);
        }

        public override void PostBuildUp(IBuilderContext context)
        {
            IBuildTrackingPolicy policy = BuildTracking.GetPolicy(context);
            if (policy != null && policy.BuildKeys.Count > 0) policy.BuildKeys.Pop();
        }
    }
}