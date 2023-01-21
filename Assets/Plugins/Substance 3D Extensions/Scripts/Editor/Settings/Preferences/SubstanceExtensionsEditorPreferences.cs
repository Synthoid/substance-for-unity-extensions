using UnityEngine;
using UnityEditor;

namespace SOS.SubstanceExtensionsEditor
{
    /// <summary>
    /// Handles initializing and drawing UI for substance extension user preferences.
    /// </summary>
    public static class SubstanceExtensionsEditorPreferences
    {
        private const string kDeleteUnusedTexturesKey = "sos-delete-unused-tex";

        private static readonly GUIContent kAssetUpdatesLabel = new GUIContent("Asset Updates", "Settings that affect asset update operations.");
        private static readonly GUIContent kDeleteUnusedTexturesLabel = new GUIContent("Delete Unused Textures", "If true, delete unused graph output textures during .sbsar updates. If false, any outputs that are removed during an asset update will not delete their texture assets from the project.");

        private static bool initialized = false;
        private static bool deleteUnusedTextures = true;

        public static bool DeleteUnusedTextures
        {
            get { return deleteUnusedTextures; }
        }

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            if(initialized) return;

            initialized = true;

            deleteUnusedTextures = EditorPrefs.GetBool(kDeleteUnusedTexturesKey, true);
        }

        [SettingsProvider]
        public static SettingsProvider CreateExtensionsPreferencesSettingsProvider()
        {
            return new SettingsProvider("Preferences/Adobe Substance 3D Extensions", SettingsScope.User)
            {
                keywords = new string[] { "Adobe", "Substance", "3D", "Extensions", "sbsar" },
                guiHandler = OnPreferencesGUI
            };
        }


        private static void OnPreferencesGUI(string searchString)
        {
            float labelWidth = EditorGUIUtility.labelWidth;

            EditorGUIUtility.labelWidth = SubstanceExtensionsEditorUtility.kSettingsLabelWidth;

            DrawAssetUpdatesSection();

            EditorGUIUtility.labelWidth = labelWidth;
        }


        private static void DrawAssetUpdatesSection()
        {
            EditorGUILayout.LabelField(kAssetUpdatesLabel, EditorStyles.boldLabel);
            //TODO: Show warning if project settings has disabled auto updates?

            if(SubstanceExtensionsProjectSettings.DisableAutoUpdates)
            {
                EditorGUILayout.HelpBox("Auto updates have been disabled in project settings.", MessageType.Warning);
            }

            EditorGUI.BeginChangeCheck();
            deleteUnusedTextures = EditorGUILayout.Toggle(kDeleteUnusedTexturesLabel, deleteUnusedTextures);
            if(EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetBool(kDeleteUnusedTexturesKey, deleteUnusedTextures);
            }
        }
    }
}