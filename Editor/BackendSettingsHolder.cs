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
        // TalusSettings.asset path
        public string Path => GetFilePath();

        // Unity3D - BackendSettings Panel Path
        private const string _ProviderPath = "Talus Studio/Backend Auth";
        public static string ProviderPath => _ProviderPath;

        // {Talus Web Dashboard} - Api Root URL
        [SerializeField]
        private string _ApiUrl = "http://34.252.141.173";
        public string ApiUrl
        {
            get => _ApiUrl;
            set
            {
                _ApiUrl = value;
                SaveSettings();
            }
        }

        // {Talus Web Dashboard} - Api Token
        [SerializeField]
        private string _ApiToken = default;
        public string ApiToken
        {
            get => _ApiToken;
            set
            {
                _ApiToken = value;
                SaveSettings();
            }
        }

        [SerializeField]
        private string _AppId;
        public string AppId
        {
            get => _AppId;
            set
            {
                _AppId = value;
                SaveSettings();
            }
        }

        // {Talus Packages} - Backend Symbol
        // Some packages use this symbol for conditional compilation.
        private readonly string _BackendSymbol = "ENABLE_BACKEND";
        public string BackendSymbol => _BackendSymbol;

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

        public void SaveSettings() => Save(true);
    }
}