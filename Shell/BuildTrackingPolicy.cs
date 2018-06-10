using System.Collections;

namespace phirSOFT.Applications.MusicStand
{
    public class BuildTrackingPolicy : IBuildTrackingPolicy
    {

        public BuildTrackingPolicy()
        {
            BuildKeys = new Stack();
        }

        public Stack BuildKeys { get; }
    }
}