using System.Windows;
using Prism.Ioc;
using Prism.Unity;
using Unity;

namespace phirSOFT.Applications.MusicStand
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override IContainerExtension CreateContainerExtension()
        {
            var containerExtension = base.CreateContainerExtension();
            ((IContainerProvider)containerExtension).GetContainer()
                .AddNewExtension<BuildTracking>()
                .AddNewExtension<LogCreation>();
            return containerExtension;
        }

       
    }
}
