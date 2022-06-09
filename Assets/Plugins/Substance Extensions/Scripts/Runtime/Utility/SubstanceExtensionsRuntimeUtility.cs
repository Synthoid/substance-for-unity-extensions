using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SOS.SubstanceExtensions
{
    public static class SubstanceExtensionsRuntimeUtility
    {
#region Utility

        public class FileData
        {
            public class Names
            {
                public const string EngineLinux = "libsubstance_ogl3_blend.so";
                public const string EngineMacOS = "libsubstance_ogl3_blend.dylib";
                public const string EngineWindows = "substance_d3d11pc_blend.dll";
                public const string PluginLinux = "libsbsario.so";
                public const string PluginMacOS = "libsbsario.dylib";
                public const string PluginWindows = "sbsario.dll";
            }

            public class GUIDs
            {
                public const string EngineLinux = "659d27409e79e7740ad0aebc20f2090f";
                public const string EngineMacOS = "80f5b5aab5950443fbafbe1cfbdb4d08";
                public const string EngineWindows = "4d93358a4514fe54981d37196b1bb9c7";
                public const string PluginLinux = "9b40b6fe75dcdffaf9b0b0db569525bd";
                public const string PluginMacOS = "e913fe577a00848c58367b3e775b8a2c";
                public const string PluginWindows = "6e45d854f55a342e6b904a84e42d4e69";
            }
        }

        private static string cachedPluginLocalPath = "";
        private static string cachedEngineLocalPath = "";
        private static string cachedPluginPath = "";
        private static string cachedEnginePath = "";

        #endregion

        #region Plugin Loading

#if UNITY_EDITOR
        [MenuItem("Window/SOS/Substance/Log Plugin Paths")]
#endif
        public static void LogPluginPaths()
        {
            Debug.Log($"Substance Engine and Plugin Info\nEngine (local): {GetEngineLocalPath()}\nPlugin (local): {GetPluginLocalPath()}\nEngine (absolute): {GetEnginePath()}\nPlugin (absolute): {GetPluginPath()}");
        }

        /// <summary>
        /// Get the name of the current platform's target engine library.
        /// </summary>
        /// <returns>Name of the current platform's target engine library.</returns>
        public static string GetEngineName()
        {
            switch(Application.platform)
            {
                case RuntimePlatform.LinuxEditor:
                case RuntimePlatform.LinuxPlayer:
                    return FileData.Names.EngineLinux;
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    return FileData.Names.EngineMacOS;
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                    return FileData.Names.EngineWindows;
            }

            return string.Empty;
        }

        /// <summary>
        /// Get the GUID for the current platform's target engine library. Mostly useful for editor operations.
        /// </summary>
        /// <returns>GUID of the current platform's target engine library.</returns>
        public static string GetEngineGUID()
        {
            switch(Application.platform)
            {
                case RuntimePlatform.LinuxEditor:
                case RuntimePlatform.LinuxPlayer:
                    return FileData.GUIDs.EngineLinux;
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    return FileData.GUIDs.EngineMacOS;
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                    return FileData.GUIDs.EngineWindows;
            }

            return string.Empty;
        }

        /// <summary>
        /// Get the local path of the current platform's target engine library.
        /// In the editor, this is relative from the Assets folder.
        /// In a build, this is relative from the relevant build plugins folder.
        /// </summary>
        /// <returns>Local path of the current platform's target engine library.</returns>
        public static string GetEngineLocalPath()
        {
            if (string.IsNullOrEmpty(cachedEngineLocalPath))
            {
#if UNITY_EDITOR
                string guid = GetEngineGUID();

                if(!string.IsNullOrEmpty(guid))
                {
                    cachedEngineLocalPath = AssetDatabase.GUIDToAssetPath(guid);
                }
                else
                {
                    return $"No engine found for current platform: {Application.platform}";
                }
#elif UNITY_WEBGL
                Debug.Log("WebGL Engine not implemented!");
#elif UNITY_STANDALONE_WIN
                cachedEngineLocalPath = string.Format("Plugins/x86_64/{0}", GetEngineName());
#elif UNITY_STANDALONE_OSX
                cachedEngineLocalPath = string.Format("PlugIns/{0}", GetEngineName());
#else
                cachedEngineLocalPath = string.Format("PlugIns/{0}", GetEngineName());
#endif
            }

            return cachedEngineLocalPath;
        }

        /// <summary>
        /// Get the absolute path of the current platform's target engine library.
        /// </summary>
        /// <returns>Absolute path of the current platform's target engine library.</returns>
        public static string GetEnginePath()
        {
            if(string.IsNullOrEmpty(cachedEnginePath))
            {
                cachedEnginePath = string.Format("{0}{1}", Application.dataPath, GetEngineLocalPath().Substring(6));
            }

            return cachedEnginePath;
        }

        /// <summary>
        /// Get the name of the current platform's target plugin library.
        /// </summary>
        /// <returns>Name of the current platform's target plugin library.</returns>
        public static string GetPluginName()
        {
            switch(Application.platform)
            {
                case RuntimePlatform.LinuxPlayer:
                case RuntimePlatform.LinuxEditor:
                    return FileData.Names.PluginLinux;
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    return FileData.Names.PluginMacOS;
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    return FileData.Names.PluginWindows;
            }

            return string.Empty;
        }

        /// <summary>
        /// Get the GUID of the current platform's target plugin library.
        /// </summary>
        /// <returns>GUID of the current platform's target plugin library.</returns>
        public static string GetPluginGUID()
        {
            switch(Application.platform)
            {
                case RuntimePlatform.LinuxPlayer:
                case RuntimePlatform.LinuxEditor:
                    return FileData.GUIDs.PluginLinux;
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    return FileData.GUIDs.PluginMacOS;
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    return FileData.GUIDs.PluginWindows;
            }

            return string.Empty;
        }

        /// <summary>
        /// Get the local path of the current platform's target plugin library.
        /// In the editor, this is relative from the Assets folder.
        /// In a build, this is relative from the relevant build plugins folder.
        /// </summary>
        /// <returns>Local path of the current platform's target plugin library.</returns>
        public static string GetPluginLocalPath()
        {
            if(string.IsNullOrEmpty(cachedPluginLocalPath))
            {
#if UNITY_EDITOR
                string guid = GetPluginGUID();

                if(!string.IsNullOrEmpty(guid))
                {
                    cachedPluginLocalPath = AssetDatabase.GUIDToAssetPath(guid);
                }
                else
                {
                    return $"No plugin found for current platform: {Application.platform}";
                }
#elif UNITY_WEBGL
                Debug.Log("WebGL Plugin not implemented!");
#elif UNITY_STANDALONE_WIN
                cachedPluginLocalPath = string.Format("Plugins/x86_64/{0}", GetPluginName());
#elif UNITY_STANDALONE_OSX
                cachedPluginLocalPath = string.Format("PlugIns/{0}", GetPluginName());
#else
                cachedPluginLocalPath = string.Format("PlugIns/{0}", GetPluginName());
#endif
            }

            return cachedPluginLocalPath;
        }

        /// <summary>
        /// Get the absolute path of the current platform's target plugin library.
        /// </summary>
        /// <returns>Local path of the current platform's target plugin library.</returns>
        public static string GetPluginPath()
        {
            if(string.IsNullOrEmpty(cachedPluginPath))
            {
                cachedPluginPath = string.Format("{0}{1}", Application.dataPath, GetPluginLocalPath().Substring(6));
            }

            return cachedPluginPath;
        }

#endregion
    }
}