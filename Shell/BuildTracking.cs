using Unity.Builder;
using Unity.Extension;
using Unity.Policy;
using Unity.Strategy;

namespace phirSOFT.Applications.MusicStand
{
    public class BuildTracking : UnityContainerExtension
    {

        protected override void Initialize()
        {
            Context.Strategies.AddNew<BuildTrackingStrategy>(UnityBuildStage.TypeMapping);
        }

        public static IBuildTrackingPolicy GetPolicy(IBuilderContext context)
        {
            return context.Policies.Get<IBuildTrackingPolicy>(context.BuildKey, true);
        }

        public static IBuildTrackingPolicy SetPolicy(IBuilderContext context)
        {
            IBuildTrackingPolicy policy = new BuildTrackingPolicy();
            context.Policies.SetDefault(policy);
            return policy;
        }
    }
}