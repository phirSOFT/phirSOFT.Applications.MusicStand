using System.Collections;
using Unity.Policy;

namespace phirSOFT.Applications.MusicStand
{
    public interface IBuildTrackingPolicy : IBuilderPolicy
    {
        Stack BuildKeys { get; }
    }
}