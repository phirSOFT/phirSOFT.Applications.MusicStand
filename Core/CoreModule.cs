using phirSOFT.Applications.MusicStand.Core.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace phirSOFT.Applications.MusicStand.Core
{
    public class CoreModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
          
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IRegionManager>().RegisterViewWithRegion("Backstage", typeof(InfoView));
        }
    }
}