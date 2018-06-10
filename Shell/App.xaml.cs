using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using Nito.AsyncEx;
using Prism.Events;
using Prism.Ioc;
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