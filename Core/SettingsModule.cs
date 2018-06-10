using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nito.AsyncEx.Synchronous;
using phirSOFT.SettingsService.Json;
using Prism.Ioc;
using Prism.Logging;
using Prism.Modularity;

namespace phirSOFT.Applications.MusicStand.Core
{
    
    public class SettingsModule : IModule
    {
        private readonly ILoggerFacade _loggerFacade;

        public SettingsModule(ILoggerFacade loggerFacade)
        {
            _loggerFacade = loggerFacade;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            string settingsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product,
                "settings.json"
            );

            _loggerFacade.Log("Creating json settings service.", Category.Info, Priority.None);
            _loggerFacade.Log($"Loading settings from \"{settingsPath}\"", Category.Debug, Priority.None);
            var settingsService = JsonSettingsService.Create(settingsPath).WaitAndUnwrapException();
            _loggerFacade.Log("Settings loaded", Category.Info, Priority.None);

            containerRegistry.RegisterInstance(settingsService);

            _loggerFacade.Log("Module loaded", Category.Info, Priority.None);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }
    }
}
