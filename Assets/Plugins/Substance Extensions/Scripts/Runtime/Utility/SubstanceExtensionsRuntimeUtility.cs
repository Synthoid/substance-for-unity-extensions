using UnityEngine;
using System;
using System.Collections.Generic;
using Adobe.Substance;
using UnityEditor.Graphs;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Contains Substance3D utility methods for use at runtime.
    /// </summary>
    public static class SubstanceExtensionsRuntimeUtility
    {
        #region Utility

        public class FileData
        {
            public class Names
            {
                public const string kEngineLinux = "libsubstance_ogl3_blend.so";
                public const string kEngineMacOS = "libsubstance_mtl_blend.dylib";
                public const string kEngineWindows = "substance_d3d11pc_blend.dll";
                public const string kPluginLinux = "libsbsario.so";
                public const string kPluginMacOS = "libsbsario.dylib";
                public const string kPluginWindows = "sbsario.dll";
            }

            public class GUIDs
            {
                public const string kEngineLinux = "659d27409e79e7740ad0aebc20f2090f";
                public const string kEngineMacOS = "576b1a9b12cf24674bcf8e9a334f688c";
                public const string kEngineWindows = "4d93358a4514fe54981d37196b1bb9c7";
                public const string kPluginLinux = "9b40b6fe75dcdffaf9b0b0db569525bd";
                public const string kPluginMacOS = "e913fe577a00848c58367b3e775b8a2c";
                public const string kPluginWindows = "6e45d854f55a342e6b904a84e42d4e69";
            }
        }

        private static string cachedPluginLocalPath = "";
        private static string cachedEngineLocalPath = "";
        private static string cachedPluginPath = "";
        private static string cachedEnginePath = "";

        #endregion

        #region Plugin Loading


#if UNITY_EDITOR
        [MenuItem("Window/SOS/Substance/Log Plugin Paths (Adobe)")]
#endif
        public static void LogPluginPathsAdobe()
        {
            Debug.Log($"Substance Engine and Plugin Info (Adobe)\nEngine (absolute): {PlatformUtils.GetEnginePath()}\nPlugin (absolute): {PlatformUtils.GetPluginPath()}");
        }

#if UNITY_EDITOR
        [MenuItem("Window/SOS/Substance/Log Plugin Paths (Custom)")]
#endif
        public static void LogPluginPathsCustom()
        {
            Debug.Log($"Substance Engine and Plugin Info (Custom)\nEngine (local): {GetEngineLocalPath()}\nPlugin (local): {GetPluginLocalPath()}\nEngine (absolute): {GetEnginePath()}\nPlugin (absolute): {GetPluginPath()}");
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
#if UNITY_2021_3_OR_NEWER
                case RuntimePlatform.LinuxServer:
#endif
                    return FileData.Names.kEngineLinux;
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
#if UNITY_2021_3_OR_NEWER
                case RuntimePlatform.OSXServer:
#endif
                    return FileData.Names.kEngineMacOS;
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
#if UNITY_2021_3_OR_NEWER
                case RuntimePlatform.WindowsServer:
#endif
                    return FileData.Names.kEngineWindows;
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
#if UNITY_2021_3_OR_NEWER
                case RuntimePlatform.LinuxServer:
#endif
                    return FileData.GUIDs.kEngineLinux;
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
#if UNITY_2021_3_OR_NEWER
                case RuntimePlatform.OSXServer:
#endif
                    return FileData.GUIDs.kEngineMacOS;
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
#if UNITY_2021_3_OR_NEWER
                case RuntimePlatform.WindowsServer:
#endif
                    return FileData.GUIDs.kEngineWindows;
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
#if UNITY_2021_3_OR_NEWER
                case RuntimePlatform.LinuxServer:
#endif
                    return FileData.Names.kPluginLinux;
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
#if UNITY_2021_3_OR_NEWER
                case RuntimePlatform.OSXServer:
#endif
                    return FileData.Names.kPluginMacOS;
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
#if UNITY_2021_3_OR_NEWER
                case RuntimePlatform.WindowsServer:
#endif
                    return FileData.Names.kPluginWindows;
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
#if UNITY_2021_3_OR_NEWER
                case RuntimePlatform.LinuxServer:
#endif
                    return FileData.GUIDs.kPluginLinux;
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
#if UNITY_2021_3_OR_NEWER
                case RuntimePlatform.OSXServer:
#endif
                    return FileData.GUIDs.kPluginMacOS;
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
#if UNITY_2021_3_OR_NEWER
                case RuntimePlatform.WindowsServer:
#endif
                    return FileData.GUIDs.kPluginWindows;
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