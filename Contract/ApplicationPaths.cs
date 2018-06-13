using System;
using System.IO;

namespace phirSOFT.Applications.MusicStand.Contract
{
    /// <summary>
    /// Provides path that the application uses.
    /// </summary>
    public static class ApplicationPaths
    {
        /// <summary>
        /// Gets the roaming app data folder of this application.
        /// </summary>
        public static readonly string AppDataFolder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "phirSOFT",
                "MusicStand");

        /// <summary>
        /// Gets the local app data folder of this application.
        /// </summary>
        public static readonly string LocalAppDataFolder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "phirSOFT",
                "MusicStand");
    }
}