using Unity.Builder;
using Unity.Extension;
using Unity.Policy;

namespace phirSOFT.Applications.MusicStand
{
    public class BuildTracking : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Context.Strategies.Add(new BuildTrackingStrategy(), UnityBuildStage.TypeMapping);
        }

        public static IBuildTrackingPolicy GetPolicy(IBuilderContext context)
        {
            return context.Policies.Get<IBuildTrackingPolicy>(context.BuildKey);
        }

        public static IBuildTrackingPolicy SetPolicy(IBuilderContext context)
        {
            IBuildTrackingPolicy policy = new BuildTrackingPolicy();
            context.Policies.SetDefault(policy);
            return policy;
        }
    }
}