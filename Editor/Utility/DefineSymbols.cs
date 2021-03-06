using System.Collections.Generic;
using System.Linq;

using UnityEditor;

namespace TalusBackendData.Editor.Utility
{
    public static class DefineSymbols
    {
        private const char _DefineSeperator = ';';
        private static readonly List<string> _AllDefines = new List<string>();

        public static void Add(params string[] defines)
        {
            _AllDefines.Clear();
            _AllDefines.AddRange(GetDefines());
            _AllDefines.AddRange(defines.Except(_AllDefines));
            UpdateDefines(_AllDefines);
        }

        public static void Remove(params string[] defines)
        {
            _AllDefines.Clear();
            _AllDefines.AddRange(GetDefines().Except(defines));
            UpdateDefines(_AllDefines);
        }

        public static bool Contains(string define)
        {
            return GetDefines().Contains(define);
        }

        public static void Clear()
        {
            _AllDefines.Clear();
            UpdateDefines(_AllDefines);
        }

        private static IEnumerable<string> GetDefines() =>
            PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup)
                          .Split(_DefineSeperator)
                          .ToList();

        private static void UpdateDefines(List<string> allDefines)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
                string.Join(_DefineSeperator.ToString(), allDefines.ToArray())
            );

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}