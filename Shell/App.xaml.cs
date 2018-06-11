using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using Fluent;
using Nito.AsyncEx;
using phirSOFT.Applications.MusicStand.Core;
using phirSOFT.FluentPrismAdapters;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using Unity;
using static SingleInstanceManager.SingleInstanceManager;

namespace phirSOFT.Applications.MusicStand
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static readonly Guid AppGuid;
        private readonly AsyncManualResetEvent _started = new AsyncManualResetEvent(set: false);
        private SingleInstanceManager.SingleInstanceManager _manager;

        static App()
        {
            Assembly currentAssembly = Assembly.GetAssembly(typeof(App));

            AppGuid = new Guid(currentAssembly.GetCustomAttribute<GuidAttribute>().Value);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(Ribbon),
                (IRegionAdapter)Container.Resolve(typeof(RibbonRegionAdapter)));
            regionAdapterMappings.RegisterMapping(typeof(BackstageTabControl), 
                (IRegionAdapter)Container.Resolve(typeof(BackstageTabControlRegionAdapter)));
            regionAdapterMappings.RegisterMapping(typeof(RibbonMenu), 
                (IRegionAdapter)Container.Resolve(typeof(RibbonMenuRegionAdapter)));
            regionAdapterMappings.RegisterMapping(typeof(RibbonTabItem), 
                (IRegionAdapter)Container.Resolve(typeof(RibbonTabItemRegionAdapter)));

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<SettingsModule>();
            moduleCatalog.AddModule<CoreModule>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _manager = CreateManager(AppGuid.ToString());
            if (_manager.RunApplication(e.Args))
            {
                _manager.SecondInstanceStarted += async (sender, secondE) =>
                {
                    await _started.WaitAsync();

                    var aggregator = Container.Resolve<IEventAggregator>();
                    aggregator.GetEvent<SecondInstanceStartedEvent>().Publish(secondE.CommandLineParameters);
                };
                base.OnStartup(e);
                _started.Set();
            }
            else
            {
                Environment.Exit(exitCode: 1);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _manager.Shutdown();
            base.OnExit(e);
        }

        protected override IContainerExtension CreateContainerExtension()
        {
            IContainerExtension containerExtension = base.CreateContainerExtension();
            ((IContainerProvider) containerExtension).GetContainer()
                .AddNewExtension<BuildTracking>()
                .AddNewExtension<LogCreation>();
            return containerExtension;
        }
    }

    public class SecondInstanceStartedEvent : PubSubEvent<string[]>
    {
    }
}