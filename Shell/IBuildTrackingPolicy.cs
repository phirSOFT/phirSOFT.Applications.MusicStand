using System.Collections;

namespace phirSOFT.Applications.MusicStand
{
    public interface IBuildTrackingPolicy : IBuilderPolicy
    {
        Stack BuildKeys { get; }
    }
}