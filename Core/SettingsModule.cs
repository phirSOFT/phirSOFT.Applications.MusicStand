using System;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;
using Nito.AsyncEx;
using phirSOFT.SettingsService.Abstractions;
using phirSOFT.SettingsService.Json;
using phirSOFT.SettingsService.Unity;
using Prism.Ioc;
using Prism.Logging;
using Prism.Modularity;
using Unity;

namespace phirSOFT.Applications.MusicStand.Core
{
    [PublicAPI]
    public class SettingsModule : IModule
    {
        private readonly ILoggerFacade _loggerFacade;
        private readonly IContainerExtension<IUnityContainer> _container;

        public SettingsModule(ILoggerFacade loggerFacade, IContainerExtension<IUnityContainer> container)
        {
            _loggerFacade = loggerFacade;
            _container = container;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _loggerFacade.Log("Registring settings extensions.", Category.Info, Priority.None);
            _container.Instance.AddNewExtension<SettingsServiceContainerExtension>();

            string settingsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product,
                "settings.json"
            );
            


            _loggerFacade.Log("Creating json settings service.", Category.Info, Priority.None);
            _loggerFacade.Log($"Loading settings from \"{settingsPath}\"", Category.Debug, Priority.None);
            ISettingsService settingsService = AsyncContext.Run(() => JsonSettingsService.CreateAsync(settingsPath));
            _loggerFacade.Log("Settings loaded", Category.Info, Priority.None);

            containerRegistry.RegisterInstance(settingsService);

            _loggerFacade.Log("Module loaded", Category.Info, Priority.None);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}