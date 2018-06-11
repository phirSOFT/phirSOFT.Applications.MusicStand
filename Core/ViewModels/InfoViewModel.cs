using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace phirSOFT.Applications.MusicStand.Core.ViewModels
{
    public class InfoViewModel
    {
        public InfoViewModel()
        {
            ApplicationVersion = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion;

            ApplicationTitle = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTitleAttribute>()
                ?.Title;

            CoreVersion = Assembly.GetAssembly(typeof(InfoViewModel)).GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion;

            // Todo: Load actual contract type
            ContractVersion =  Assembly.GetAssembly(typeof(InfoViewModel)).GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion;

        }

        public string ApplicationTitle { get; }

        public string ApplicationVersion { get; }

        public string CoreVersion { get; }

        public string ContractVersion { get; }
    }
}
