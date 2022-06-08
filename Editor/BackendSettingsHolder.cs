using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using TalusBackendData.Editor.Utility;

namespace TalusBackendData.Editor
{
    /// <summary>
    ///     FilePath must be ignored by GIT!
    ///     BackendSettingsHolder provides information for other Talus Packages.
    /// </summary>
    [FilePath("ProjectSettings/TalusSettings.asset", FilePathAttribute.Location.ProjectFolder)]
    public class BackendSettingsHolder : ScriptableSingleton<BackendSettingsHolder>
    {
        /// <summary>
        ///     TalusSettings.asset path
        /// </summary>
        public string Path => GetFilePath();

        /// <summary>
        ///     Unity3D - BackendSettings Panel Path
        /// </summary>
        private const string _ProviderPath = "Talus Studio/Backend Settings";
        public static string ProviderPath => _ProviderPath;

        /// <summary>
        ///     Talus Web Dashboard - Api URL
        /// </summary>
        [SerializeField]
        private string _ApiUrl = "http://34.252.141.173";
        public string ApiUrl
        {
            get { return _ApiUrl; }
            set
            {
                _ApiUrl = value;
                SaveSettings();
            }
        }

        /// <summary>
        ///     Talus Web Dashboard - Api Token
        /// </summary>
        [SerializeField]
        private string _ApiToken = default;
        public string ApiToken
        {
            get { return _ApiToken; }
            set
            {
                _ApiToken = value;
                SaveSettings();
            }
        }

        /// <summary>
        ///     To save AppId.
        /// </summary>
        [SerializeField]
        private string _AppId;
        public string AppId
        {
            get { return _AppId; }
            set
            {
                _AppId = value;
                SaveSettings();
            }
        }

        /// <summary>
        ///     Talus Packages - Backend Symbol
        ///     Some packages use this symbol for conditional compilation.
        /// </summary>
        private string _BackendSymbol = "ENABLE_BACKEND";
        public string BackendSymbol
        {
            get { return _BackendSymbol; }
        }

        /// <summary>
        ///     Talus Packages - All required packages in prototoypes.
        /// </summary>
        public readonly Dictionary<string, string> Packages = new Dictionary<string, string>
        {
            { "talus-framework",    "com.talus.talusframework" },
            { "talus-kit",          "com.talus.taluskit" },
            { "talus-backenddata",  "com.talus.talusbackenddata" },
            { "talus-ci",           "com.talus.talusci" },
            { "talus-settings",     "com.talus.talussettings" },
            { "talus-playservices", "com.talus.talusplayservicesresolver" },
            { "talus-facebook",     "com.talus.talusfacebook" },
            { "talus-elephant",     "com.talus.taluselephant" }
        };

        public void AddBackendSymbol()
        {
            if (DefineSymbols.Contains(BackendSymbol)) { return; }

            DefineSymbols.Add(BackendSymbol);
            Debug.Log($"[TalusBackendData-Package] {BackendSymbol} define symbol adding...");
        }

        public void RemoveBackendSymbol()
        {
            if (!DefineSymbols.Contains(BackendSymbol)) { return; }

            DefineSymbols.Remove(BackendSymbol);
            Debug.Log($"[TalusBackendData-Package] {BackendSymbol} define symbol removing...");
        }

        public void SaveSettings()
        {
            Save(true);
        }
    }
}