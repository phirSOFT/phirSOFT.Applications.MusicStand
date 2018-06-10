using Unity.Builder;
using Unity.Extension;

namespace phirSOFT.Applications.MusicStand
{
    public class LogCreation : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Context.Strategies.Add(new LogCreationStrategy(), UnityBuildStage.PreCreation);
        }
    }
}