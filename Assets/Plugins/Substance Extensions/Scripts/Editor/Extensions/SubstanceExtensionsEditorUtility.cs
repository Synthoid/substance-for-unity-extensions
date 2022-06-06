using UnityEngine;
using UnityEditor;
using System.IO;
using SOS.SubstanceExtensions;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensionsEditor
{
    public static class SubstanceExtensionsEditorUtility
    {
        #region Utility

        public class Labels
        {
            public class Controls
            {
                public static readonly GUIContent LinkedLabel = new GUIContent(EditorGUIUtility.IconContent(EditorGUIUtility.isProSkin ? "d_Linked" : "Linked").image);
                public static readonly GUIContent UnlinkedLabel = new GUIContent(EditorGUIUtility.IconContent(EditorGUIUtility.isProSkin ? "d_Unlinked" : "Linked").image);
            }
        }

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

        //TODO: Move this section to a runtime script...
        #region Plugin Loading

       /*[MenuItem("Window/SOS/Test")]
        private static void Test()
        {
            Debug.Log($"Engine (absolute): {GetEnginePath()}\nEngine (local): {GetEngineLocalPath()}\nPlugin (absolute): {GetPluginPath()}\bPlugin (local): {GetPluginLocalPath()}");
        }*/
        

        public static string GetEngineName()
        {
            if(Application.platform == RuntimePlatform.LinuxEditor ||
                Application.platform == RuntimePlatform.LinuxPlayer)
            {
                return FileData.Names.EngineLinux;
            }
            else if(Application.platform == RuntimePlatform.OSXEditor ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                return FileData.Names.EngineMacOS;
            }
            else if(Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.WindowsPlayer)
            {
                return FileData.Names.EngineWindows;
            }

            return string.Empty;
        }


        public static string GetEngineGUID()
        {
            if(Application.platform == RuntimePlatform.LinuxEditor ||
                Application.platform == RuntimePlatform.LinuxPlayer)
            {
                return FileData.GUIDs.EngineLinux;
            }
            else if(Application.platform == RuntimePlatform.OSXEditor ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                return FileData.GUIDs.EngineMacOS;
            }
            else if(Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.WindowsPlayer)
            {
                return FileData.GUIDs.EngineWindows;
            }

            return string.Empty;
        }


        public static string GetEngineLocalPath()
        {
            if(string.IsNullOrEmpty(cachedEngineLocalPath))
            {
#if UNITY_EDITOR
                string guid = GetEngineGUID();

                if(!string.IsNullOrEmpty(guid))
                {
                    cachedEngineLocalPath = AssetDatabase.GUIDToAssetPath(guid);
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


        public static string GetEnginePath()
        {
            if(string.IsNullOrEmpty(cachedEnginePath))
            {
                cachedEnginePath = string.Format("{0}{1}", Application.dataPath, GetEngineLocalPath().Substring(6));
            }

            return cachedEnginePath;
        }


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


        public static string GetPluginPath()
        {
            if(string.IsNullOrEmpty(cachedPluginPath))
            {
                cachedPluginPath = string.Format("{0}{1}", Application.dataPath, GetPluginLocalPath().Substring(6));
            }

            return cachedPluginPath;
        }

#endregion

#region SerializedProperty

        public static Vector4Int GetVector4IntValue(this SerializedProperty vectorProperty)
        {
            return new Vector4Int(vectorProperty.FindPropertyRelative("x").intValue,
                vectorProperty.FindPropertyRelative("y").intValue,
                vectorProperty.FindPropertyRelative("z").intValue,
                vectorProperty.FindPropertyRelative("w").intValue);
        }

        public static void SetVector4IntValue(this SerializedProperty vectorProperty, Vector4Int vector)
        {
            vectorProperty.FindPropertyRelative("x").intValue = vector.x;
            vectorProperty.FindPropertyRelative("y").intValue = vector.y;
            vectorProperty.FindPropertyRelative("z").intValue = vector.z;
            vectorProperty.FindPropertyRelative("w").intValue = vector.w;
        }

        /// <summary>
        /// Get the <see cref="SubstanceFileSO"/> referenced by a <see cref="SubstanceParameter"/> field.
        /// </summary>
        /// <param name="property"><see cref="SubstanceParameter"/> property to get the target reference for.</param>
        public static SubstanceFileSO GetGUIDReferenceSubstance(this SerializedProperty property)
        {
            string guid = property.FindPropertyRelative("guid").stringValue;

            if(string.IsNullOrEmpty(guid)) return null;

            return AssetDatabase.LoadAssetAtPath<SubstanceFileSO>(AssetDatabase.GUIDToAssetPath(guid));
        }

#endregion

#region Controls


        public static bool DrawLinkedButton(Rect position, bool isLinked)
        {
            return DrawLinkedButton(position, isLinked, Labels.Controls.LinkedLabel, Labels.Controls.UnlinkedLabel);
        }


        public static bool DrawLinkedButton(Rect position, bool isLinked, GUIContent linkedLabel, GUIContent unlinkedLabel)
        {
            //Color cachedGUI = GUI.backgroundColor;

            //GUI.color *= isLinked ? Color.white : new Color(0.7f, 0.7f, 0.7f, 1f);

            if(GUI.Button(position, isLinked ? linkedLabel : unlinkedLabel, EditorStyles.iconButton))
            {
                isLinked = !isLinked;
            }

            //GUI.color = cachedGUI;

            return isLinked;
        }

#endregion
    }
}