using Unity.Builder;
using Unity.Extension;
using Unity.Strategy;

namespace phirSOFT.Applications.MusicStand
{
    public class LogCreation : UnityContainerExtension
    {

        protected override void Initialize()
        {
            Context.Strategies.AddNew<LogCreationStrategy>(UnityBuildStage.PreCreation);
        }
    }
}