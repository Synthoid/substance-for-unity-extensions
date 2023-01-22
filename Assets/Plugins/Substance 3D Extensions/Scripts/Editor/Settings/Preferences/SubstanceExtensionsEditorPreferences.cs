using UnityEngine;
using UnityEditor;

namespace SOS.SubstanceExtensionsEditor
{
    /// <summary>
    /// Handles initializing and drawing UI for substance extension user preferences.
    /// </summary>
    public static class SubstanceExtensionsEditorPreferences
    {
        private const string kLogUpdateWarningsKey = "sos-log-update-warnings";

        private static readonly GUIContent kAssetUpdatesLabel = new GUIContent("Asset Updates", "Settings that affect asset update operations.");
        private static readonly GUIContent kLogUpdateWarningsLabel = new GUIContent("Log Update Warnings", "If true, log harmless warnings about the update process. ie Any inputs or outputs that were removed during .sbsar updates.");

        private static bool initialized = false;
        private static bool logUpdateWarnings = true;

        public static bool LogUpdateWarnings
        {
            get
            {
                Initialize();

                return logUpdateWarnings;
            }
        }

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            if(initialized) return;

            initialized = true;

            logUpdateWarnings = EditorPrefs.GetBool(kLogUpdateWarningsKey, true);
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

            if(SubstanceExtensionsProjectSettings.DisableAutoUpdates)
            {
                EditorGUILayout.HelpBox("Auto updates have been disabled in project settings.", MessageType.Warning);
            }

            EditorGUI.BeginChangeCheck();
            logUpdateWarnings = EditorGUILayout.Toggle(kLogUpdateWarningsLabel, logUpdateWarnings);
            if(EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetBool(kLogUpdateWarningsKey, logUpdateWarnings);
            }
        }
    }
}